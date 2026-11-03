#!/usr/bin/env bash
# Unit tests for check_arg_error() in server_common.sh

set -u
export LLAMACPP=/tmp
cd "$(dirname "$0")/../.." || exit 1
source scripts/debug.sh >/dev/null 2>&1
source scripts/common.sh >/dev/null 2>&1
source scripts/server_common.sh >/dev/null 2>&1

fail=0

# Case 1: log with timestamp+level prefix (typical llama.cpp format)
log1=$(mktemp)
cat > "$log1" <<'LOG'
0.00.001.234 I srv    llama_server: loading model
0.00.005.678 E error while handling argument "--spec-type": unknown speculative type: draft-mtp.ngram-simple
LOG
out=$(check_arg_error "$log1")
expected='error while handling argument "--spec-type": unknown speculative type: draft-mtp.ngram-simple'
if [[ "$out" == "$expected" ]]; then echo "PASS case1"; else echo "FAIL case1: got [$out] want [$expected]"; fail=1; fi

# Case 2: log without prefix (raw message as shown in TODO.md)
log2=$(mktemp)
echo 'error while handling argument "--spec-type": unknown speculative type: ngram-simle' > "$log2"
out=$(check_arg_error "$log2")
expected='error while handling argument "--spec-type": unknown speculative type: ngram-simle'
if [[ "$out" == "$expected" ]]; then echo "PASS case2"; else echo "FAIL case2: got [$out] want [$expected]"; fail=1; fi

# Case 3: healthy log -> no output
log3=$(mktemp)
cat > "$log3" <<'LOG'
srv    llama_server: loading model
srv    llama_server: model loaded
listening on http://127.0.0.1:8001
LOG
out=$(check_arg_error "$log3")
if [[ -z "$out" ]]; then echo "PASS case3"; else echo "FAIL case3: unexpected [$out]"; fail=1; fi

# Case 4: missing file -> no output, no crash
out=$(check_arg_error "/nonexistent/log/file.log")
if [[ -z "$out" ]]; then echo "PASS case4"; else echo "FAIL case4: unexpected [$out]"; fail=1; fi

rm -f "$log1" "$log2" "$log3"
exit $fail
