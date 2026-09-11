# TODO


## Backlog

- Unify the llama-server call of test adn prod calls
- Removed/commented out error check of JSON response in run_llamacpp function
- Manage server start failing due to error loading the model
- **[feat/02_llama_server_ui]** **Create a UI program to start Llama-server**
    - **Goal:** Windows desktop app replacing `scripts/start_server_prod.sh`: pick a model from `models_config.yaml`, launch `llama-server.exe` with EXACTLY the same arguments the script builds, wait for `/health`, report status. Must show an icon on the taskbar.
    - **Stack (FIXED, no alternatives):** C#, .NET 10 LTS (`net10.0-windows`), WinForms (`<UseWindowsForms>true</UseWindowsForms>`). No Avalonia/WPF. Publish: `dotnet publish -r win-x64 --self-contained true -p:PublishSingleFile=true` → standalone `.exe`, nothing to install. Set `<ApplicationIcon>` (.ico) on the exe and window.
    - **Project layout:** new folder `src/LlamaServerUI/` (+ tests under `tests/` if any logic is unit-testable, e.g. arg builder). Follow repo conventions (`Directory.Build.props`).
    - **App config file** `config.json` next to the exe:
      ```json
      { "LlamaBinsFolder": "D:\Standalone Programs\llama-bXXXX-bin-win-cuda-12.4-x64",
        "GgufFolder": "L:\GGUF", "Port": 8001, "LastModelId": "" }
      ```
      First run: dialog asking for llama.cpp bin folder (must contain `llama-server.exe`). Persist all fields; restore `LastModelId` selection on startup. Defaults mirror `scripts/common.sh`: GGUF folder `L:\GGUF`, port `8001`. Log file: `<appdir>/logs/llama_server.log`.
    - **PHASE 1 — MVP (this task):**
      1. Load & parse `models_config.yaml` (path configurable in `config.json`, default `scripts/models_config.yaml`) using YamlDotNet. Read `.models` map only.
      2. Main window: ComboBox of model IDs (suffix 🟢 if `$GgufFolder/<file>` exists else 🔴), **Start**, **Stop** buttons, status label, read-only multiline log TextBox.
      3. Before starting: kill any running `llama-server` process (`Process.GetProcessesByName("llama-server")` + Kill) — parity with `stop_server`.
      4. Build args per spec below, launch detached via `Process.Start` with stdout+stderr appended to the log file (truncate it first, like `echo "" > $SERVER_LOG`).
      5. Health check: poll `http://127.0.0.1:<port>/health` every 3 s, up to 180 s total → status “Ready 🚀” or timeout error. Also watch the log for `failed to load model` → immediate error state (parity with `server_common.sh`).
      6. Errors must NEVER crash: missing yaml / unknown model / missing .gguf / missing `llama-server.exe` / health timeout / model-load failure → clear message box + status bar text.
      7. Save `LastModelId` when a server is started successfully.
    - **Argument construction (MUST match `start_server_prod.sh`; values from `common.sh`/`server_common.sh`):**
      Fixed args (always, in this order):
      ```
      --host 127.0.0.1 --port <Port> --parallel 1 --prio 3 --n-cpu-ffn 0
      --flash-attn on --kv-unified --load-mode mmap --fit off --no-mmproj --agent
      --cache-reuse 64 --ctx-checkpoints 4 --checkpoint-min-step 16384
      --spec-draft-p-min 0.2 --log-verbosity 4
      --samplers "penalties;dry;top_k;top_p;min_p;temperature"
      --temperature 0.3 --top-k 20 --top-p 0.85 --min-p 0.02
      --repeat-penalty 1.10 --repeat-last-n 512
      --reasoning-preserve --reasoning on --reasoning-budget 4096
      --reasoning-budget-message "... Considering the limited time by the user, I have to give the solution based on the thinking directly now."
      ```
      Per-model mapping (from the selected YAML entry):
      | YAML key | Rule |
      |---|---|
      | `file` *(required)* | full path `$GgufFolder/<file>`; if not found → abort with error |
      | — | `--model <full_path>` |
      | `alias` | `--alias <alias>`, else `--alias <model_id>` |
      | `ctx_k` *(required)* | `--ctx-size <ctx_k * 1024>` |
      | `gpu_layers` *(required)* | `--n-gpu-layers <value>` |
      | `cpu_moe` *(required)* | `--n-cpu-moe <value>` |
      | `batch` / `ubatch` *(required)* | `--batch-size <batch> --ubatch-size <ubatch>` |
      | `quant` *(required)* | split on `/`: before = k/v cache type, after = draft k/v cache type (if no `/`, draft = same). Emit `--cache-type-k/-v` and `--cache-type-k-draft/-v-draft`. Missing `quant` → abort with clear error (parity: script exits too) |
      | `spec_type` *(required)* | if ≠ `none` → `--spec-type <value>` + branch rules below |
      | `spec_draft_n_min/max` | required when `spec_type` contains `draft-simple` or `draft-mtp` → `--spec-draft-n-min/-max`; missing → abort |
      | `spec_ngram_simple_size_n/m/min_hits` | required when `spec_type` contains `ngram-simple` → `--spec-ngram-simple-size-n/-m/--spec-ngram-simple-min-hits`; missing → abort |
      | `spec_type == "dflash"` | NOT supported → abort with error (parity) |
      | `draft_model` | if set and ≠ `none` → `--spec-draft-model $GgufFolder/<draft_model>` |
      | `jinja` | if `== 1` → add `--jinja` |
      | `QWEN_REASONING_EFFORT_MEDIUM` | if `== 1` → `--chat-template-kwargs '{"reasoning_effort":"medium"}'` |
      | — (derived) | `--cache-ram 4096` if `cpu_moe != 0 || gpu_layers != 99`, else `--cache-ram 0` |
    - **Acceptance criteria:**
      - [ ] Builds & runs as single win-x64 exe on clean Windows machine (no .NET install needed)
      - [ ] Shows all models from yaml with 🟢/🔴 file-exists indicator
      - [ ] Generated command line for a given model is byte-identical to what `start_server_prod.sh` would produce (verify by diff against manual run for ≥2 models, e.g. one `draft-mtp` and one `ngram-simple`)
      - [ ] Server starts, health check reports ready; Stop kills it
      - [ ] All error cases show friendly messages, app never crashes
      - [ ] Taskbar icon visible while running
    - **PHASE 2 (later, separate task):** edit/add model entries in the UI and save back to `models_config.yaml`.

