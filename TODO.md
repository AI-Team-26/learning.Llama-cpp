# TODO

## Backlog

- **Feature 6**: [feat/06_sharpyaml] Llama Server UI: Replace YamlDotNet with SharpYaml (PropertyNamingPolicy=SnakeCaseLower); drop [YamlMember] aliases except [JsonPropertyName("ubatch")] on UBatch — TODO notes already in ModelsConfigLoader.cs/ModelConfig.cs
- Unify the llama-server call of test and prod calls
- Remove/comment out error check of JSON response in run_llamacpp function
- Manage server start failing due to error loading the model

## Done

- **[feat/02_llama_server_ui]** Llama Server UI program initial structure
- **[feat/07_get_prediction_info_dflash]** Feature 7 — get_prediction_info, dflash extraction
- **[feat/07_1_compact_pred_info]** Feature 7.1 — print spec prediction info compactly in test_call output
- **[feat/07_2_dflash_accepted_rate]** Feature 7.2 — extract per-type accepted speculative rate from server log statistics