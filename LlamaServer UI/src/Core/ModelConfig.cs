using YamlDotNet.Serialization;

namespace LlamaServerCore;

// One entry of the .models map in models_config.yaml.
// Non-nullable properties are mandatory keys (the launch script uses them
// unconditionally); nullable ones are optional (defaulted or applied only
// to some spec types).
public sealed class ModelConfig
{
    [YamlMember(Alias = "file")] public string File { get; set; } = "";
    [YamlMember(Alias = "ctx_k")] public int CtxK { get; set; }
    [YamlMember(Alias = "gpu_layers")] public int GpuLayers { get; set; }
    [YamlMember(Alias = "cpu_moe")] public int CpuMoe { get; set; }
    [YamlMember(Alias = "batch")] public int Batch { get; set; }
    [YamlMember(Alias = "ubatch")] public int UBatch { get; set; }
    [YamlMember(Alias = "quant")] public string Quant { get; set; } = "";
    [YamlMember(Alias = "spec_type")] public string SpecType { get; set; } = "";
    [YamlMember(Alias = "alias")] public string? Alias { get; set; }
    [YamlMember(Alias = "spec_draft_n_min")] public int? SpecDraftNMin { get; set; }
    [YamlMember(Alias = "spec_draft_n_max")] public int? SpecDraftNMax { get; set; }
    [YamlMember(Alias = "spec_ngram_simple_size_n")] public int? SpecNgramSimpleSizeN { get; set; }
    [YamlMember(Alias = "spec_ngram_simple_size_m")] public int? SpecNgramSimpleSizeM { get; set; }
    [YamlMember(Alias = "spec_ngram_simple_min_hits")] public int? SpecNgramSimpleMinHits { get; set; }
    [YamlMember(Alias = "draft_model")] public string? DraftModel { get; set; }
    [YamlMember(Alias = "jinja")] public int? Jinja { get; set; }
    [YamlMember(Alias = "qwen_reasoning_effort_medium")] public int? QwenReasoningEffortMedium { get; set; }
}
