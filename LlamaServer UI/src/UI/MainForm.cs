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
    private readonly Button _configureButton = new()
    {
        Text = "\u2699 Configure",
        AutoSize = true,
        Visible = false,
    };
    private readonly TextBox _logBox = new()
    {
        Multiline = true,
        ReadOnly = true,
        ScrollBars = ScrollBars.Vertical,
        Font = new Font(FontFamily.GenericMonospace, 9f),
    };

    // Config panel controls
    private readonly Panel _configPanel = new()
    {
        Dock = DockStyle.Top,
        Height = 0,
        Padding = new Padding(8),
        BackColor = Color.LightYellow,
    };
    private readonly Label _lblBins = new() { Text = "llama.cpp bin:", AutoSize = true };
    private readonly TextBox _txtBins = new();
    private readonly Button _btnBrowseBins = new() { Text = "\uF07A Browse", Width = 70 };
    private readonly Label _lblGguf = new() { Text = "GGUF folder:", AutoSize = true };
    private readonly TextBox _txtGguf = new();
    private readonly Button _btnBrowseGguf = new() { Text = "\uF07A Browse", Width = 70 };
    private readonly Label _lblPort = new() { Text = "Port:", AutoSize = true };
    private readonly NumericUpDown _numPort = new() { Minimum = 1, Maximum = 65535, Value = 8001 };
    private readonly Label _lblYaml = new() { Text = "Models config:", AutoSize = true };
    private readonly TextBox _txtYaml = new();
    private readonly Button _btnBrowseYaml = new() { Text = "\uF07A Browse", Width = 70 };
    private readonly FlowLayoutPanel _btnRow = new() { FlowDirection = System.Windows.Forms.FlowDirection.TopDown, AutoSize = true, Padding = new Padding(0) };
    private readonly Button _btnSaveConfig = new() { Text = "Save", Width = 80 };
    private readonly Button _btnCancelConfig = new() { Text = "Cancel", Width = 80 };

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
        topPanel.Controls.Add(_configureButton);
        _startButton.Location = new Point(8, 8);
        _stopButton.Location = new Point(_startButton.Right + 8, 8);
        _statusLabel.Location = new Point(_stopButton.Right + 16, 11);
        _warningLabel.Location = new Point(8, 34);
        _configureButton.Location = new Point(_warningLabel.Right + 8, 34);

        // Config panel layout
        var row1 = new TableLayoutPanel { ColumnCount = 4, RowCount = 1, AutoSize = true, Margin = new Padding(0, 0, 0, 6) };
        row1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
        row1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
        row1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10));
        row1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70));
        row1.Controls.Add(_lblBins, 0, 0);
        row1.Controls.Add(_txtBins, 1, 0);
        row1.Controls.Add(new Control(), 2, 0);
        row1.Controls.Add(_btnBrowseBins, 3, 0);

        var row2 = new TableLayoutPanel { ColumnCount = 4, RowCount = 1, AutoSize = true, Margin = new Padding(0, 0, 0, 6) };
        row2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
        row2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
        row2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10));
        row2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70));
        row2.Controls.Add(_lblGguf, 0, 0);
        row2.Controls.Add(_txtGguf, 1, 0);
        row2.Controls.Add(new Control(), 2, 0);
        row2.Controls.Add(_btnBrowseGguf, 3, 0);

        var row3 = new TableLayoutPanel { ColumnCount = 4, RowCount = 1, AutoSize = true, Margin = new Padding(0, 0, 0, 6) };
        row3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
        row3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
        row3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10));
        row3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70));
        row3.Controls.Add(_lblPort, 0, 0);
        row3.Controls.Add(_numPort, 1, 0);
        row3.Controls.Add(new Control(), 2, 0);
        row3.Controls.Add(new Control(), 3, 0);

        var row4 = new TableLayoutPanel { ColumnCount = 4, RowCount = 1, AutoSize = true, Margin = new Padding(0, 0, 0, 6) };
        row4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
        row4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
        row4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10));
        row4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70));
        row4.Controls.Add(_lblYaml, 0, 0);
        row4.Controls.Add(_txtYaml, 1, 0);
        row4.Controls.Add(new Control(), 2, 0);
        row4.Controls.Add(_btnBrowseYaml, 3, 0);

        _btnRow.Controls.Add(_btnSaveConfig);
        _btnRow.Controls.Add(new Panel { Width = 8 });
        _btnRow.Controls.Add(_btnCancelConfig);

        _configPanel.Controls.Add(row1);
        _configPanel.Controls.Add(row2);
        _configPanel.Controls.Add(row3);
        _configPanel.Controls.Add(row4);
        _configPanel.Controls.Add(_btnRow);

        // Main layout
        _modelsList.Dock = DockStyle.Fill;
        _modelsList.SelectionMode = SelectionMode.One;

        _logBox.Dock = DockStyle.Fill;

        var modelsSplitter = new Splitter { Dock = DockStyle.Bottom, Height = 5 };
        var logHost = new Panel { Dock = DockStyle.Bottom, Height = 200 };
        logHost.Controls.Add(_logBox);

        Controls.Add(modelsSplitter);
        Controls.Add(logHost);
        Controls.Add(topPanel);
        Controls.Add(_configPanel);
        Controls.Add(_modelsList);

        _configureButton.Click += OnConfigureClick;
        _btnBrowseBins.Click += (_, _) => BrowseFolder(_txtBins);
        _btnBrowseGguf.Click += (_, _) => BrowseFolder(_txtGguf);
        _btnBrowseYaml.Click += (_, _) => BrowseFile(_txtYaml, "YAML files|*.yaml;*.yml");
        _btnSaveConfig.Click += OnSaveConfigClick;
        _btnCancelConfig.Click += (_, _) => HideConfigPanel();

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

    private void ShowWarning(string message)
    {
        _warningLabel.Text = $"\u26A0 {message}";
        _warningLabel.Visible = true;
        _configureButton.Visible = true;
    }

    private void ClearWarning()
    {
        _warningLabel.Visible = false;
        _configureButton.Visible = false;
    }

    private void SetStatus(string text) => _statusLabel.Text = text;

    private void LoadModels()
    {
        try
        {
            var models = ModelsConfigLoader.Load(_config.ResolvedModelsConfigPath);
            foreach (var (id, model) in models)
            {
                _modelIds.Add(id);
                var exists = File.Exists(Path.Combine(_config.GgufFolder, model.File));
                _modelsList.Items.Add($"{id} {(exists ? "\uD83D\uDFE2" : "\uD83C\uDFA4")}");
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
            ShowWarning("The configuration of the app is not complete");
            SetStatus("Configuration incomplete");
        }
       catch (JsonException ex)
        {
            ShowWarning("Invalid config.json — " + ex.Message);
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

    // --- Config panel ---

    private void ShowConfigPanel()
    {
        _txtBins.Text = _config.LlamaBinsFolder;
        _txtGguf.Text = _config.GgufFolder;
        _numPort.Value = _config.Port;
        _txtYaml.Text = _config.ModelsConfigPath;
        _configPanel.Height = 260;
    }

    private void HideConfigPanel() => _configPanel.Height = 0;

    private void OnConfigureClick(object? sender, EventArgs e) => ShowConfigPanel();

    private void OnSaveConfigClick(object? sender, EventArgs e)
    {
        _config.LlamaBinsFolder = _txtBins.Text.Trim();
        _config.GgufFolder = _txtGguf.Text.Trim();
        _config.Port = (int)_numPort.Value;
        _config.ModelsConfigPath = _txtYaml.Text.Trim();
        _config.Save(AppConfig.DefaultConfigPath);
        HideConfigPanel();
        LoadModels(); // reload models with new config
    }

    private void BrowseFolder(TextBox target)
    {
        using var dlg = new FolderBrowserDialog();
        dlg.InitialDirectory = target.Text.Length > 0 ? target.Text : Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        if (dlg.ShowDialog(this) == DialogResult.OK)
            target.Text = dlg.SelectedPath;
    }

    private void BrowseFile(TextBox target, string filter)
    {
        using var dlg = new OpenFileDialog { Filter = filter };
        if (dlg.ShowDialog(this) == DialogResult.OK)
            target.Text = dlg.FileName;
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
                        _config.LlamaBinsFolder = match.FullName;
                        _config.Save(AppConfig.DefaultConfigPath);
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
                _config.LlamaBinsFolder = p;
                _config.Save(AppConfig.DefaultConfigPath);
                return;
            }
        }

        ShowWarning("The configuration of the app is not complete");
        SetStatus("llama-server.exe not discovered — click Configure to set it up");
        _startButton.Enabled = false;
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
