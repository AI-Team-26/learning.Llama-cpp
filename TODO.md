# TODO

## ✅ Done

### **[feat/02_llama_server_ui]** Llama Server UI program — all merged (PRs #1–#11)
- PR #1 (docs/01_todo_ui_task): Added UI task to TODO
- PR #2 (feat/02a_scaffold): WinForms project skeleton with Directory.Build.props
- PR #3 (feat/02b_config): AppConfig load/save + F# NUnit+Unquote tests
- PR #4 (feat/03_arg_builder): llama-server argument builder with tests
- PR #5 (ci/05_build): GitHub Actions CI workflow
- PR #6 (feat/04_ui_layout): Main window with model list, YamlDotNet, app icon
- PR #7 (fix/01_first_run_wizard): Silent auto-discovery of llama-server.exe
- PR #8 (fix/02_config_frame): ConfigurationForm rewrite, ShowDialog pattern, start/stop wiring
- PR #11 (fix/UI_main_form): Replace panel spacers with button margins in header row

- **[feat/07_get_prediction_info_dflash]** Feature 7 — get_prediction_info, dflash extraction

## Backlog

- **Feature 7.1**: Print dflash info in test_call output without making lines too long (depends on Feature 7)
- **Feature 7.2**: Extract dflash accepted speculative rate (depends on Feature 7)
- **Feature 6**: [feat/06_sharpyaml] Replace YamlDotNet with SharpYaml (PropertyNamingPolicy=SnakeCaseLower); drop [YamlMember] aliases except [JsonPropertyName("ubatch")] on UBatch — TODO notes already in ModelsConfigLoader.cs/ModelConfig.cs
- Unify the llama-server call of test and prod calls
- Remove/comment out error check of JSON response in run_llamacpp function
- Manage server start failing due to error loading the model
