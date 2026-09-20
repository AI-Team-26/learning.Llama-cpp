using LlamaServerCore;

namespace LlamaServerUI;

/// <summary>Standalone configuration dialog for editing config.json values.</summary>
public sealed class ConfigurationForm : Form
{
    private readonly AppConfig config;
    private readonly Action onRestored;
    private readonly TextBox txtBins = new();
    private readonly TextBox txtGguf = new();
    private readonly NumericUpDown numPort = new() { Minimum = 1, Maximum = 65535, Value = 8001 };
    private readonly TextBox txtYaml = new();
    private readonly Button btnSave = new() { Text = "Save", Width = 80 };
    private readonly Button btnCancel = new() { Text = "Cancel", Width = 80 };

    public ConfigurationForm(AppConfig config, Action onRestored)
    {
        this.config = config;
        this.onRestored = onRestored;
        Text = "Configuration";
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        ShowInTaskbar = false;
        MaximizeBox = false;
        MinimizeBox = false;
        ClientSize = new Size(520, 250);

        // Grid with columns: [label 120] [value 100%] [spacer 10] [browse 70]
        var grid = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 4,
            RowCount = 5,
            Padding = new Padding(16, 4, 16, 8),
        };
        for (var c = 0; c < grid.ColumnCount; c++)
        {
            grid.ColumnStyles.Add(c == 0
                ? new ColumnStyle(SizeType.Absolute, 120)
                : c == 2
                    ? new ColumnStyle(SizeType.Absolute, 10)
                    : c == 3
                        ? new ColumnStyle(SizeType.Absolute, 70)
                        : new ColumnStyle(SizeType.Percent, 100f));
        }
        for (var r = 0; r < grid.RowCount; r++)
            grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));

        Label MakeLabel(string text) => new() { Text = text, AutoSize = true, Anchor = AnchorStyles.Left };
        Button MakeBrowse(TextBox target, bool isFile)
        {
            var btn = new Button { Text = "Browse", Width = 70 };
            btn.Click += (_, _) =>
            {
                if (isFile)
                {
                    using var ofd = new OpenFileDialog { Filter = "YAML files|*.yaml;*.yml" };
                    if (ofd.ShowDialog(this) == DialogResult.OK)
                        target.Text = ofd.FileName;
                }
                else
                {
                    using var fbd = new FolderBrowserDialog();
                    if (fbd.ShowDialog(this) == DialogResult.OK)
                        target.Text = fbd.SelectedPath;
                }
            };
            return btn;
        }

        txtBins.Anchor = txtGguf.Anchor = txtYaml.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        numPort.Anchor = AnchorStyles.Left;

        grid.Controls.Add(MakeLabel("llama.cpp bin:"), 0, 0);
        grid.Controls.Add(txtBins, 1, 0);
        grid.Controls.Add(MakeBrowse(txtBins, isFile: false), 3, 0);

        grid.Controls.Add(MakeLabel("GGUF folder:"), 0, 1);
        grid.Controls.Add(txtGguf, 1, 1);
        grid.Controls.Add(MakeBrowse(txtGguf, isFile: false), 3, 1);

        grid.Controls.Add(MakeLabel("Port:"), 0, 2);
        grid.Controls.Add(numPort, 1, 2);

        grid.Controls.Add(MakeLabel("Models config:"), 0, 3);
        grid.Controls.Add(txtYaml, 1, 3);
        grid.Controls.Add(MakeBrowse(txtYaml, isFile: true), 3, 3);

        var btnPanel = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.LeftToRight,
            AutoSize = true,
            Anchor = AnchorStyles.Left,
            Margin = new Padding(0, 8, 0, 0),
        };
        btnPanel.Controls.Add(btnSave);
        btnPanel.Controls.Add(new Panel { Width = 8, Height = 1 });
        btnPanel.Controls.Add(btnCancel);
        grid.SetColumnSpan(btnPanel, 4);
        grid.Controls.Add(btnPanel, 0, 4);

        Controls.Add(grid);

        // Populate from config
        txtBins.Text = config.LlamaBinsFolder;
        txtGguf.Text = config.GgufFolder;
        numPort.Value = Math.Clamp(config.Port, 1, 65535);
        txtYaml.Text = config.ModelsConfigPath;

        btnSave.Click += OnSaveClick;
        btnCancel.Click += (_, _) => Close();
    }

    private void OnSaveClick(object? sender, EventArgs e)
    {
        config.LlamaBinsFolder = txtBins.Text.Trim();
        config.GgufFolder = txtGguf.Text.Trim();
        config.Port = (int)numPort.Value;
        config.ModelsConfigPath = txtYaml.Text.Trim();
        config.Save(AppConfig.DefaultConfigPath);
        onRestored();
        Close();
    }
}