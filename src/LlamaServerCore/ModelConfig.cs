namespace LlamaServerCore;

// One entry of the .models map in models_config.yaml.
// Optional keys in the YAML map to nullable properties.
public sealed class ModelConfig
{
    public string File { get; set; } = "";
    public string? Alias { get; set; }
    public int? CtxK { get; set; }
    public int? GpuLayers { get; set; }
    public int? CpuMoe { get; set; }
    public int? Batch { get; set; }
    public int? UBatch { get; set; }
    public string? Quant { get; set; }
    public string? SpecType { get; set; }
    public int? SpecDraftNMin { get; set; }
    public int? SpecDraftNMax { get; set; }
    public int? SpecNgramSimpleSizeN { get; set; }
    public int? SpecNgramSimpleSizeM { get; set; }
    public int? SpecNgramSimpleMinHits { get; set; }
    public string? DraftModel { get; set; }
    public int? Jinja { get; set; }
    public int? QwenReasoningEffortMedium { get; set; }
}
