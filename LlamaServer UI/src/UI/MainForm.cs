namespace LlamaServerUI;

public sealed class MainForm : Form
{
    public MainForm()
    {
        Text = "Llama Server";
        ClientSize = new Size(900, 600);
        StartPosition = FormStartPosition.CenterScreen;
    }
}
