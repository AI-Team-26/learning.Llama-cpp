using System.Reflection;
using System.Text.Json;
using LlamaServerCore;

namespace LlamaServerUI;

public sealed class MainForm : Form
{
    private readonly ListBox modelsList = new();
    private readonly Button startButton = new() { Text = "Start", AutoSize = true };
    private readonly Button stopButton = new() { Text = "Stop", AutoSize = true, Enabled = false };
    private readonly Button configureButton = new() { Text = "\u2699 Configure", AutoSize = true, };
    private readonly TextBox logBox = new()
    {
        Multiline = true,
        ReadOnly = true,
        ScrollBars = ScrollBars.Vertical,
        Dock = DockStyle.Fill,
        Font = new Font(FontFamily.GenericMonospace, 9f),
    };

    // Warning label — shown when config is incomplete
    private readonly Label warningLabel = new()
    {
        AutoSize = true,
        Dock = DockStyle.Fill,
        ForeColor = Color.DarkRed,
        Font = new Font(FontFamily.GenericSansSerif, 11f, FontStyle.Bold),
        Visible = false,
        Padding = new Padding(8),
    };

    private readonly List<string> modelIds = [];
    private readonly AppConfig config;

    public MainForm()
    {
        Text = "Llama Server";
        ClientSize = new Size(900, 600);
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(700, 450);
        Icon = LoadAppIcon();

        // Parent panel for all top content (deterministic stacking)
        var headerPanel = new FlowLayoutPanel()
        {
            Dock = DockStyle.Top,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Margin = Padding.Empty,
            Padding = new Padding(0),
            BorderStyle = BorderStyle.None,
        };

        // Button row (below status)
        var buttonRow = new FlowLayoutPanel
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            Margin = Padding.Empty,
            Padding = new Padding(8),
        };

        buttonRow.Controls.Add(configureButton);
        buttonRow.Controls.Add(startButton);
        buttonRow.Controls.Add(stopButton);

        // Status bar (top of header)
        var statusBarRow = new FlowLayoutPanel
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            Margin = Padding.Empty,
            Padding = new Padding(0),
        };
        statusBarRow.Controls.Add(warningLabel);

        // Last added is docked first; add buttonRow before statusBarRow so
        // the status/message bar appears above the button row.
        headerPanel.Controls.Add(buttonRow);
        headerPanel.Controls.Add(statusBarRow);

        // Main layout
        modelsList.Dock = DockStyle.Fill;
        modelsList.SelectionMode = SelectionMode.One;

        var modelsSplitter = new Splitter { Dock = DockStyle.Bottom, Height = 5 };
        var logHost = new Panel { Dock = DockStyle.Bottom, Height = 200 };
        logHost.Controls.Add(logBox);

        Controls.Add(headerPanel);
        Controls.Add(modelsSplitter);
        Controls.Add(modelsList);
        Controls.Add(logHost);

        configureButton.Click += OnConfigureClick;
        startButton.Click += OnStartClick;
        stopButton.Click += OnStopClick;

        config = AppConfig.Load(AppConfig.DefaultConfigPath);

        // Auto-discover llama-server.exe on first run.
        if (config.LlamaBinsFolder.Length == 0)
        {
            DiscoverLlamaBins();
        }

        LoadModels();
    }

    private static Icon? LoadAppIcon()
    {
        try
        {
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("app.ico");
            return stream is null ? null : new Icon(stream);
        }
        catch (Exception ex) when (ex is IOException or InvalidCastException or ArgumentException)
        {
            return null;
        }
    }

    private void ShowError(string message)
    {
        warningLabel.Text = "\u26A0 " + message;
        warningLabel.ForeColor = Color.DarkRed;
        warningLabel.Visible = true;
    }

    private void SetStatus(string text)
    {
        warningLabel.Text = text;
        warningLabel.ForeColor = Color.Black;
        warningLabel.Visible = true;
    }

    private void ClearStatus() => warningLabel.Visible = false;



    private void LoadModels()
    {
        try
        {
            var models = ModelsConfigLoader.Load(config.ResolvedModelsConfigPath);
            foreach (var (id, model) in models)
            {
                modelIds.Add(id);
                var exists = File.Exists(Path.Combine(config.GgufFolder, model.File));
                modelsList.Items.Add($"{id} {(exists ? "\uD83D\uDFE2" : "\uD83C\uDFA4")}");
            }

            if (config.LastModelId.Length > 0)
            {
                var idx = modelIds.IndexOf(config.LastModelId);
                if (idx >= 0)
                    modelsList.SelectedIndex = idx;
            }
            else if (modelIds.Count > 0)
            {
                modelsList.SelectedIndex = 0;
            }

            ClearStatus();
            SetStatus($"{modelIds.Count} model(s) loaded");
        }
       catch (FileNotFoundException)
        {
            ShowError("The configuration of the app is not complete");
        }
       catch (JsonException ex)
        {
            ShowError("Invalid config.json — " + ex.Message);
        }
       catch (InvalidOperationException ex)
        {
            ShowError(ex.Message);
        }
       catch (Exception ex)
        {
            SetStatus($"Unexpected error: {ex.Message}");
        }
    }

    private string? SelectedModelId =>
        modelsList.SelectedIndex >= 0 ? modelIds[modelsList.SelectedIndex] : null;

    private void AppendLog(string line) => logBox.AppendText(line + Environment.NewLine);

    // --- Configuration dialog ---

    private void OnConfigureClick(object? sender, EventArgs e)
    {
        Visible = false;
        try
        {
            using var dlg = new ConfigurationForm(config, LoadModels);
            dlg.ShowDialog(this);
        }
        finally
        {
            Visible = true;
        }
    }

    private void DiscoverLlamaBins()
    {
        var candidates = new[]
        {
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Programs", "llama-b*-bin-win-cuda*"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "llama-b*-bin-win-cuda*"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "llama-b*-bin-win-cuda*"),
        };

        foreach (var pattern in candidates)
        {
            try
            {
                var dir = Directory.GetParent(pattern);
                if (dir is null) continue;
                foreach (var match in dir.GetDirectories(Path.GetFileName(pattern)))
                {
                    var exe = Path.Combine(match.FullName, "llama-server.exe");
                    if (File.Exists(exe))
                    {
                        config.LlamaBinsFolder = match.FullName;
                        config.Save(AppConfig.DefaultConfigPath);
                        return;
                    }
                }
            }
            catch { /* skip unreadable dirs */ }
        }

        var pathDirs = Environment.GetEnvironmentVariable("PATH")?.Split(Path.PathSeparator) ?? Array.Empty<string>();
        foreach (var p in pathDirs)
        {
            var exe = Path.Combine(p, "llama-server.exe");
            if (File.Exists(exe))
            {
                config.LlamaBinsFolder = p;
                config.Save(AppConfig.DefaultConfigPath);
                return;
            }
        }

        ShowError("The configuration of the app is not complete");
        startButton.Enabled = false;
    }

    // --- Server stubs ---

    private void OnStartClick(object? sender, EventArgs e)
    {
        var id = SelectedModelId;
        if (id is null)
        {
            SetStatus("No model selected");
            return;
        }
        SetStatus($"Starting {id}...");
        AppendLog($"[ui] start requested for '{id}' (logic pending)");
    }

    private void OnStopClick(object? sender, EventArgs e)
    {
        SetStatus("Stopped");
        AppendLog("[ui] stop requested (logic pending)");
    }
}
