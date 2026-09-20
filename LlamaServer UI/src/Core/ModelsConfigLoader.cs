// TODO(feat/sharpyaml-swap): replace YamlDotNet with SharpYaml.
// Use YamlSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower },
// drop all [YamlMember] aliases from ModelConfig except UBatch
// ([JsonPropertyName("ubatch")], since STJ snake_case yields "u_batch").
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace LlamaServerCore;

public static class ModelsConfigLoader
{
    /// <summary>
    /// Parses the .models map of a models_config.yaml file.
    /// Unknown keys (note, pi_agent_model, ...) are ignored.
    /// </summary>
    public static IReadOnlyDictionary<string, ModelConfig> Load(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"Model config not found: '{path}'", path);

        var deserializer = new DeserializerBuilder()
            .IgnoreUnmatchedProperties()
            .Build();

        try
        {
            var root = deserializer.Deserialize<ModelsRoot>(File.ReadAllText(path));
            return root.Models ?? new Dictionary<string, ModelConfig>();
        }
        catch (YamlException ex)
        {
            throw new InvalidOperationException(
                $"Failed to parse '{path}': {ex.Message}", ex);
        }
    }

    private sealed class ModelsRoot
    {
        [YamlMember(Alias = "models")]
        public Dictionary<string, ModelConfig>? Models { get; set; }
    }
}
