#!/bin/bash
# Set an Environment Variable

set -euo pipefail

# ---------------------------------------------------------------------------
detect_os() {
    case "$(uname -s)" in
        Darwin*)              echo "macos" ;;
        Linux*)               echo "linux" ;;
        MINGW*|MSYS*|CYGWIN*) echo "windows" ;;
        *)                    echo "unknown" ;;
    esac
}

detect_os_2() {
    case "$(uname -s)" in
        Darwin*) echo "macos" ;;
        Linux*)
            if [ -f /etc/os-release ]; then
                # subshell: sourcing does not pollute our variables
                ID=$(. /etc/os-release && echo "${ID:-unknown}")
                echo "linux-${ID}"
            else
                echo "linux-unknown"
            fi
            ;;
        MINGW*|MSYS*|CYGWIN*) echo "windows" ;;
        *) echo "unknown" ;;
    esac
}

# ---------------------------------------------------------------------------
fail() { echo "❌ $*" >&2; exit 1; }


echo "Set an environment variable"
read -rp "Key: " KEY
read -rsp "Secret: " VALUE

[[ -n "$KEY" ]]                          || fail "The key cannot be empty"
[[ "$KEY" =~ ^[A-Za-z_][A-Za-z0-9_]*$ ]] || fail "Invalid key name: '$KEY' (use LETTERS_DIGITS_UNDERSCORE, must not start with a digit)"
[[ -n "$VALUE" ]]                        || fail "The value cannot be empty"


OS=$(detect_os)

if [[ "$OS" == "windows" ]]; then
case "$OS" in
    Windows)
        (( ${#VALUE} <= 1024 )) || fail "setx cannot store values longer than 1024 chars"
        setx "$KEY" "$VALUE" >/dev/null || fail "setx failed"
        export "$KEY=$VALUE"   # also make it visible in this Git Bash session
        ;;
    linux)
        RC="$HOME/.bashrc"
        touch "$RC"
        sed -i "/^export $KEY=/d" "$RC"          # avoid duplicates on re-run
        printf 'export %s=%q\n' "$KEY" "$VALUE" >> "$RC"
        export "$KEY=$VALUE"
        echo "Written to $RC - run 'source $RC' or open a new shell"
        ;;
    macos)
        RC="$HOME/.zshrc"
        touch "$RC"
        sed -i '' "/^export $KEY=/d" "$RC"      # BSD sed: -i needs an empty arg
        printf 'export %s=%q\n' "$KEY" "$VALUE" >> "$RC"
        export "$KEY=$VALUE"
        launchctl setenv "$KEY" "$VALUE"   # also expose to GUI apps until reboot
        echo "Written to $RC - open a new shell"
        ;;
    *)
        fail "Unknown OS: $OS"
        ;;
esac

echo "✔️ Environment variable \"${KEY}\" set successfully"
