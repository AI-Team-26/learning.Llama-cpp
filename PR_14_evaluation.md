# Bug 14 — Evaluation of the 76 open PRs

**Date:** 2026-11-03 · **Corrected:** 2026-11-04 (after live re-verification)
**Target:** `scripts/UpdateLlamaCpp.py`
**Scope:** every PR open against `main` for Bug 14 (76 PRs). PR #98 is excluded — different task.

> **Correction notice.** The first edition of this document selected **#32** as the best candidate.
> A live re-verification of the no-API fallback showed that #32 — and 11 other candidates rated
> "correct" — fail whenever the newest tag in the releases Atom feed has no published release yet.
> #32 has since been closed. This edition corrects the counts, the ratings and the choice.
> See [Why not #32](#why-not-32).

## TL;DR

| | |
|---|---|
| PRs reviewed | **76** (75 distinct variants — #39/#91 and #71/#85 are byte-identical) |
| Correct and safe to merge | **3** — #95, #25, #69 |
| Break the no-API fallback | **68** — 56 build a 404 URL, 12 give up (or 404) on a tag without a release |
| Other breakage | **2** — #42 returns 33 assets; #50 drops the cudart zip |
| Don't fix the bug | **3** — #59, #87 still use `GITHUB_TOKEN`; #88 deletes the fallback entirely |
| **Chosen design** | multi-tag scrape that reuses `pick_assets()` — best of the 76: **#95**; cleanest implementation: **#106** (`fix/34_bug14_upd_llamacpp_scrape_fallback`) |

## The bug

From the backlog:

> **Bug 14 | UpdateLlamaCpp.py**
> - avoid use GITHUB_TOKEN
> - USE a const for CUDA version WITHOUT minor version (set to "12", nt "12.4").

Two requirements, and both are easy on their own. The difficulty is entirely in the side effect of the second one: the script's **no-API fallback path** builds asset URLs from that constant.

## Two traps, not one

The first edition found trap 1 and stopped there. Trap 2 is what collapses the "correct" list from 15 to 3.

### Trap 1 — the filename always carries the CUDA minor (56 PRs)

The real release assets are:

```
llama-b12072-bin-win-cuda-12.4-x64.zip
cudart-llama-bin-win-cuda-12.4-x64.zip
```

The filename **always carries the CUDA minor version**. Verified against a live release:

```
llama-b12072-bin-win-cuda-12.4-x64.zip   -> HTTP 302 (exists)
llama-b12072-bin-win-cuda-12-x64.zip     -> HTTP 404
```

and against the release-page HTML (`releases/download/b12072/llama-b12072-bin-win-cuda-12.4-x64.zip`). There is no `-cuda-12-x64.zip`.

So `"12"` is valid as a **matching** constant but invalid as a **URL-building** constant. 56 PRs replaced `FALLBACK_CUDA = "12.4"` with `"12"` and left the fallback building filenames with it — every one of them now 404s on the exact path that exists *because* the token was removed:

- before the fix: token set → API works → fallback rarely runs
- after the fix: no token → API rate-limiting is likelier → fallback runs more often → and it is now broken

That is why the two smallest diffs (#77 = 7 lines, #38 = 8 lines) are also among the worst.

### Trap 2 — the newest feed tag is not necessarily a release

The no-API fallback reads the releases **Atom feed**. The highest `bNNNNN` tag in that feed is *not* guaranteed to be a published release: llama.cpp pushes the git tag first and the release (with its 38 assets) is published later — or occasionally never.

On 2026-11-04 the feed's newest tag was `b12109`, while the newest *published* release was `b12102`:

```
github.com/ggml-org/llama.cpp/releases/tag/b12109  -> HTTP 404 (tag only, 0 assets)
github.com/ggml-org/llama.cpp/releases/tag/b12102  -> HTTP 200 (38 assets)
```

A fallback that reads only `max(tags)`, scrapes that single page and gives up when it is empty **fails on the very path it was written to fix** — and, as in trap 1, it runs more often precisely because the token was removed. A correct fallback must **iterate the recent tags** and use the first one that actually has Windows CUDA assets, mirroring what the API path already does:

```python
for rel in sorted(builds, key=..., reverse=True):
    try:
        main, cudart = pick_assets(rel["assets"])
    except SystemExit:
        continue          # still uploading -> try the next build
    return ...
```

Of the 12 candidates that scrape a single tag, **11 abort with `sys.exit(...)`** and **1 (#55) returns URLs that 404**. Only the 3 that loop over several tags survive.

## How the evaluation was done

Every candidate was checked mechanically, not by reading the description:

1. `py_compile` — syntax.
2. Grep for `GITHUB_TOKEN` / `Authorization` / `Bearer` — requirement 1.
3. Extract the CUDA constant — requirement 2.
4. **Replay `latest_release_via_feed()`** with the Atom feed and the release-page request stubbed, then assert that every returned asset name exists:
   - against a real *published* release page — checks the scrape itself; and
   - **against the live feed** (`b12109`, tag-only) — checks that the fallback survives its real input. This second form is the check that separates the 3 correct PRs from the 68 broken ones.
5. Finalists: a live `latest_release_via_feed()` call against the real feed.

A live `--dry-run` was used as evidence in the first edition; it is **not** used here. `--dry-run` only exercises the API path, never the fallback, so it cannot distinguish a working fix from a broken one — it passed for #32 and would pass for all 56 of the 404 PRs.

## Rating rubric

| Rating | Meaning |
|---|---|
| ⭐⭐⭐⭐⭐ | Fixes both requirements; no hardcoded minor anywhere; fallback iterates recent tags, resolves real asset names and reuses `pick_assets()`; minimal diff |
| ⭐⭐⭐⭐ | Same correctness, slightly larger diff, duplicates a little logic, or a style nit |
| ⭐⭐⭐ | Correct fallback, but leaves a hardcoded minor (partial requirement) or adds significant extra code |
| ⭐ | Introduces a regression: 404 URL, a fallback that gives up on a tag without a release, a broken scrape, or a crash |
| ⛔ | Does not fix the bug |

## Results

| Verdict | Count | Notes |
|---|---|---|
| ✅ Correct | **3** | #95, #25, #69 — fallback loops over recent tags |
| ⭐ Regression | **68** | 56 build a 404 URL; 12 give up on a tag without a release (11 exit, #55 404s) |
| ⚠️ Other breakage | **2** | broken scrape (#42, #50) |
| ⛔ No fix | **3** | token still used (#59, #87) or fallback deleted (#88) |

The distribution is flat: the failure is not about effort or diff size but about two constraints the requirement never states — *"keep the fallback URL valid"* and *"the newest feed tag may not be a release"*. The requirement itself ("const without minor") is satisfied literally by every candidate and still produces a broken script.

## Finalists — good and bad

### ✅ #95 — 43 lines · `fix/14_pi_kiwi_20261012_150725` — **best of the 76**

- **Good:** reads the 10 newest tags and scrapes each release page (newest first) for the real asset names; skips builds with no Windows CUDA zip; no minor literal. Verified live — skips the tag-only `b12109` and resolves `b12102` + `cuda-12.4`.
- **Bad:** biggest diff of the correct ones; up to 10 HTML fetches in the worst case; duplicates asset matching instead of reusing `pick_assets()`.

### ✅ #25 — 42 lines · `fix/14c_update_llama_cpp`

- **Good:** loops the 10 newest tags, scrapes each page, breaks on the first with a `llama-*` asset. Verified live → `b12102`.
- **Bad:** commits a `.pyc` and touches `.gitignore`; keeps its own regex instead of `pick_assets()`.

### ✅ #69 — 49 lines · `fix/14z_update_llama_cpp` — different idea, works

- **Good:** no HTML scraping at all — probes `...-cuda-12.N-x64.zip` with `HEAD` requests for N = 9...0 over the 5 newest tags and builds the URLs from the answer. Verified live → `b12102`.
- **Bad:** chatty (up to 10 probes × 5 tags when builds are young), assumes the minor is ≤ 9, and assumes cudart uses the same minor as the main zip. Most complex correct solution.

### ❌ #32 — 28 lines · `fix/14h_update_llama_cpp` — the first edition's choice

- **Good:** token fully removed; `CUDA_VERSION = "12"` used only for matching; fallback scrapes the release page for the real filenames and reuses `pick_assets()`.
- **Bad:** `tag = f"b{max(tags)}"` — **a single attempt**. A live call on 2026-11-04 returned
  `No Windows CUDA 12 llama.cpp asset found on the b12109 release page.` and the script exited.
  Functionally identical to the flaw the first edition criticised in #94.

### ❌ #55 — 17 lines — smallest diff overall

- **Good:** 17 lines, no scraping, no new network behaviour, `CUDA_VERSION = "12"` used for matching.
- **Bad:** keeps `FALLBACK_CUDA = "12.4"`, so the "no minor" requirement is only half met — **and** it is a single attempt: it returns `llama-b12109-bin-win-cuda-12.4-x64.zip` for the tag-only `b12109`, which is a 404.

## Full ranking

Sorted by rating, then diff size. Only 3 of the 76 are correct.

| Rating | PR | ±lines | Good | Bad |
|---|---|---|---|---|
| ⭐⭐⭐⭐ | [#95](https://github.com/AI-Team-26/learning.Llama-cpp/pull/95) | 43 | no `GITHUB_TOKEN`; major-only const; fallback loops 10 tags, resolves real names | duplicates `pick_assets()`; up to 10 fetches |
| ⭐⭐⭐ | [#25](https://github.com/AI-Team-26/learning.Llama-cpp/pull/25) | 42 | no `GITHUB_TOKEN`; major-only const; fallback loops 10 tags, resolves real names | commits a `.pyc`; touches `.gitignore` |
| ⭐⭐⭐ | [#69](https://github.com/AI-Team-26/learning.Llama-cpp/pull/69) | 49 | no `GITHUB_TOKEN`; major-only const; fallback loops 5 tags | up to 10 `HEAD` probes per tag; assumes minor ≤ 9 |
| ⭐ | [#55](https://github.com/AI-Team-26/learning.Llama-cpp/pull/55) | 17 | no `GITHUB_TOKEN`; major-only const used for matching | keeps `FALLBACK_CUDA="12.4"`; single attempt → 404 on a tag-only newest tag |
| ⭐ | [#60](https://github.com/AI-Team-26/learning.Llama-cpp/pull/60) | 31 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | single attempt; lowercase `cuda_version` const |
| ⭐ | [#33](https://github.com/AI-Team-26/learning.Llama-cpp/pull/33) | 36 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | single attempt |
| ⭐ | [#51](https://github.com/AI-Team-26/learning.Llama-cpp/pull/51) | 38 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | single attempt |
| ⭐ | [#94](https://github.com/AI-Team-26/learning.Llama-cpp/pull/94) | 38 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | single attempt |
| ⭐ | [#71](https://github.com/AI-Team-26/learning.Llama-cpp/pull/71) | 39 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | single attempt; duplicate of #85 |
| ⭐ | [#85](https://github.com/AI-Team-26/learning.Llama-cpp/pull/85) | 39 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | single attempt; duplicate of #71 |
| ⭐ | [#30](https://github.com/AI-Team-26/learning.Llama-cpp/pull/30) | 42 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | single attempt; touches `.gitignore` |
| ⭐ | [#46](https://github.com/AI-Team-26/learning.Llama-cpp/pull/46) | 44 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | single attempt |
| ⭐ | [#79](https://github.com/AI-Team-26/learning.Llama-cpp/pull/79) | 44 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | single attempt; touches `.gitignore` |
| ⭐ | [#41](https://github.com/AI-Team-26/learning.Llama-cpp/pull/41) | 49 | no `GITHUB_TOKEN`; major-only const; fallback resolves real names | single attempt; commits a `.pyc` |

### Introduces a regression — 68 PRs

**(a) Fallback builds `...-cuda-12-x64.zip`, which does not exist → 404 on every download (56):**

`#22 #23 #24 #26 #27 #28 #29 #31 #34 #35 #36 #37 #38 #39 #40 #43 #44 #45 #47 #48 #49 #52 #53 #54 #56 #57 #58 #61 #62 #63 #64 #65 #66 #67 #68 #70 #72 #73 #74 #75 #76 #77 #78 #80 #81 #82 #83 #84 #86 #89 #90 #91 #92 #93 #96 #97`

Sorted by size, the most tempting ones (#77 = 7 lines, #38 = 8, #96 = 11, #26/#34/#43 = 12) are exactly the ones that look best in a review and break in production.

**(b) Fallback scrapes a single tag, then gives up (or 404s) because the newest feed tag has no release (12):**

`#30 #32 #33 #41 #46 #51 #55 #60 #71 #79 #85 #94`

All of them except #55 abort with `sys.exit("No Windows CUDA 12 llama.cpp asset found on the b12109 release page.")` on the live 2026-11-04 feed; #55 returns a 404 URL instead. #32 (the first edition's choice) and #94 sit in this group.

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

## Chosen

The correct fix is a **multi-tag scrape that reuses `pick_assets()`**:

1. read the newest N `bNNNNN` tags from the Atom feed;
2. scrape each release page for the real asset names (which carry the CUDA minor);
3. skip the builds that have no Windows CUDA assets yet;
4. hand the names to `pick_assets()`, so the selection rules stay in one place.

Among the original 76 this is best realised by **#95** (⭐⭐⭐⭐). The cleanest implementation is **#106** (`fix/34_bug14_upd_llamacpp_scrape_fallback`, still open), which factors out `recent_tags_via_feed()` and `assets_from_release_page()` and reuses `pick_assets()`. Verified live: it skips the tag-only `b12109` and resolves `b12102` + both `cuda-12.4` zips.

## Why not #32

1. #32 is **not** simultaneously correct, small and non-duplicative — it is small and non-duplicative, but not correct: `latest_release_via_feed()` reads `max(tags)` from the feed, scrapes that one page and calls `sys.exit()` if the page has no Windows CUDA zip. On the live feed (newest tag `b12109`, tag only) it exits instead of downloading `b12102`.
2. The first edition verified #32 with a replay in which the feed was stubbed to a **fixed, known-good tag** (`b11060`) and with a `--dry-run`, which only exercises the API path. Neither could ever observe the failure — both are structurally incapable of rejecting a single-attempt fallback.
3. The first edition had already recorded the same defect for **#94** ("exits immediately if the newest release page has no CUDA asset ... doesn't try the next build down") yet ranked #32 ⭐⭐⭐⭐⭐ with "Bad: none functionally" and #94 ⭐⭐⭐⭐. The two fallbacks are the same shape; only one of them was caught.
4. #32 has since been **closed** and superseded by #106.

## Process lessons

- **76 PRs for a ~28-line change.** The larger cost was triage, not code. `AGENTS.md` already says "check open PRs before starting"; the step was skipped 76 times.
- **The requirement was incomplete, and every attempt filled the gap differently.** "Set the const to 12" is under-specified once that value ends up in a URL. The backlog entry should read: *"and keep the no-API fallback able to download a real file, even when the newest feed tag has no release yet"*.
- **A fallback that makes a single attempt is a fallback that fails on the newest input.** The API path already loops over releases and skips empty ones; the feed fallback must do the same. Any non-looping fallback is suspect by construction.
- **The verification must feed the fallback its real input.** Replaying against a hand-picked good tag, or a `--dry-run` that never reaches the fallback, proves nothing. A test that stubs the feed with a *tag without a release* followed by a *good release* would have rejected 12 of the "correct" candidates and the first edition's choice.
- **No test covers the fallback.** A ~10-line test (stub `http_open`, assert the resolved asset name exists) would have rejected 68 of these 76 PRs before they were opened. That belongs with Features 13/16 (CI for scripts), and it should include the tag-without-release case.
- **Ratings are not about diff size.** The two smallest patches (#77, #38) were among the worst — and the first edition's pick (#32, 28 lines) is broken too.

## Appendix — reproduce the check

```bash
# 1. real asset names carry the CUDA minor — no "-cuda-12-x64.zip" exists
curl -sL https://github.com/ggml-org/llama.cpp/releases/tag/b11060 \
  | grep -o 'releases/download/b11060/[^"]*cuda-12[^"]*x64\.zip' | sort -u

# 2. the newest feed tag is often a tag without a release
curl -s https://github.com/ggml-org/llama.cpp/releases.atom \
  | grep -o '/releases/tag/b[0-9]*' | grep -o 'b[0-9]*' | sort -u | tail -3   # -> b12109 ...
curl -s -o /dev/null -w '%{http_code}\n' \
  https://api.github.com/repos/ggml-org/llama.cpp/releases/tags/b12109        # -> 404 (tag only)

# 3. replay the no-API fallback against the LIVE feed (the real input)
python3 - <<'EOF'
import importlib.util
spec = importlib.util.spec_from_file_location("m", "scripts/UpdateLlamaCpp.py")
m = importlib.util.module_from_spec(spec); spec.loader.exec_module(m)
try:
    tag, assets = m.latest_release_via_feed()
    print("OK ->", tag, [a["name"] for a in assets])
except SystemExit as e:
    print("FAILED ->", e)   # e.g. "No Windows CUDA 12 llama.cpp asset found on the b12109 release page."
EOF
```
