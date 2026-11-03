# Bug 14 — Evaluation of the 76 open PRs

**Date:** 2026-11-03
**Target:** `scripts/UpdateLlamaCpp.py`
**Scope:** every PR open against `main` for Bug 14 (76 PRs). PR #98 is excluded — different task.

## TL;DR

| | |
|---|---|
| PRs reviewed | **76** (75 distinct variants — #39/#91 and #71/#85 are byte-identical) |
| Correct and safe to merge | **15** |
| Break the no-API fallback (404) | **56** |
| Other breakage | **2** (#42 returns 33 assets; #50 drops the cudart zip) |
| Don't fix the bug | **3** (#59, #87 still use `GITHUB_TOKEN`; #88 deletes the fallback entirely) |
| **Chosen** | **#32** — `fix/14h_update_llama_cpp` ⭐⭐⭐⭐⭐ |

## The bug

From the backlog:

> **Bug 14 | UpdateLlamaCpp.py**
> - avoid use GITHUB_TOKEN
> - USE a const for CUDA version WITHOUT minor version (set to "12", nt "12.4").

Two requirements, and both are easy on their own. The difficulty is entirely in the side effect of the second one: the script's **no-API fallback path** builds asset URLs from that constant.

## The trap that broke 56 of them

The real release asset is:

```
llama-b12072-bin-win-cuda-12.4-x64.zip
cudart-llama-bin-win-cuda-12.4-x64.zip
```

The filename **always carries the CUDA minor version**. Verified against the live release:

```
llama-b12072-bin-win-cuda-12.4-x64.zip   -> HTTP 302 (exists)
llama-b12072-bin-win-cuda-12-x64.zip     -> HTTP 404
```

and against the release-page HTML (`releases/download/b12072/llama-b12072-bin-win-cuda-12.4-x64.zip`). There is no `-cuda-12-x64.zip`.

So `"12"` is valid as a **matching** constant but invalid as a **URL-building** constant. 56 PRs replaced `FALLBACK_CUDA = "12.4"` with `"12"` and left the fallback building filenames with it — every one of them now 404s on the exact path that exists *because* the token was removed:

- before the fix: token set → API works → fallback rarely runs
- after the fix: no token → API rate-limiting is likelier → fallback runs more often → and it is now broken

That is why the two smallest diffs (#77 = 7 lines, #38 = 8 lines) are also among the worst.

Correct approaches found in the set: keep the exact version for URL building (#55), **discover the real asset names by scraping the release page** and feed them to the existing `pick_assets()` (#32, #94, #95, …), or **probe the download URLs with `HEAD`** until the minor resolves (#69).

## How the evaluation was done

Every candidate was checked mechanically, not by reading the description:

1. `py_compile` — syntax.
2. Grep for `GITHUB_TOKEN` / `Authorization` / `Bearer` — requirement 1.
3. Extract the CUDA constant — requirement 2.
4. **Replay `latest_release_via_feed()`** with the Atom feed stubbed to a real build tag and the release-page request stubbed to the **real HTML** (downloaded from `github.com/ggml-org/llama.cpp/releases/tag/b11060`), then assert the returned asset names exist.
5. For the finalists: live `python3 UpdateLlamaCpp.py --dry-run` against the GitHub API.

Check 4 is the one that separates 15 PRs from 61.

## Rating rubric

| Rating | Meaning |
|---|---|
| ⭐⭐⭐⭐⭐ | Fixes both requirements, no hardcoded minor anywhere, fallback still resolves real assets, minimal diff, reuses existing helpers |
| ⭐⭐⭐⭐ | Same correctness, slightly larger diff, duplicates a little logic, or a style nit |
| ⭐⭐⭐ | Correct, but leaves a hardcoded minor (partial requirement) or adds significant extra code |
| ⭐ | Introduces a regression: broken fallback URL, broken scrape, or a crash |
| ⛔ | Does not fix the bug |

## Results

| Verdict | Count | Notes |
|---|---|---|
| ✅ Correct | 15 | Merge candidates — #32 chosen |
| ⭐ Regression | 58 | 56 build a 404 URL; #42 and #50 break the scrape |
| ⛔ No fix | 3 | Token still used (#59, #87) or fallback deleted (#88) |

The distribution is very flat: the failure is not about effort or diff size but about the missing constraint. The requirement ("const without minor") is satisfied literally by every candidate and still produces a broken script — nothing in the TODO text says *"and keep the fallback URL valid"*. The two smallest patches (#77, 7 lines; #38, 8 lines) are among the worst results, while the best one is only 28 lines.

## Finalists — good and bad

### ⭐⭐⭐⭐⭐ #32 — +13/−15 · `fix/14h_update_llama_cpp` — **CHOSEN**

- **Good:** token fully removed (code + docstring + `import os`); `CUDA_VERSION = "12"` used only for matching, no `12.4` literal anywhere; fallback scrapes the release page for exact filenames and reuses `pick_assets()`; smallest *complete* fix (28 lines); verified live (`b12072` + `cuda-12.4` assets) and against real release HTML.
- **Bad:** none functionally. Leaves one stray blank line where the Bug 14 block was removed from `TODO.md`.

### ⭐⭐⭐⭐ #94 — +18/−20

- **Good:** `CUDA_MAJOR = "12"`, no minor literal, scrapes real names, reuses `pick_assets()`, widest safety of the scrapers.
- **Bad:** larger diff; exits immediately if the newest release page has no CUDA asset (doesn't try the next build down, unlike the API path which already loops releases).

### ⭐⭐⭐⭐ #33 — +15/−21

- **Good:** `CUDA_MAJOR = "12"`, clean docstring explaining *why* the minor can't be constructed, tight scrape regex.
- **Bad:** 36 lines of change for a 2-line requirement; second regex next to `cuda12_version()`.

### ⭐⭐⭐⭐ #95 — +23/−20

- **Good:** most resilient — retries up to 10 recent builds, mirroring the API path's behaviour for half-uploaded releases; no minor literal.
- **Bad:** biggest diff of the correct ones; up to 10 HTML fetches in the worst case; duplicates the asset-matching logic instead of reusing `pick_assets()`.

### ⭐⭐⭐⭐ #60 / #51 / #71 / #85 — 31–39 lines

- **Good:** all functionally correct, scrape-based fallback.
- **Bad:** `#60` uses a lowercase `cuda_version` constant (breaks the module-level constant convention); the rest add redundant regex/matching code for the same result. `#71` and `#85` are the same branch content pushed twice.

### ⭐⭐⭐ #69 — 49 lines — different idea, works

- **Good:** no HTML scraping at all — probes `…-cuda-12.N-x64.zip` with `HEAD` requests for N = 9…0 and builds the URLs from the answer; also retries 5 recent tags. Verified to return the exact real names (7 HTTP calls).
- **Bad:** chatty (up to 10 probes × 5 tags when a build is young), assumes the minor is ≤ 9, and assumes cudart uses the same minor as the main zip. Most complex correct solution.

### ⭐⭐⭐ #55 — +6/−11 — smallest diff overall

- **Good:** 17 lines, no scraping, no new network behaviour, `CUDA_VERSION = "12"` used for matching.
- **Bad:** keeps `FALLBACK_CUDA = "12.4"`, so the requirement "no minor version" is only half met — and it silently breaks again at the next CUDA minor bump.

## Full ranking

Sorted by rating, then diff size.

| Rating | PR | ±lines | Good | Bad |
|---|---|---|---|---|
| ⭐⭐⭐⭐⭐ | [#32](https://github.com/AI-Team-26/learning.Llama-cpp/pull/32) | 28 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | — |
| ⭐⭐⭐⭐ | [#60](https://github.com/AI-Team-26/learning.Llama-cpp/pull/60) | 31 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | lowercase const name |
| ⭐⭐⭐⭐ | [#33](https://github.com/AI-Team-26/learning.Llama-cpp/pull/33) | 36 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | — |
| ⭐⭐⭐⭐ | [#51](https://github.com/AI-Team-26/learning.Llama-cpp/pull/51) | 38 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | — |
| ⭐⭐⭐⭐ | [#94](https://github.com/AI-Team-26/learning.Llama-cpp/pull/94) | 38 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | — |
| ⭐⭐⭐⭐ | [#71](https://github.com/AI-Team-26/learning.Llama-cpp/pull/71) | 39 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | duplicate of #85 |
| ⭐⭐⭐⭐ | [#85](https://github.com/AI-Team-26/learning.Llama-cpp/pull/85) | 39 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | duplicate of #71 |
| ⭐⭐⭐⭐ | [#95](https://github.com/AI-Team-26/learning.Llama-cpp/pull/95) | 43 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | 10-tag retry loop |
| ⭐⭐⭐ | [#55](https://github.com/AI-Team-26/learning.Llama-cpp/pull/55) | 17 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | keeps `FALLBACK_CUDA="12.4"` literal |
| ⭐⭐⭐ | [#25](https://github.com/AI-Team-26/learning.Llama-cpp/pull/25) | 42 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | commits a `.pyc` |
| ⭐⭐⭐ | [#30](https://github.com/AI-Team-26/learning.Llama-cpp/pull/30) | 42 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | touches `.gitignore` |
| ⭐⭐⭐ | [#46](https://github.com/AI-Team-26/learning.Llama-cpp/pull/46) | 44 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | — |
| ⭐⭐⭐ | [#79](https://github.com/AI-Team-26/learning.Llama-cpp/pull/79) | 44 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | touches `.gitignore` |
| ⭐⭐⭐ | [#41](https://github.com/AI-Team-26/learning.Llama-cpp/pull/41) | 49 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | commits a `.pyc` |
| ⭐⭐⭐ | [#69](https://github.com/AI-Team-26/learning.Llama-cpp/pull/69) | 49 | no `GITHUB_TOKEN`; major-only const; fallback probes the real filenames | up to 10 `HEAD` probes per tag; assumes minor ≤ 9 |

### Introduces a regression — 56 PRs

All of the following remove the token and add a major-only constant, but the no-API fallback now builds `…-cuda-12-x64.zip`, which does not exist → **404 on every download**:

`#22 #23 #24 #26 #27 #28 #29 #31 #34 #35 #36 #37 #38 #39 #40 #43 #44 #45 #47 #48 #49 #52 #53 #54 #56 #57 #58 #61 #62 #63 #64 #65 #66 #67 #68 #70 #72 #73 #74 #75 #76 #77 #78 #80 #81 #82 #83 #84 #86 #89 #90 #91 #92 #93 #96 #97`

Sorted by size, the most tempting ones (#77 = 7 lines, #38 = 8, #96 = 11, #26/#34/#43 = 12) are exactly the ones that look best in a review and break in production.

### Other breakage

| PR | Problem |
|---|---|
| [#42](https://github.com/AI-Team-26/learning.Llama-cpp/pull/42) | Scrape returns **33 assets** (all platforms, all runtimes) — no filtering at all |
| [#50](https://github.com/AI-Team-26/learning.Llama-cpp/pull/50) | Fallback drops the `cudart` zip → CUDA runtime DLLs never downloaded |

### Do not fix the bug

| PR | Problem |
|---|---|
| [#59](https://github.com/AI-Team-26/learning.Llama-cpp/pull/59) | Still reads `GITHUB_TOKEN` |
| [#87](https://github.com/AI-Team-26/learning.Llama-cpp/pull/87) | Still reads `GITHUB_TOKEN` |
| [#88](https://github.com/AI-Team-26/learning.Llama-cpp/pull/88) | Deletes the no-API fallback entirely (`FEED_URL`, `latest_release_via_feed()`, the `try/except` in `get_latest_release()`) and calls the API directly — the opposite of what removing the token requires |

## Why #32

1. It is the only PR that is simultaneously **correct**, **small**, and **non-duplicative** — it scrapes the real asset names and then hands them to the existing `pick_assets()`, so the selection rules stay in one place.
2. It satisfies both requirements literally: no token anywhere, no `12.4` literal anywhere.
3. It was verified end to end (`--dry-run` → `b12072` + `cuda-12.4`; fallback replay → correct names), not just read.
4. It touches only the script and `TODO.md`, with Bug 14 moved to **Done**.

## Process lessons

- **76 PRs for a 28-line change.** The larger cost here was triage, not code. `AGENTS.md` already says "check open PRs before starting"; the step was skipped 76 times.
- **The requirement was incomplete, and every attempt filled the gap differently.** "Set the const to 12" is under-specified once that value ends up in a URL. Worth adding to the backlog entry: *"and keep the no-API fallback able to download a real file"*.
- **No test covers the fallback.** A 10-line test (stub `http_open`, assert the asset name matches an existing release) would have rejected 58 of these 76 PRs before they were opened. That belongs with Features 13/16 (CI for scripts).
- **Ratings are not about diff size.** The two smallest patches were the worst two.

## Appendix — reproduce the check

```bash
# 1. real asset names carry the CUDA minor — no "-cuda-12-x64.zip" exists
curl -sL https://github.com/ggml-org/llama.cpp/releases/tag/b11060 \
  | grep -o 'releases/download/b11060/[^"]*cuda-12[^"]*x64\.zip' | sort -u

# 2. replay the no-API fallback against a real release page
python3 - <<'EOF'
import importlib.util, io, contextlib
html = open("page.html","rb").read()          # saved release page
class R:
    def __init__(s,b): s.b=b
    def read(s,*a): return s.b
    def __enter__(s): return s
    def __exit__(s,*a): return False
spec = importlib.util.spec_from_file_location("m","scripts/UpdateLlamaCpp.py")
m = importlib.util.module_from_spec(spec); spec.loader.exec_module(m)
m.http_open = lambda url,*a,**k: R(html if "/releases/tag/" in url else b'<link href=".../releases/tag/b11060">')
print(m.latest_release_via_feed())
EOF
```
