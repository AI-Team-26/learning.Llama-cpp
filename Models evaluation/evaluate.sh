#!/usr/bin/env bash
#
# evaluate_model.sh
#
# Runs one coding task against one local model, N times, and scores each
# attempt against a hidden test suite the model never sees.
#
# ASSUMPTIONS (adjust the run_agent() function if these don't hold):
#   - You're using Aider (https://aider.chat) as the coding agent, pointed
#     at llama.cpp's OpenAI-compatible server (llama-server --port 8080).
#   - llama-server is already running before you call this script (that's
#     your step 2 — this script doesn't start/stop it).
#   - Each task lives in a folder with this layout:
#
#       eval-tasks/<task-name>/
#         TASK.md        <- prompt given to the agent
#         eval.conf       <- TASK_TYPE=dotnet|react , TEST_CMD="..."
#         files.map       <- "<file-in-this-folder>  <destination-in-repo>"
#         <files listed in files.map>
#
#     files.map has two whitespace-separated columns per line. Files whose
#     name starts with HIDDEN_ are copied in ONLY for grading, after the
#     agent has already committed; everything else is copied in as the
#     starting skeleton and handed to the agent as editable files.
#
# USAGE:
#   ./evaluate_model.sh -m <model_label> -t eval-tasks/dotnet-pagination \
#       -r /path/to/your/repo [-n 3] [-u http://localhost:8080/v1] [-p] [-k]
#
#   -m  model label (used in branch names and the results log)   [required]
#   -t  path to the task folder                                  [required]
#   -r  path to your git repo under test                         [required]
#   -n  number of repeated runs (default: 1)
#   -u  llama.cpp OpenAI-compatible base URL (default: http://localhost:8080/v1)
#   -b  base branch to branch from (default: main)
#   -p  push branch and open a PR via `gh` after each run (default: off)
#   -k  keep local eval branches instead of deleting after grading

set -uo pipefail

# ---------- defaults ----------
RUNS=1
LLAMA_URL="http://localhost:8080/v1"
BASE_BRANCH="main"
PUSH_PR=0
KEEP_BRANCHES=0

# ---------- args ----------
while getopts "m:t:r:n:u:b:pk" opt; do
  case "$opt" in
    m) MODEL_LABEL="$OPTARG" ;;
    t) TASK_DIR="$OPTARG" ;;
    r) REPO_DIR="$OPTARG" ;;
    n) RUNS="$OPTARG" ;;
    u) LLAMA_URL="$OPTARG" ;;
    b) BASE_BRANCH="$OPTARG" ;;
    p) PUSH_PR=1 ;;
    k) KEEP_BRANCHES=1 ;;
    *) echo "Unknown option"; exit 1 ;;
  esac
done

: "${MODEL_LABEL:?-m <model_label> is required}"
: "${TASK_DIR:?-t <task_dir> is required}"
: "${REPO_DIR:?-r <repo_dir> is required}"

TASK_NAME="$(basename "$TASK_DIR")"
[ -f "$TASK_DIR/TASK.md" ]    || { echo "Missing $TASK_DIR/TASK.md"; exit 1; }
[ -f "$TASK_DIR/eval.conf" ]  || { echo "Missing $TASK_DIR/eval.conf"; exit 1; }
[ -f "$TASK_DIR/files.map" ]  || { echo "Missing $TASK_DIR/files.map"; exit 1; }

# eval.conf sets TASK_TYPE and TEST_CMD
# shellcheck disable=SC1090
source "$TASK_DIR/eval.conf"
: "${TASK_TYPE:?eval.conf must set TASK_TYPE}"
: "${TEST_CMD:?eval.conf must set TEST_CMD}"

RESULTS_DIR="$(pwd)/eval-results"
RESULTS_CSV="$RESULTS_DIR/results.csv"
mkdir -p "$RESULTS_DIR"
if [ ! -f "$RESULTS_CSV" ]; then
  echo "timestamp,model,task,run,branch,passed,failed,total,notes" > "$RESULTS_CSV"
fi

# ---------- helpers ----------

# Reads files.map into two parallel behaviors: skeleton files (copied before
# the agent runs, and handed to it as editable paths) and hidden test files
# (copied in only for grading, after the agent's commit).
skeleton_dest_paths() {
  awk '$1 !~ /^HIDDEN_/ {print $2}' "$TASK_DIR/files.map"
}
hidden_pairs() {
  awk '$1 ~ /^HIDDEN_/ {print $1, $2}' "$TASK_DIR/files.map"
}

seed_skeleton() {
  while read -r src dest; do
    mkdir -p "$(dirname "$REPO_DIR/$dest")"
    cp "$TASK_DIR/$src" "$REPO_DIR/$dest"
  done < <(awk '$1 !~ /^HIDDEN_/ {print $1, $2}' "$TASK_DIR/files.map")
}

install_hidden_tests() {
  while read -r src dest; do
    mkdir -p "$(dirname "$REPO_DIR/$dest")"
    cp "$TASK_DIR/$src" "$REPO_DIR/$dest"
  done < <(hidden_pairs)
}

