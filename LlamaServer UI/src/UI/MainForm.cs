using System.Reflection;
using LlamaServerCore;

namespace LlamaServerUI;

public sealed class MainForm : Form
{
    private readonly ListBox _modelsList = new();
    private readonly Button _startButton = new() { Text = "Start", AutoSize = true };
    private readonly Button _stopButton = new() { Text = "Stop", AutoSize = true, Enabled = false };
    private readonly Label _statusLabel = new() { AutoSize = true, Text = "Idle" };
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

        var topPanel = new Panel { Dock = DockStyle.Top, Height = 36, Padding = new Padding(8) };
        topPanel.Controls.Add(_startButton);
        topPanel.Controls.Add(_stopButton);
        topPanel.Controls.Add(_statusLabel);
        _startButton.Location = new Point(8, 8);
        _stopButton.Location = new Point(_startButton.Right + 8, 8);
        _statusLabel.Location = new Point(_stopButton.Right + 16, 11);

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

            SetStatus($"Loaded {_modelIds.Count} model(s) from config");
        }
       catch (Exception ex)
        {
            SetStatus($"Error: {ex.Message}");
            MessageBox.Show(this, ex.Message, "Llama Server", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

    private void OnStopClick(object? sender, EventArgs e)
    {
        SetStatus("Stopped");
        AppendLog("[ui] stop requested (logic pending)");
    }
}
