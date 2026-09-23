# TODO

## Backlog

- **Feature 7**: `get_pred_info` → rename to `get_prediction_info`, extract dflash/draft-speculative values
- **Feature 7.1**: Print dflash info in test_call output without making lines too long
- **Feature 7.2**: Extract dflash accepted speculative rate (depends on Feature 7)
- **Feature 6**: [feat/06_sharpyaml] Replace YamlDotNet with SharpYaml (PropertyNamingPolicy=SnakeCaseLower); drop [YamlMember] aliases except [JsonPropertyName("ubatch")] on UBatch — TODO notes already in ModelsConfigLoader.cs/ModelConfig.cs
- Unify the llama-server call of test and prod calls
- Remove/comment out error check of JSON response in run_llamacpp function
- Manage server start failing due to error loading the model
