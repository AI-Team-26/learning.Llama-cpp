# TODO

## Backlog


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

- Feature 6 [feat/06_sharpyaml] | Llama Server UI: Replace YamlDotNet with SharpYaml (PropertyNamingPolicy=SnakeCaseLower); drop [YamlMember] aliases except [JsonPropertyName("ubatch")] on UBatch — TODO notes already in ModelsConfigLoader.cs/ModelConfig.cs
- Feature 8 | Unify the llama-server call of test and prod calls

### Scripts robustness

- Bug 15 | printf format-string bugs in the scripts
  `printf "error=$arg_err\n"` (scripts/server_common.sh, scripts/test_models_common.sh) uses log text as the format string, so any `%` corrupts the message (`100% foo` -> `1000 foo`).
  scripts/test_models_common.sh:319 also drops the value: `printf 'error=Failed to extract .usage.completion_tokens n'` (no `%s`, literal `n` instead of `\n`).
  Use `printf 'error=%s\n' "$value"` everywhere.

- Bug 16 | SERVER_LOG is a relative path (scripts/common.sh:13)
  `logs/llama_server.log` only works when the script is launched from the repo root; check_arg_error / check_load_model_fail then read the wrong file.
  Resolve it once against the script directory, so scripts work from any CWD.

- Bug 17 | Startup timeout hardcoded in 3 wait loops
  `for i in {1..60}` + `sleep 3` in server_common.sh, start_server_prod.sh, test_models_common.sh, and the real error is inspected only at `i -eq 60`.
  Introduce a SERVER_START_TIMEOUT_S constant and inspect the last log error on every tick.

- Bug 18 | curl health poll has no timeout
  A server that accepts the connection but hangs blocks the wait loop past the 180 s budget.
  Add `--max-time 2 --connect-timeout 1`.

- Bug 19 | start_server() reads `$1` but validates the global `$model`
  scripts/server_common.sh:81 reads `local model_file="$1"` and line 96 validates the global `$model`; it works only because every caller happens to set that global. Align the parameter handling.

- Feature 15 | Fail fast on any fatal startup exit (generalizes the Bug 1 fix)
  The Bug 1 fix detects one log pattern; port already in use, missing DLL, CUDA init failure or a bad draft model still wait 180 s.
  Detect the process death (PID file / tasklist) instead of pattern-matching the log.

### CI

- Feature 16 | CI job for the shell scripts (extends Feature 13)
  Linux job running shellcheck + `bash -n` + scripts/tests/*.sh, with `paths:` filters so the C# build only runs for `LlamaServer UI/**` and this job only for `scripts/**`.

- Feature 17 | Contract test C# ArgBuilder vs start_server_prod.sh (extends Feature 8)
  Add a `--dry-run` that echoes the built args, then assert it matches ArgBuilder output, so the two implementations cannot drift.

### llama.cpp server API (untapped endpoints)

- Feature 18 | Use /props instead of parsing the server log
  GET /props returns n_ctx, model path, chat template and default sampling exactly; replace the regex scraping in extract_info_from_server_log() (scripts/server_common.sh).

- Feature 19 | Read speculative stats from /slots
  /slots is enabled by default; per-slot draft acceptance and ctx usage beat scraping `tg_3s`-style log lines (see Feature 7.2).

- Feature 20 | Enable --metrics and collect per-model VRAM/KV numbers
  Prometheus endpoint exposes predicted tokens/seconds and kv_cache_usage_ratio; removes the `created`-timestamp TPS arithmetic in scripts/test_models_common.sh.

- Feature 21 | Pre-flight context check with /tokenize
  20+ ctx_k variants in scripts/models_config.yaml and nothing verifies that a prompt (system + tools) fits; tokenize before the run and fail early.

- Feature 22 | Validate `jinja: 1` models with /apply-template
  Pre-flight render of the chat template per model, before launching a full run.

- Feature 23 | Test FIM / /infill (coding assistant use case)
  Only chat-completions is benchmarked today; /infill (+ --spm-infill) is the IDE completion mode this project exists for.

- Feature 24 | Test structured output (GBNF grammar / json_schema)
  Tool support is detected but constrained decoding is untested; needed for reliable tool calls and JSON answers.

## Done

- [fix/34_bug14_update_llamacpp] Bug 14 — UpdateLlamaCpp.py: drop GITHUB_TOKEN, CUDA_VERSION="12" const, no-API fallback scrapes real asset names

- [feat/13_shellcheck] Feature 13 — Add bash syntax check (shellcheck) in CI workflow for shell scripts
- **[feat/12_shell_title_change]** Feature 12 — change the shell title when a local model is launched
- **[feat/02_llama_server_ui]** Llama Server UI program initial structure
- **[feat/07_get_prediction_info_dflash]** Feature 7 — get_prediction_info, dflash extraction
- **[feat/07_1_compact_pred_info]** Feature 7.1 — print spec prediction info compactly in test_call output
- **[feat/07_2_dflash_accepted_rate]** Feature 7.2 — extract per-type accepted speculative rate from server log statistics
- **[feat/10_manage_server_start_failure]** Feature 10 — capture and display specific model-loading error messages from server log