# TODO

## Backlog

- Bug 1: When Pi start I have this warning message:
  "Warning: Could not restore model Llama.cpp/96K . Using Llama.cpp/64K"

- Feature 11: error while handling argument "--spec-type": unknown speculative type: ngram-simle while calling a-start (llam_erver_prod.sh)
  UI still shows "Waiting for llama-server to load model" in loop .
  It has to stop and show the error.
  llama srver log:
  ```
  error while handling argument "--spec-type": unknown speculative type: ngram-simle

    usage:
        --spec-type none,draft-simple,draft-eagle3,draft-mtp,draft-dflash,draft-dspark,ngram-simple,ngram-map-k,ngram-map-k4v,ngram-mod,ngram-cache
                                        comma-separated list of types of speculative decoding to use (default:
                                        none)
                                        
                                        (env: LLAMA_ARG_SPEC_TYPE)

   to show complete usage, run with -h
   ```

- **Feature 6**: [feat/06_sharpyaml] Llama Server UI: Replace YamlDotNet with SharpYaml (PropertyNamingPolicy=SnakeCaseLower); drop [YamlMember] aliases except [JsonPropertyName("ubatch")] on UBatch — TODO notes already in ModelsConfigLoader.cs/ModelConfig.cs
- Feature 8 | Unify the llama-server call of test and prod calls

## Done

- **[feat/02_llama_server_ui]** Llama Server UI program initial structure
- **[feat/07_get_prediction_info_dflash]** Feature 7 — get_prediction_info, dflash extraction
- **[feat/07_1_compact_pred_info]** Feature 7.1 — print spec prediction info compactly in test_call output
- **[feat/07_2_dflash_accepted_rate]** Feature 7.2 — extract per-type accepted speculative rate from server log statistics
- **[feat/10_manage_server_start_failure]** Feature 10 — capture and display specific model-loading error messages from server log
- **[feat/12_change_shell_title]** Feature 12 — change the shell title when a local model is launched.