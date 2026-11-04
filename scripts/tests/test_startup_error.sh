#!/usr/bin/env bash
# Unit tests for check_startup_error() / wait_for_server_ready() in server_common.sh

set -u
# shellcheck disable=SC2034  # SERVER_START_* are read by the sourced functions
export LLAMACPP=/tmp
cd "$(dirname "$0")/.." || exit 1
# shellcheck source=../debug.sh
source debug.sh >/dev/null 2>&1
# shellcheck source=../common.sh
source common.sh >/dev/null 2>&1
# shellcheck source=../server_common.sh
source server_common.sh >/dev/null 2>&1

fail=0

# Case 1: invalid argument is reported on the very first tick
log1=$(mktemp)
cat > "$log1" <<'LOG'
0.00.001.234 I srv    llama_server: loading model
0.00.005.678 E error while handling argument "--spec-type": unknown speculative type: ngram-simle
LOG
out=$(check_startup_error "$log1")
expected='error while handling argument "--spec-type": unknown speculative type: ngram-simle'
if [[ "$out" == "$expected" ]]; then echo "PASS case1"; else echo "FAIL case1: got [$out] want [$expected]"; fail=1; fi

# Case 2: generic loader failure is reported while still loading
log2=$(mktemp)
echo '0.01.000.000 E srv    load_model: failed to load model from file' > "$log2"
out=$(check_startup_error "$log2")
expected='0.01.000.000 E srv    load_model: failed to load model from file'
if [[ "$out" == "$expected" ]]; then echo "PASS case2"; else echo "FAIL case2: got [$out] want [$expected]"; fail=1; fi

# Case 3: MTP context failure is reported while still loading
log3=$(mktemp)
echo '0.02.000.000 W llama_init_from_model: context type MTP requested but model doesn'"'"'t contain MTP layers' > "$log3"
out=$(check_startup_error "$log3")
if [[ "$out" == *"context type MTP requested"* ]]; then echo "PASS case3"; else echo "FAIL case3: unexpected [$out]"; fail=1; fi

# Case 4: healthy log -> no output
log4=$(mktemp)
cat > "$log4" <<'LOG'
srv    llama_server: loading model
srv    llama_server: model loaded
listening on http://127.0.0.1:8001
LOG
out=$(check_startup_error "$log4")
if [[ -z "$out" ]]; then echo "PASS case4"; else echo "FAIL case4: unexpected [$out]"; fail=1; fi

# Case 5: missing file -> no output, no crash
out=$(check_startup_error "/nonexistent/log/file.log")
if [[ -z "$out" ]]; then echo "PASS case5"; else echo "FAIL case5: unexpected [$out]"; fail=1; fi

# Case 6: a fatal log error stops the wait on the first tick
SERVER_START_TIMEOUT_S=1
SERVER_START_POLL_INTERVAL_S=1
out=$(wait_for_server_ready "$log1" "test-model" 2>/dev/null)
status=$?
if [[ "$status" -eq 1 && "$out" == *unknown\ speculative\ type* ]]; then
    echo "PASS case6"
else
    echo "FAIL case6: status=$status out=[$out]"; fail=1
fi

# Case 7: the timeout comes from SERVER_START_TIMEOUT_S instead of a hardcoded 180 s
start_ts=$SECONDS
SERVER_START_TIMEOUT_S=2
SERVER_START_POLL_INTERVAL_S=1
out=$(wait_for_server_ready "$log4" "test-model" 2>/dev/null)
status=$?
elapsed=$(( SECONDS - start_ts ))
if [[ "$status" -eq 1 && "$out" == "error=Server not ready after 2 seconds" && "$elapsed" -le 5 ]]; then
    echo "PASS case7"
else
    echo "FAIL case7: status=$status elapsed=${elapsed}s out=[$out]"; fail=1
fi

rm -f "$log1" "$log2" "$log3" "$log4"
exit $fail