remove_hidden_tests() {
  while read -r _ dest; do
    rm -f "$REPO_DIR/$dest"
  done < <(hidden_pairs)
}

# ---- the part you'll likely want to swap out ----
run_agent() {
  local edit_files=()
  while read -r dest; do
    edit_files+=("$REPO_DIR/$dest")
  done < <(skeleton_dest_paths)

  (
    cd "$REPO_DIR" || exit 1
    OPENAI_API_BASE="$LLAMA_URL" \
    OPENAI_API_KEY="sk-local" \
    aider \
      --model "openai/$MODEL_LABEL" \
      --yes \
      --no-show-model-warnings \
      --message-file "$(realpath "$TASK_DIR/TASK.md")" \
      "${edit_files[@]}"
  ) > "$RUN_LOG_DIR/agent.log" 2>&1
}
# ---------------------------------------------------

parse_dotnet_results() {
  local log="$1"
  local line
  line=$(grep -E "Failed:\s*[0-9]+, *Passed:\s*[0-9]+" "$log" | tail -1)
  PASSED=$(echo "$line" | grep -oE "Passed:\s*[0-9]+"  | grep -oE "[0-9]+")
  FAILED=$(echo "$line" | grep -oE "Failed:\s*[0-9]+"  | grep -oE "[0-9]+")
  TOTAL=$(echo  "$line" | grep -oE "Total:\s*[0-9]+"   | grep -oE "[0-9]+")
}

parse_vitest_results() {
  local log="$1"
  local line
  line=$(grep -E "^\s*Tests\s" "$log" | tail -1)
  PASSED=$(echo "$line" | grep -oE "[0-9]+ passed" | grep -oE "[0-9]+")
  FAILED=$(echo "$line" | grep -oE "[0-9]+ failed" | grep -oE "[0-9]+")
  TOTAL=$(echo  "$line" | grep -oE "\([0-9]+\)"     | tr -d '()')
  FAILED=${FAILED:-0}
}

# ---------- main loop ----------
for i in $(seq 1 "$RUNS"); do
  TS=$(date +%Y%m%d-%H%M%S)
  BRANCH="eval/${MODEL_LABEL}/${TASK_NAME}/run${i}-${TS}"
  RUN_LOG_DIR="$RESULTS_DIR/${MODEL_LABEL}_${TASK_NAME}_run${i}_${TS}"
  mkdir -p "$RUN_LOG_DIR"

  echo "=== [$MODEL_LABEL] $TASK_NAME run $i/$RUNS -> $BRANCH ==="

  ( cd "$REPO_DIR" && git checkout -q "$BASE_BRANCH" && git pull -q --ff-only 2>/dev/null
    git checkout -q -B "$BRANCH" )

  seed_skeleton
  ( cd "$REPO_DIR" && git add -A && git commit -q -m "eval: seed $TASK_NAME skeleton" --allow-empty )

  run_agent
  AGENT_EXIT=$?
  if [ $AGENT_EXIT -ne 0 ]; then
    echo "  agent exited non-zero ($AGENT_EXIT) — see $RUN_LOG_DIR/agent.log"
  fi

  install_hidden_tests
  ( cd "$REPO_DIR" && eval "$TEST_CMD" ) > "$RUN_LOG_DIR/test_output.log" 2>&1
  TEST_EXIT=$?

  PASSED=""; FAILED=""; TOTAL=""
  case "$TASK_TYPE" in
    dotnet) parse_dotnet_results "$RUN_LOG_DIR/test_output.log" ;;
    react)  parse_vitest_results "$RUN_LOG_DIR/test_output.log" ;;
    *) echo "  unknown TASK_TYPE '$TASK_TYPE' in eval.conf" ;;
  esac

  NOTES=""
  if [ -z "$PASSED" ]; then
    NOTES="parse_failed_check_log"
    echo "  couldn't parse test summary — check $RUN_LOG_DIR/test_output.log"
  else
    echo "  result: $PASSED/$TOTAL passed, $FAILED failed"
  fi

  echo "$(date -Iseconds),$MODEL_LABEL,$TASK_NAME,$i,$BRANCH,${PASSED:-NA},${FAILED:-NA},${TOTAL:-NA},$NOTES" >> "$RESULTS_CSV"

  remove_hidden_tests

  if [ "$PUSH_PR" -eq 1 ]; then
    ( cd "$REPO_DIR" && git add -A && git commit -q -m "eval: run $i results" --allow-empty \
      && git push -q -u origin "$BRANCH" \
      && gh pr create --fill --head "$BRANCH" --base "$BASE_BRANCH" \
           --body "Automated eval run. $PASSED/$TOTAL hidden tests passed. Model: $MODEL_LABEL." )
  fi

  ( cd "$REPO_DIR" && git checkout -q "$BASE_BRANCH" )
  if [ "$KEEP_BRANCHES" -eq 0 ] && [ "$PUSH_PR" -eq 0 ]; then
    ( cd "$REPO_DIR" && git branch -q -D "$BRANCH" )
  fi
done

echo ""
echo "Done. Results appended to $RESULTS_CSV"