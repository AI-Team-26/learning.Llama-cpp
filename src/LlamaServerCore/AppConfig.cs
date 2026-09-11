using System.Text.Json;

namespace LlamaServerCore;

/// <summary>Persisted application settings (config.json next to the exe).</summary>
public sealed class AppConfig
{
    public string LlamaBinsFolder { get; set; } = "";
    public string GgufFolder { get; set; } = @"L:\GGUF";
    public int Port { get; set; } = 8001;
    public string LastModelId { get; set; } = "";
    public string ModelsConfigPath { get; set; } = "scripts/models_config.yaml";

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        WriteIndented = true,
    };

    public static string DefaultConfigPath =>
        Path.Combine(AppContext.BaseDirectory, "config.json");

    /// <summary>Loads config from <paramref name="path"/>. Missing file returns defaults.</summary>
    /// <exception cref="InvalidOperationException">The file exists but is not valid JSON.</exception>
    public static AppConfig Load(string path)
    {
        if (!File.Exists(path))
            return new AppConfig();

        var json = File.ReadAllText(path);
        try
        {
            // Partial files are fine: absent properties keep their default values.
            return JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException(
                $"Config file '{path}' is not valid JSON: {ex.Message}", ex);
        }
    }

    public void Save(string path)
    {
        var dir = Path.GetDirectoryName(Path.GetFullPath(path));
        Directory.CreateDirectory(dir!);
        File.WriteAllText(path, JsonSerializer.Serialize(this, JsonOpts));
    }
}
