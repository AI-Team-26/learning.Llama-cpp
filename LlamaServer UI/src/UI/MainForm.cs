using System.Reflection;
using System.Text.Json;
using LlamaServerCore;

namespace LlamaServerUI;

public sealed class MainForm : Form
{
    private readonly ListBox _modelsList = new();
    private readonly Button _startButton = new() { Text = "Start", AutoSize = true };
    private readonly Button _stopButton = new() { Text = "Stop", AutoSize = true, Enabled = false };
    private readonly Label _statusLabel = new() { AutoSize = true, Text = "Idle" };
    private readonly Label _warningLabel = new()
    {
        AutoSize = true,
        ForeColor = Color.DarkOrange,
        Font = new Font(FontFamily.GenericSansSerif, 9f, FontStyle.Bold),
        Visible = false,
    };
    private readonly TextBox _logBox = new()
    {
        Multiline = true,
        ReadOnly = true,
        ScrollBars = ScrollBars.Vertical,
        Font = new Font(FontFamily.GenericMonospace, 9f),
    };

    private readonly List<string> _modelIds = [];
    private readonly AppConfig _config;

    public MainForm()
    {
        Text = "Llama Server";
        ClientSize = new Size(900, 600);
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(700, 450);
        Icon = LoadAppIcon();

        var topPanel = new Panel { Dock = DockStyle.Top, Height = 56, Padding = new Padding(8) };
        topPanel.Controls.Add(_startButton);
        topPanel.Controls.Add(_stopButton);
        topPanel.Controls.Add(_statusLabel);
        topPanel.Controls.Add(_warningLabel);
        _startButton.Location = new Point(8, 8);
        _stopButton.Location = new Point(_startButton.Right + 8, 8);
        _statusLabel.Location = new Point(_stopButton.Right + 16, 11);
        _warningLabel.Location = new Point(8, 34);

        _modelsList.Dock = DockStyle.Fill;
        _modelsList.SelectionMode = SelectionMode.One;

        _logBox.Dock = DockStyle.Fill;

        var modelsSplitter = new Splitter { Dock = DockStyle.Bottom, Height = 5 };
        var logHost = new Panel { Dock = DockStyle.Bottom, Height = 200 };
        logHost.Controls.Add(_logBox);

        Controls.Add(modelsSplitter);
        Controls.Add(logHost);
        Controls.Add(topPanel);

        _startButton.Click += OnStartClick;
        _stopButton.Click += OnStopClick;

        _config = AppConfig.Load(AppConfig.DefaultConfigPath);

        // Auto-discover llama-server.exe on first run.
        if (_config.LlamaBinsFolder.Length == 0)
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

    private void LoadModels()
    {
        try
        {
            var models = ModelsConfigLoader.Load(_config.ResolvedModelsConfigPath);
            foreach (var (id, model) in models)
            {
                _modelIds.Add(id);
                var exists = File.Exists(Path.Combine(_config.GgufFolder, model.File));
                _modelsList.Items.Add($"{id} {(exists ? "🟢" : "🔴")}");
            }

            if (_config.LastModelId.Length > 0)
            {
                var idx = _modelIds.IndexOf(_config.LastModelId);
                if (idx >= 0)
                    _modelsList.SelectedIndex = idx;
            }
            else if (_modelIds.Count > 0)
            {
                _modelsList.SelectedIndex = 0;
            }

            ClearWarning();
            SetStatus($"Loaded {_modelIds.Count} model(s) from config");
        }
       catch (FileNotFoundException)
        {
            ShowWarning("Models config not found — check ModelsConfigPath in config.json");
            SetStatus("Configuration incomplete");
        }
       catch (JsonException ex)
        {
            ShowWarning($"Invalid config.json — {ex.Message}");
            SetStatus("Configuration error");
        }
       catch (InvalidOperationException ex)
        {
            ShowWarning(ex.Message);
            SetStatus("Configuration error");
        }
       catch (Exception ex)
        {
            SetStatus($"Unexpected error: {ex.Message}");
        }
    }

    private string? SelectedModelId =>
        _modelsList.SelectedIndex >= 0 ? _modelIds[_modelsList.SelectedIndex] : null;

    private void AppendLog(string line) => _logBox.AppendText(line + Environment.NewLine);

    private void SetStatus(string text) => _statusLabel.Text = text;

    // Stubs: server lifecycle arrives in a follow-up change.
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

    private void ShowWarning(string message)
    {
        _warningLabel.Text = $"\u26A0 {message}";
        _warningLabel.Visible = true;
    }

    private void ClearWarning() => _warningLabel.Visible = false;

    private void DiscoverLlamaBins()
    {
        var candidates = new[]
        {
            // Common download locations for llama.cpp zips
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
                        _config.LlamaBinsFolder = match.FullName;
                        _config.Save(AppConfig.DefaultConfigPath);
                        return;
                    }
                }
            }
            catch { /* skip unreadable dirs */ }
        }

        // Also check PATH
        var pathDirs = Environment.GetEnvironmentVariable("PATH")?.Split(Path.PathSeparator) ?? Array.Empty<string>();
        foreach (var p in pathDirs)
        {
            var exe = Path.Combine(p, "llama-server.exe");
            if (File.Exists(exe))
            {
                _config.LlamaBinsFolder = p;
                _config.Save(AppConfig.DefaultConfigPath);
                return;
            }
        }

        // Nothing found — show warning label instead of popup
        ShowWarning("llama-server.exe not found — set LlamaBinsFolder in config.json manually");
        SetStatus("llama-server.exe not discovered");
        _startButton.Enabled = false;
    }

    private void OnStopClick(object? sender, EventArgs e)
    {
        SetStatus("Stopped");
        AppendLog("[ui] stop requested (logic pending)");
    }
}
