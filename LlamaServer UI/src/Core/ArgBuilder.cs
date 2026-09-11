namespace LlamaServerCore;

public sealed class ArgBuildException : Exception
{
    public ArgBuildException(string message) : base(message) { }
}

// Builds the llama-server.exe argument list matching the production launch script.
public static class ServerArgs
{
    // Values from scripts/common.sh and scripts/server_common.sh
    public static IReadOnlyList<string> FixedArgs(int port) => new[]
    {
        "--host", "127.0.0.1",
        "--port", port.ToString(),
        "--parallel", "1",
        "--prio", "3",
        "--n-cpu-ffn", "0",
        "--flash-attn", "on",
        "--kv-unified",
        "--load-mode", "mmap",
        "--fit", "off",
        "--no-mmproj",
        "--agent",
        "--cache-reuse", "64",
        "--ctx-checkpoints", "4",
        "--checkpoint-min-step", "16384",
        "--spec-draft-p-min", "0.2",
        "--log-verbosity", "4",
        "--samplers", "penalties;dry;top_k;top_p;min_p;temperature",
        "--temperature", "0.3",
        "--top-k", "20",
        "--top-p", "0.85",
        "--min-p", "0.02",
        "--repeat-penalty", "1.10",
        "--repeat-last-n", "512",
        "--reasoning-preserve",
        "--reasoning", "on",
        "--reasoning-budget", "4096",
        "--reasoning-budget-message", ReasoningBudgetMessage,
    };
    private const string ReasoningBudgetMessage =
        "... Considering the limited time by the user, I have to give the solution based on the thinking directly now.";

    public static IReadOnlyList<string> Build(string modelId, int port, ModelConfig modelConfig, string ggufFolder)
    {
        var args = new List<string>(FixedArgs(port));

        if (string.IsNullOrEmpty(modelConfig.Quant))
            throw new ArgBuildException("Variable \"quant\" is missing!");

        // quant: before '/' = k/v cache type, after '/' = draft k/v cache type (same if absent)
        var slash = modelConfig.Quant.IndexOf('/');
        var cacheTypeKv = slash >= 0 ? modelConfig.Quant[..slash] : modelConfig.Quant;
        var cacheTypeDraftKv = slash >= 0 ? modelConfig.Quant[(slash + 1)..] : modelConfig.Quant;

        args.AddRange(new[]
        {
            "--cache-type-k", cacheTypeKv,
            "--cache-type-v", cacheTypeKv,
            "--cache-type-k-draft", cacheTypeDraftKv,
            "--cache-type-v-draft", cacheTypeDraftKv,
        });

        if (string.IsNullOrEmpty(modelConfig.SpecType))
            throw new ArgBuildException("Variable \"spec_type\" is missing!");

        var specType = modelConfig.SpecType;

        var modelFile = Path.Combine(ggufFolder, modelConfig.File);
        args.Add("--model");
        args.Add(modelFile);
        args.Add("--alias");
        args.Add(!string.IsNullOrEmpty(modelConfig.Alias) ? modelConfig.Alias! : modelId);
        args.Add("--ctx-size");
        args.Add((modelConfig.CtxK * 1024).ToString());
        args.Add("--n-gpu-layers");
        args.Add(modelConfig.GpuLayers.ToString());
        args.Add("--n-cpu-moe");
        args.Add(modelConfig.CpuMoe.ToString());
        args.Add("--batch-size");
        args.Add(modelConfig.Batch.ToString());
        args.Add("--ubatch-size");
        args.Add(modelConfig.UBatch.ToString());

        if (specType != "none")
        {
            args.Add("--spec-type");
            args.Add(specType);
        }

        // Disable RAM "cache" if no MoE or GPU offloading
        if (modelConfig.CpuMoe != 0 || modelConfig.GpuLayers != 99)
            args.AddRange(new[] { "--cache-ram", "4096" });
        else
            args.AddRange(new[] { "--cache-ram", "0" });

        if (specType.Contains("draft-simple"))
        {
            RequireArg(args, modelConfig.SpecDraftNMin, "spec_draft_n_min", "--spec-draft-n-min");
            RequireArg(args, modelConfig.SpecDraftNMax, "spec_draft_n_max", "--spec-draft-n-max");
        }

        if (specType.Contains("ngram-simple"))
        {
            RequireArg(args, modelConfig.SpecNgramSimpleSizeN, "spec_ngram_simple_size_n", "--spec-ngram-simple-size-n");
            RequireArg(args, modelConfig.SpecNgramSimpleSizeM, "spec_ngram_simple_size_m", "--spec-ngram-simple-size-m");
            RequireArg(args, modelConfig.SpecNgramSimpleMinHits, "spec_ngram_simple_min_hits", "--spec-ngram-simple-min-hits");
        }

        if (specType.Contains("draft-mtp"))
        {
            RequireArg(args, modelConfig.SpecDraftNMin, "spec_draft_n_min", "--spec-draft-n-min");
            RequireArg(args, modelConfig.SpecDraftNMax, "spec_draft_n_max", "--spec-draft-n-max");
        }

        // EXPERIMENTAL: ngram-mod (hardcoded defaults)
        if (specType.Contains("ngram-mod"))
        {
            args.AddRange(new[]
            {
                "--spec-ngram-mod-n-match", "24",
                "--spec-ngram-mod-n-min", "8",
                "--spec-ngram-mod-n-max", "32",
            });
        }

        if (specType == "dflash")
            throw new ArgBuildException("Spec \"DFlash\" not supported!");

        if (!string.IsNullOrEmpty(modelConfig.DraftModel) && modelConfig.DraftModel != "none")
        {
            args.Add("--spec-draft-model");
            args.Add(Path.Combine(ggufFolder, modelConfig.DraftModel));
        }

        if (modelConfig.Jinja == 1)
            args.Add("--jinja");

        if (modelConfig.QwenReasoningEffortMedium == 1)
            args.AddRange(new[] { "--chat-template-kwargs", "{\"reasoning_effort\":\"medium\"}" });

        return args;
    }

    private static void RequireArg(List<string> args, int? value, string varName, string argName)
    {
        if (value is null)
            throw new ArgBuildException($"Argument \"{varName}\" is missing!");
        args.Add(argName);
        args.Add(value.ToString());
    }
}
