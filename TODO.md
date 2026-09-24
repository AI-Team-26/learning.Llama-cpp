# TODO

## Backlog

- **Feature 6**: [feat/06_sharpyaml] Llama Server UI: Replace YamlDotNet with SharpYaml (PropertyNamingPolicy=SnakeCaseLower); drop [YamlMember] aliases except [JsonPropertyName("ubatch")] on UBatch — TODO notes already in ModelsConfigLoader.cs/ModelConfig.cs
- Feature 8 | Unify the llama-server call of test and prod calls
- Feature 10 | Manage server start failing due to error loading the model
  When the model does not support MTP for example:
  ```
  0.13.744.231 W llama_init_from_model: context type MTP requested but model doesn't contain MTP layers
  0.13.744.245 E common_speculative_init_result: failed to create MTP context
  0.13.744.287 E srv    load_model: failed to create MTP context
  0.13.744.321 I srv    operator(): operator(): cleaning up before exit...
  0.13.746.229 E srv  llama_server: exiting due to model loading error
  ```

  Should show this error: "X Can't start the serve. Error: context type MTP requested but model doesn't contain MTP layers" 

## Done

- **[feat/02_llama_server_ui]** Llama Server UI program initial structure
- **[feat/07_get_prediction_info_dflash]** Feature 7 — get_prediction_info, dflash extraction
- **[feat/07_1_compact_pred_info]** Feature 7.1 — print spec prediction info compactly in test_call output
- **[feat/07_2_dflash_accepted_rate]** Feature 7.2 — extract per-type accepted speculative rate from server log statistics