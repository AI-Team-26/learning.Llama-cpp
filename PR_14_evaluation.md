# Bug 14 — model evaluation

**Date:** 2026-11-04
**Task:** backlog Bug 14 — `scripts/UpdateLlamaCpp.py` must stop using `GITHUB_TOKEN` **and** use a CUDA version constant *without* the minor version (`"12"`, not `"12.4"`).
**Sample:** the 76 PRs opened against that task. Every PR body carries an `AI agent execution report` naming the model that produced the run, so the 76 PRs are **76 independent runs of one task on 31 model configurations**.

> **What this document rates.** The models, not the pull requests. The PR is only the container the run was delivered in — where this edition disagrees with the first one it is because the two judged different things. The first edition ranked the PRs, picked #32, and was wrong: #32's fallback breaks on the input it exists for. That verdict is not repeated here; the run-level detail survives as [Appendix A](#appendix-a--run-by-run).

## TL;DR

| | |
|---|---|
| Runs | **76** (74 name a model; #24 and #48 have no report) |
| Model configurations | **31**, in 8 families |
| 🥇 Solved the task | **3 runs / 3 configurations** — 96 % of configurations never produced a working fallback |
| 🥈 Near miss | **35 runs / 6 configurations** — scraped the release page, but made a *single* attempt |
| 🥉 Naive | **34 runs / 20 configurations** — pasted the new constant straight into the asset URL → every download 404s |
| ⛔ Off-target | **2 runs / 2 configurations** — did not change the behaviour at all |
| Best run | **#95** · `Swift-1.5-Qwen3.8-27B-GSQ-RCO-IQ3_S-mtp (ukisai) [64k]` |
| Most efficient failure | `z-ai/glm-5.3` — 0.6 min, 9.2 k tokens, still ships a 404 URL |

Best on substance: **#25 and #69** — one run each, one hit each, no regressions. #95 is the best *implementation* but its model solved only 1 of 3 attempts and shipped two regressions.

## The task is a trap, and that is the point

Both requirements are literal and trivial: delete the token, rename a constant. The trap is the **side effect of requirement 2 on the no-API fallback**, which builds asset URLs from that very constant.

### Trap 1 — the filename always carries the CUDA minor

```
llama-b12072-bin-win-cuda-12.4-x64.zip   -> 302 (exists)
llama-b12072-bin-win-cuda-12-x64.zip     -> 404
```

No `-cuda-12-x64.zip` asset exists. So `"12"` is a valid **matching** constant and an invalid **URL-building** constant. Any model that obeyed the instruction literally without noticing the URL broke the script — on exactly the path that removing the token makes *more* likely to run.

### Trap 2 — the newest feed tag is not necessarily a release

The fallback reads the releases **Atom feed**. Its newest tag may have no release yet (the tag is pushed first, the assets later — sometimes never). A fallback that reads `max(tags)` and scrapes that single page is therefore wrong by construction: it must iterate the recent tags and take the first with real assets, exactly as the API path does.

```
2026-11-04   /releases/tag/b12109  -> 404   (tag only, 0 assets)
             /releases/tag/b12102  -> 200   (38 assets)
```

Neither trap is stated in the backlog entry. Both are inferable from the code, and that is what the sample measures.
## Scoreboard

Sorted by tier, then by number of runs. `✅` correct · `🥈` single-attempt scrape · `❌` 404 URL · `⚠️` other breakage or no fix.

| Model configuration | Tier | Runs | ✅ | 🥈 | ❌ | ⚠️ | Avg time | Avg tokens |
|---|:--:|--:|--:|--:|--:|--:|--:|--:|
| `Swift-1.5-Qwen3.8-27B-GSQ-RCO-IQ3 S-mtp ukisai 64k` | 🥇 solved | 3 | 1 | | 2 | | 7.3m | 21,167 |
| `Qwen3.8 27B UD-IQ4 XS Unsloth 40k Q8 ngram` | 🥇 solved | 1 | 1 | | | | 9.0m | 23,813 |
| `Qwen3.8-27B UD-Q3-K-XL Unsloth Q4 80k` | 🥇 solved | 1 | 1 | | | | 11.5m | 29,626 |
| `Swift-Qwen3.8-27B-IQ4 XS ahmeddelkilami01 64k` | 🥈 near miss | 12 | | 3 | 9 | | 3.9m | 14,275 |
| `Qwen3.8 27B UD-IQ4 XS Unsloth 56k` | 🥈 near miss | 6 | | 2 | 3 | 1 | 7.9m | 18,292 |
| `Qwen3.8-27B-IQ4 XS byteshape 56k` | 🥈 near miss | 6 | | 3 | 3 | | 8.0m | 21,154 |
| `Qwen3.8 27B UD-IQ4 XS Unsloth 52k` | 🥈 near miss | 5 | | 2 | 3 | | 9.4m | 18,025 |
| `Qwen3.8-27B-IQ3 S ista 64k` | 🥈 near miss | 5 | | 1 | 4 | | 2.8m | 15,711 |
| `Qwen3.8 27B UD-IQ4 XS Unsloth 48k Q8` | 🥈 near miss | 1 | | 1 | | | 12.6m | 19,729 |
| `Swift-1.5-Qwen3.8-27B-Uncensored-VAL-APEX-I-MiniPlus-V3 isvalorum` | 🥉 naive | 4 | | | 4 | | 3.2m | 13,762 |
| `Qwen3.8-27B-IQ3 S ista 64k ngram` | 🥉 naive | 3 | | | 3 | | 5.1m | 20,021 |
| `Swift-1.5-Qwen3.8-27b.i1-IQ4 XS mrradermacher Q4 48k` | 🥉 naive | 3 | | | 3 | | 4.5m | 14,642 |
| `Qwen3.8 27B UD-IQ4 XS Unsloth 52k ngram` | 🥉 naive | 2 | | | 2 | | 4.2m | 16,309 |
| `Qwen3.8 27B UD-IQ4 XS Unsloth Q8 Q8 52k` | 🥉 naive | 2 | | | 2 | | 5.3m | 14,709 |
| `Qwen3.8 27B UD-IQ4 XS Unsloth Q8Q8 48k` | 🥉 naive | 2 | | | 1 | 1 | 7.1m | 16,646 |
| `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP finex666 64k` | 🥉 naive | 2 | | | 1 | 1 | 5.0m | 17,864 |
| `Qwen3.8-27B-IQ4 XS byteshape 64k` | 🥉 naive | 2 | | | 2 | | 4.8m | 17,151 |
| *(no report in PR body — #24, #48)* | 🥉 naive | 2 | | | 2 | | — | — |
| `z-ai/glm-5.3` | 🥉 naive | 2 | | | 2 | | 0.6m | 9,182 |
| `North-Mini-Code-1.0-UD-IQ4 XS unsloth 64` | 🥉 naive | 1 | | | 1 | | 6.7m | 32,114 |
| `Qwen3.8 27B UD-IQ4 XS Unsloth 48k ngram` | 🥉 naive | 1 | | | 1 | | 4.1m | 13,281 |
| `Qwen3.8 27B UD-IQ4 XS Unsloth Q4 64k` | 🥉 naive | 1 | | | 1 | | 6.5m | 16,287 |
| `Qwen3.8-27B-ASCII-Condensed-IQ4 XS-3 troed 64k ngram` | 🥉 naive | 1 | | | 1 | | 2.8m | 14,136 |
| `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP finex666 Q4 56k` | 🥉 naive | 1 | | | 1 | | 2.3m | 12,851 |
| `Qwen3.8-27B-abliterated-UD-IQ4 XS huihui 64k` | 🥉 naive | 1 | | | 1 | | 4.3m | 11,608 |
| `Swift-1.5-Qwen3.8-27B-IQ4 XS ukisai` | 🥉 naive | 1 | | | 1 | | 6.0m | 11,099 |
| `Swift-1.5-Qwen3.8-27B-MiniPlus-V3 isvalorum` | 🥉 naive | 1 | | | 1 | | 3.8m | 15,007 |
| `Swift-1.5-Qwen3.8-27B-MiniPlus-V3 isvalorum 48k` | 🥉 naive | 1 | | | 1 | | 5.5m | 12,698 |
| `minimax-m2.7` | 🥉 naive | 1 | | | 1 | | 1.6m | 11,414 |
| `Gemma-4-26B-UD-IQ4 XS unsloth 104k` | ⛔ off-target | 1 | | | | 1 | 2.6m | 23,710 |
| `minimax/minimax-m3` | ⛔ off-target | 1 | | | | 1 | 13.2m | 27,484 |
## The four tiers

**🥇 Solved — 3 configurations, 5 runs.** Understood both traps: the constant must not reach the URL, and the feed's newest tag may have no release. All three scraped real asset names and looped over recent tags.

- `Swift-1.5-Qwen3.8-27B-GSQ-RCO-IQ3 S-mtp ukisai 64k` — #95 correct; #96 and #97 the naive 404. **Inconsistent**: the same configuration, in the same session shape, produced one good run and two bad ones.
- `Qwen3.8 27B UD-IQ4 XS Unsloth 40k Q8 ngram` — #25, 1/1.
- `Qwen3.8-27B UD-Q3-K-XL Unsloth Q4 80k` — #69, 1/1, and the only solution that avoids HTML scraping entirely: it HEAD-probes `...-cuda-12.N-x64.zip` for N = 9…0 and builds the URL from the answer.

**🥈 Near miss — 6 configurations, 35 runs.** These models *did* realise the filename must not be built from the constant — the harder half of the task — and scraped the release page for the real names. They then read `max(tags)` from the feed and gave up on the first tag that had nothing, so on the live feed they exit instead of falling back to `b12102`. Adding one loop around the scrape is all that separates this tier from the top one.

Nine of the twelve single-attempt runs come from `Swift-Qwen3.8-27B-IQ4 XS (ahmeddelkilami01) [64k]`, which produced a single-attempt scraper in 3 of its 12 runs and the naive 404 in the other 9 — the single least consistent configuration in the sample.

**🥉 Naive — 20 configurations, 34 runs.** `FALLBACK_CUDA = "12"` went straight into the download URL. Correct against the literal backlog text, worst possible outcome functionally: the fallback now fails exactly when the token removal makes it run. This is where 45 % of all runs land, and it contains the *smallest* diff (#77, 7 lines) and the *fastest* run (`z-ai/glm-5.3`, 0.6 min). Two of these runs broke something else instead: #42's scrape returns all 33 assets unfiltered, #50's drops the cudart zip.

**⛔ Off-target — 2 configurations, 2 runs.** `Gemma-4-26B` (#87) still reads `GITHUB_TOKEN`; `minimax/m3` (#88) deleted the no-API fallback altogether — the opposite of what removing the token requires.

## Effort correlates with success

| Outcome | Runs | Avg time | Avg tokens |
|---|--:|--:|--:|
| ✅ correct | 3 | 10.9 min | 28,946 |
| 🥈 near miss | 12 | 9.2 min | 20,049 |
| 🥉 404 URL | 56 | 4.2 min | 15,152 |
| ⛔ other / no fix | 5 | 8.6 min | 22,424 |

The three correct runs took **2.6× the wall-clock and 1.9× the tokens** of the naive ones. On this task speed is a symptom: the fast runs are the ones that took the requirement at face value, made the two-line edit and stopped. Total spend across the 74 reported runs: **1.26 M tokens**.

## Families

| Family | Runs | 🥇 | 🥈 | 🥉 | ⛔ |
|---|--:|--:|--:|--:|--:|
| `Qwen3.8-27B` (incl. abliterated, ASCII-Condensed, byteshape, ista, Unsloth) | 43 | 2 | 9 | 30 | 2 |
| `Swift-1.5-Qwen3.8-27B` | 13 | 1 | 0 | 12 | 0 |
| `Swift-Qwen3.8-27B` | 12 | 0 | 3 | 9 | 0 |
| `z-ai/glm-5.3` | 2 | 0 | 0 | 2 | 0 |
| `minimax` (m2.7, m3) | 2 | 0 | 0 | 1 | 1 |
| *(no report)* | 2 | 0 | 0 | 2 | 0 |
| `North-Mini-Code-1.0` | 1 | 0 | 0 | 1 | 0 |
| `Gemma-4-26B` | 1 | 0 | 0 | 0 | 1 |

No family solved the task; the three successes are spread across three different families. With 43 of 76 runs from one family, this sample says more about `Qwen3.8-27B` quantisations than about the other families — the two OpenRouter models and `Gemma-4-26B` contribute 5 runs between them, too few to rank.
## Caveats on the attribution

The model name comes from the PR's own execution report and **cannot be verified** — the repository records the agent shell, not the weights. Three checks on the sample:

1. **Two runs are unattributed.** #24 and #48 predate the execution-report template. They are counted, but in no model's row.
2. **76 runs contain only 71 distinct implementations.** Eight runs collapse into three groups of identical code across *different* model labels: `{#39, #77, #91, #97}`, `{#71, #85}`, `{#43, #81}`. #71 and #85 additionally carry the same commit timestamp — they look like one run delivered twice.
3. **All models in this sample but three are Qwen3.8-27B derivatives** driven by the same scaffolding and the same prompt. Converging on a mechanical two-line edit is expected, which is the benign explanation for (2).

Consequences: differences **between close variants of the same base model are noise**, not signal. Treat the tier a configuration lands in as meaningful and its exact run counts as indicative only.

## What the sample does say

- **Every model understood the requirement; almost none modelled the consequence.** The literal instruction is satisfied by 74 of 76 runs. The models that failed did not misread the backlog — they analysed the constant in isolation and never traced it into the URL.
- **The near-miss tier is the tell.** 12 runs knew the filename must be scraped, and 9 of them still made a single attempt against a feed whose newest entry may have no release. Understanding the trap is not the same as being robust to it.
- **Correct runs are not repeatable.** The only configuration with several runs — `GSQ-RCO-IQ3 S-mtp ukisai` — went 1 for 3. Nothing here supports ranking models by a single run.
- **Effort is not wasted.** The models that spent 2–3× the time and tokens on reading the fallback are the only ones that found the trap. On this evidence, time spent re-reading the code before editing is the single best predictor of success.

## Why #32 was the wrong pick

The first edition ranked PRs and selected #32 (28 lines). It is a *near miss* in the table above — its model read the release page and reused `pick_assets()`, but took a single attempt:

```python
tag = f"b{max(tags)}"          # single attempt
...
sys.exit(f"No Windows CUDA {CUDA_VERSION} llama.cpp asset found on the {tag} release page.")
```

Verified live on 2026-11-04: the call returns `No Windows CUDA 12 llama.cpp asset found on the b12109 release page.` and the script exits, while `b12102` is published with 38 assets. The first edition rated #32 ⭐⭐⭐⭐⭐ and its sibling #94 ⭐⭐⭐⭐ **for the same defect**, because both were checked with a `--dry-run` (API path only) or against a hand-picked good tag (a feed stub fixed to `b11060`). Neither check can ever reject a single-attempt fallback. #32 is now closed.

## Process lessons

- **76 PRs for a ~28-line change.** The cost was triage, not code. `AGENTS.md` already says "check open PRs before starting"; the step was skipped 76 times.
- **The backlog entry was under-specified.** It should read: *"and keep the no-API fallback able to download a real file, even when the newest feed tag has no release yet"*. Stating the requirement is cheaper than 76 attempts at guessing it.
- **A single-attempt fallback is a fallback that fails on its newest input.** The API path already loops over releases and skips empty ones; the feed fallback must do the same.
- **Verification must feed the fallback its real input.** A `--dry-run` never reaches the fallback and a hand-picked good tag cannot fail it. A stub of *tag-without-release followed by a good release* would have rejected 12 runs and the first edition's choice.
- **No test covers the fallback.** A ~10-line test (stub `http_open`, assert the resolved asset name exists) would have rejected 68 of the 76 runs before they were opened. That belongs with Features 13/16 (CI for scripts), and it must include the tag-without-release case.
## Appendix A — run by run

Model string as reported by each PR's `AI agent execution report` (`Llama.cpp/` prefix stripped; `#24`/`#48` have no report).

| # | Model | Outcome |
|---:|---|---|
| #22 | `z-ai/glm-5.3` | ❌ 404 URL |
| #23 | `Swift-1.5-Qwen3.8-27B-IQ4_XS_ukisai.gguf` | ❌ 404 URL |
| #24 | *(no report)* | ❌ 404 URL |
| #25 | `Qwen3.8_27B_UD-IQ4_XS_(Unsloth)_[40k]_Q8_ngram` | ✅ correct |
| #26 | `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]` | ❌ 404 URL |
| #27 | `Qwen3.8-27B-abliterated-UD-IQ4_XS_huihui_64k` | ❌ 404 URL |
| #28 | `Swift-Qwen3.8-27B-IQ4_XS_(ahmeddelkilami01)_64k` | ❌ 404 URL |
| #29 | `Qwen3.8-27B-IQ4_XS_byteshape_56k` | ❌ 404 URL |
| #30 | `Qwen3.8-27B-IQ4_XS_byteshape_56k` | 🥈 single-tag |
| #31 | `North-Mini-Code-1.0-UD-IQ4_XS_(unsloth)_64` | ❌ 404 URL |
| #32 | `Swift-Qwen3.8-27B-IQ4_XS_(ahmeddelkilami01)_[64k]` | 🥈 single-tag |
| #33 | `Swift-Qwen3.8-27B-IQ4_XS_(ahmeddelkilami01)_[64k]` | 🥈 single-tag |
| #34 | `Swift-1.5-Qwen3.8-27B-Uncensored-VAL-APEX-I-MiniPlus-V3_isvalorum.gguf` | ❌ 404 URL |
| #35 | `Qwen3.8-27B-IQ4_XS_(byteshape)_[64k]` | ❌ 404 URL |
| #36 | `Swift-1.5-Qwen3.8-27B-Uncensored-VAL-APEX-I-MiniPlus-V3_isvalorum.gguf` | ❌ 404 URL |
| #37 | `Swift-1.5-Qwen3.8-27B-Uncensored-VAL-APEX-I-MiniPlus-V3_isvalorum.gguf` | ❌ 404 URL |
| #38 | `Swift-1.5-Qwen3.8-27B-Uncensored-VAL-APEX-I-MiniPlus-V3_isvalorum.gguf` | ❌ 404 URL |
| #39 | `Qwen3.8-27B-IQ4_XS_(byteshape)_[56k]` | ❌ 404 URL |
| #40 | `Qwen3.8-27B-IQ4_XS_(byteshape)_[56k]` | ❌ 404 URL |
| #41 | `Qwen3.8-27B-IQ4_XS_(byteshape)_[56k]` | 🥈 single-tag |
| #42 | `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]` | ⚠️ scrape returns all 33 assets |
| #43 | `Swift-1.5-Qwen3.8-27B-MiniPlus-V3_isvalorum.gguf` | ❌ 404 URL |
| #44 | `Swift-1.5-Qwen3.8-27B-MiniPlus-V3_[isvalorum]_(48k)` | ❌ 404 URL |
| #45 | `Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_Q4_[64k]` | ❌ 404 URL |
| #46 | `Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[48k]_Q8` | 🥈 single-tag |
| #47 | `Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[48k]_ngram` | ❌ 404 URL |
| #48 | *(no report)* | ❌ 404 URL |
| #49 | `Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_Q8Q8_[48k]` | ❌ 404 URL |
| #50 | `Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_Q8Q8_[48k]` | ⚠️ cudart zip dropped |
| #51 | `Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[56k]` | 🥈 single-tag |
| #52 | `Swift-Qwen3.8-27B-IQ4_XS_[ahmeddelkilami01]_[64k]` | ❌ 404 URL |
| #53 | `Swift-Qwen3.8-27B-IQ4_XS_[ahmeddelkilami01]_[64k]` | ❌ 404 URL |
| #54 | `Swift-Qwen3.8-27B-IQ4_XS_[ahmeddelkilami01]_[64k]` | ❌ 404 URL |
| #55 | `Swift-Qwen3.8-27B-IQ4_XS_[ahmeddelkilami01]_[64k]` | 🥈 single-tag |
| #56 | `Swift-Qwen3.8-27B-IQ4_XS_[ahmeddelkilami01]_[64k]` | ❌ 404 URL |
| #57 | `Swift-Qwen3.8-27B-IQ4_XS_[ahmeddelkilami01]_[64k]` | ❌ 404 URL |
| #58 | `Swift-Qwen3.8-27B-IQ4_XS_[ahmeddelkilami01]_[64k]` | ❌ 404 URL |
| #59 | `Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[56k]` | ⛔ still uses `GITHUB_TOKEN` |
| #60 | `Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[56k]` | 🥈 single-tag |
| #61 | `Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[56k]` | ❌ 404 URL |
| #62 | `Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_Q8_Q8_[52k]` | ❌ 404 URL |
| #63 | `Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_Q8_Q8_[52k]` | ❌ 404 URL |
| #64 | `Qwen3.8-27B-IQ3_S_[ista]_[64k]` | ❌ 404 URL |
| #65 | `Qwen3.8-27B-IQ3_S_[ista]_[64k]` | ❌ 404 URL |
| #66 | `Qwen3.8-27B-IQ3_S_[ista]_[64k]` | ❌ 404 URL |
| #67 | `Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[56k]` | ❌ 404 URL |
| #68 | `Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[56k]` | ❌ 404 URL |
| #69 | `Qwen3.8-27B_UD-Q3-K-XL_[Unsloth]_Q4_[80k]` | ✅ correct |
| #70 | `Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[52k]` | ❌ 404 URL |
| #71 | `Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[52k]` | 🥈 single-tag |
| #72 | `Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[52k]` | ❌ 404 URL |
| #73 | `Swift-Qwen3.8-27B-IQ4_XS_[ahmeddelkilami01]_[64k]` | ❌ 404 URL |
| #74 | `Swift-Qwen3.8-27B-IQ4_XS_[ahmeddelkilami01]_[64k]` | ❌ 404 URL |
| #75 | `Swift-1.5-Qwen3.8-27b.i1-IQ4_XS_[mrradermacher]_Q4_[48k]` | ❌ 404 URL |
| #76 | `Swift-1.5-Qwen3.8-27b.i1-IQ4_XS_[mrradermacher]_Q4_[48k]` | ❌ 404 URL |
| #77 | `Swift-1.5-Qwen3.8-27b.i1-IQ4_XS_[mrradermacher]_Q4_[48k]` | ❌ 404 URL |
| #78 | `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_[finex666]_Q4_[56k]` | ❌ 404 URL |
| #79 | `Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[52k]` | 🥈 single-tag |
| #80 | `Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[52k]_ngram` | ❌ 404 URL |
| #81 | `Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[52k]_ngram` | ❌ 404 URL |
| #82 | `Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[52k]` | ❌ 404 URL |
| #83 | `Qwen3.8-27B-ASCII-Condensed-IQ4_XS-3_[troed]_[64k]_ngram` | ❌ 404 URL |
| #84 | `Qwen3.8-27B-IQ3_S_[ista]_[64k]` | ❌ 404 URL |
| #85 | `Qwen3.8-27B-IQ3_S_[ista]_[64k]` | 🥈 single-tag |
| #86 | `Openrouter minimax /minimax-m2.7` | ❌ 404 URL |
| #87 | `Gemma-4-26B-UD-IQ4_XS_(unsloth)_104k` | ⛔ still uses `GITHUB_TOKEN` |
| #88 | `Openrouter/minimax/minimax-m3` | ⛔ deletes the fallback |
| #89 | `Openrouter/z-ai/glm-5.3` | ❌ 404 URL |
| #90 | `Qwen3.8-27B-IQ3_S_[ista]_[64k]_ngram` | ❌ 404 URL |
| #91 | `Qwen3.8-27B-IQ3_S_[ista]_[64k]_ngram` | ❌ 404 URL |
| #92 | `Qwen3.8-27B-IQ3_S_[ista]_[64k]_ngram` | ❌ 404 URL |
| #93 | `Qwen3.8-27B-IQ4_XS_(byteshape)_[64k]` | ❌ 404 URL |
| #94 | `Qwen3.8-27B-IQ4_XS_[byteshape]_[56k]` | 🥈 single-tag |
| #95 | `Swift-1.5-Qwen3.8-27B-GSQ-RCO-IQ3_S-mtp_[ukisai]_[64k]` | ✅ correct |
| #96 | `Swift-1.5-Qwen3.8-27B-GSQ-RCO-IQ3_S-mtp_[ukisai]_[64k]` | ❌ 404 URL |
| #97 | `Swift-1.5-Qwen3.8-27B-GSQ-RCO-IQ3_S-mtp_[ukisai]_[64k]` | ❌ 404 URL |

## Appendix B — reproduce the checks

```bash
# 1. real asset names carry the CUDA minor — no "-cuda-12-x64.zip" exists
curl -sL https://github.com/ggml-org/llama.cpp/releases/tag/b11060 \
  | grep -o 'releases/download/b11060/[^"]*cuda-12[^"]*x64\.zip' | sort -u

# 2. the newest feed tag is often a tag without a release
curl -s https://github.com/ggml-org/llama.cpp/releases.atom \
  | grep -o '/releases/tag/b[0-9]*' | grep -o 'b[0-9]*' | sort -u | tail -3   # -> b12109 ...
curl -s -o /dev/null -w '%{http_code}\n' \
  https://api.github.com/repos/ggml-org/llama.cpp/releases/tags/b12109        # -> 404 (tag only)

# 3. replay the no-API fallback against the LIVE feed (its real input)
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

# 4. re-extract the scoreboard data (model / time / tokens per PR)
gh pr list --state all --limit 300 --json number,body > /tmp/all_prs.json
```
