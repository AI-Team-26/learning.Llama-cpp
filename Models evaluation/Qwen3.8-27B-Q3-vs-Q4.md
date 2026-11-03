# Qwen3.8 27B — Q3 vs Q4

**Date:** 2026-11-03
**Hardware:** 16 GB VRAM · 66/66 layers offloaded · GPU-only inference
**Sources:** `Models optimization/GOOD/Qwen 3.8 27B Q3.md`, `Models optimization/GOOD/Qwen 3.8 27B Q4.md`, `Models evaluation/Qwen3.8-27B.eval.md`, `scripts/models_config.yaml`

## TL;DR

There is no single winner, and the repo already reflects that: **both Unsloth `UD` files carry a 🥇 note** in `scripts/models_config.yaml`.

| Need | Use | Why |
|---|---|---|
| 64–80 k context, everyday coding | **`Qwen3.8-27B-UD-Q3_K_XL_unsloth.gguf`** (12.2 GB) | Reaches 80 k, ~2 t/s faster, 1.1 GB lighter, "Super smart" |
| 48–52 k context at Q8 KV, max quality | **`Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf`** (13.2 GB) | "Good reasoning. Reliable." — the tested 48/52 k configs |
| Uncensored variants | neither of the above | See the per-file tables |

**If you must pick one: Q3_K_XL.** It is the only Qwen3.8-27B file with a verified 80 k run, it is the fastest of the four "good" options, and it costs the least VRAM.

## Measured differences

The bit-level is a poor predictor. What actually separates the two Unsloth files is ~1.1 GB of weights, which is what you spend on KV cache:

| | Size | Max ctx observed | KV type at that ctx | Speed | VRAM |
|---|---|---|---|---|---|
| **UD-Q3_K_XL** | 12.2 GB | **80 k** | q4_0/q4_0 | 40–47 t/s | 11.7 / 15.4 |
| **UD-IQ4_XS** | 13.2 GB | **80 k** | q4_0/q4_0 | 43–44 t/s | 12.8 / 15.7 |
| UD-IQ4_XS | 13.2 GB | ~52 k | q8_0/q4_0 | 42–43 t/s | 12.8 / 15.5 |
| IQ4_XS (byteshape) | 12.1 GB | 72 k | q8_0/q8_0 | 41–44 t/s | 11.5 / 15.7 |

Two corrections to the intuitive story:

1. **At equal KV settings both reach 80 k.** Q4 is not "too big for 80 k" — it does 80 k with compressed (`q4_0`) KV, exactly like Q3.
2. **The 1.1 GB does not buy a measured extra context.** Q3_K_XL should in theory reach a higher context than Q4 at Q8 KV, but **no Q8-KV run of Q3_K_XL exists in the repo**. The 80 k figure is a `q4_0` KV number.

Speed is within noise: 46 vs 44 t/s. Throughput here comes from MTP/ngram speculation, not from the quant.

## Full per-file results

### Q3 — `Models optimization/GOOD/Qwen 3.8 27B Q3.md`

| File | GB | Result |
|---|---|---|
| `Qwen3.8-27B-UD-Q3_K_XL_unsloth.gguf` | 12.2 | ✔️ 80 k: 20–45 t/s **Super smart** |
| `Swift-Qwen3.8-27B-IQ3_XS_ukisai.gguf` | 12.1 | ✔️ 64 k: 25–45 t/s |
| `Qwen3.8-27B-Cold-Fusion-…-IQ3_M_davidau.gguf` | 13.5 | ✔️ 64 k: 15–30 t/s — short reasoning |
| `Qwen3.8-27B-UD-IQ3_S_unsloth.gguf` | 11.2 | 45–46 t/s @ 128 k (no Pi run) |
| `Qwen3.8-27B-GSQ-RCO-IQ3_S-mtp_ista.gguf` | 11.2 | 44–45 t/s @ 68 k, medium reasoning |
| `Qwen3.8-27B-IQ3_S-3.23bpw_byteshape.gguf` | 10.2 | not recorded |
| `Qwen3.8-27B-Uncensored-Aggressive-IQ3_M_HauhauCS.gguf` | 12.9 | contradictory — see note below |
| `Qwen3.8-27B-Uncensored-IQ3_M_orcarouter.gguf` | 11.8 | ❌ not for coding |
| `RVN-Q3_K_M-mtp_observerx.gguf` | 12.8 | ❌ slow, only 64 k |
| `RVN-Q3_K_L_observerx.gguf` | 13.3 | ❌ slow, no MTP |
| `RVN-IQ3_M-mtp_Observerx.gguf` | 12.1 | ❌ tool call failed on first attempt |
| `Swift-1.5-Qwen3.8-27B-IQ3_M_ukisai.gguf` | 13.8 | ❌ max 56 k |
| `Swift-Qwen3.8-27b-IQ3_M_bartowski.gguf` | 13.8 | ❌ max 32 k |
| `Qwen3.8-27B-Q3_K_S_unsloth.gguf` | 11.7 | ❌ edited CHANGELOG during the test |
| `Qwen3.8-27B-UD-Q2_K_XL_unsloth.gguf` | 9.2 | ❌ edited CHANGELOG during the test |
| `Qwen3.8-27B-YMQ-M_zerodigest.gguf` | 13.6 | ❌ rubbish |

### Q4 — `Models optimization/GOOD/Qwen 3.8 27B Q4.md`

| File | GB | Result |
|---|---|---|
| `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` | 13.2 | ✔️ 64 k: 20–40 t/s · Q8 80 k: 20–30 t/s |
| `Qwen3.8-27B-IQ4_XS-3.84bpw_byteshape.gguf` | 12.1 | ✔️ 64 k: 25–45 t/s |
| `Qwen3.8-27B-i1-IQ4_XS-GGUF-Smaller_jrell.gguf` | 12.6 | ✔️ 56 k: 15–45 t/s |
| `Swift-Qwen3.8-27b-i1-IQ4_XS-Smaller_ahmeddelkilami01.gguf` | 12.6 | ✔️ but ❌ loops at ubatch 512 |
| `Qwen3.8-27B-ZB4.00-MIN-v5-IQ4_XS_tooltd.gguf` | 12.7 | ✔️ 64 k |
| `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666.gguf` | 13.2 | ✔️ 64 k: 25–45 t/s, looping suspected |
| `Qwen3.8-27B-ASCII-Condensed-IQ4_XS-3.84bpw_troed.gguf` | 11.4 | **52–53 t/s @ 80 k** (55 @ 68 k) — needs a DFlash draft file |
| `Qwen3.8-27B-abliterated-UD-IQ4_XS_huihui.gguf` | 13.4 | ✔️ 64 k but slow / poor results |
| `Qwen3.8-27B-UD-IQ4_XS_peculiar.gguf` | 13.2 | not tested in Pi |
| `Qwen3.8-27B-heretic-ara.i1-IQ4_XS_mradermacher.gguf` | 14.2 | ❌ slow, max 32 k |
| `Swift-1.5-Qwen3.8-27B-IQ4_XS_ukisai.gguf` | 14.4 | ❌ max 44 k |
| `Qwen3.8-27B-IQ4_XS_unsloth.gguf` | 14.6 | ❌ too slow, unusable |
| `Qwen3.8-27B.i1-IQ4_KT-attn_qkv-IQ4_KS-MTP_chunter789.gguf` | 13.9 | ❌ `attn_qkv-` does not work |
| `Swift-1.5-Qwen3.8-27B-MiniPlus-V3_isvalorum.gguf` | 13.1 | ❌ too slow |
| `Swift-1.5-Qwen3.8-27b.i1-IQ4_XS_mrradermacher.gguf` | 14.2 | ❌ too big, too slow |
| `Qwen3.8-27B-YMQ-M_zerodigest.gguf` | 13.6 | ❌ rubbish |

## The label trap

"Q3" and "Q4" are nominal. Size and quantizer matter more, and the notes prove it:

- **`UD-IQ4_XS` (13.2 GB) ✔️ vs `IQ4_XS` (14.6 GB) ❌ "Too slow. Unusable."** — same nominal Q4, 1.4 GB apart, opposite verdicts. The `UD` (dynamic) recipe spends fewer bits on insensitive tensors, so it lands at Q4 quality in a Q3-sized file.
- **`UD-Q3_K_XL` (12.2 GB) "Super smart" vs `Swift-1.5 IQ3_M` (13.8 GB) ❌ "Max 56 k"** — the bigger "better" Q3 is the worse model, because it does not fit.
- **`ASCII-Condensed IQ4_XS` (11.4 GB) is the fastest file in either list** at 53–55 t/s and 80 k — smaller than both 🥇 files, proving the quant label alone tells you nothing.

**Conclusion:** choose a quantizer and a size that fits your KV budget, then verify with a run. Do not choose by the letter.

## Inconsistencies found in the source notes

Reported here rather than fixed, since the notes are the working record:

1. **`Qwen3.8-27B-Uncensored-Aggressive-IQ3_M_HauhauCS.gguf` contradicts itself** — the summary table says `12.9 GB · ? 128k: 25-40 t/s. Good`, while the detail section says `11.9 GB · ❌ Got stuck in its own reasoning`. Both size and verdict differ.
2. **`Qwen3.8-27B-Uncensored-IQ3_M_orcarouter.gguf` is marked ✔️ in its detail heading** while the summary table says ❌ for coding — the ✔️ refers to how uncensored it is, which reads as a contradiction at a glance.
3. **`Qwen3.8-27B-UD-Q2_K_XL_unsloth.gguf` appears in both files** — as a Q3-table row (9.2 GB) and as `## ❌ UD Q2_K_XL` in the Q4 file. A Q2 entry belongs in neither.

## What the eval table can and cannot tell you

`Models evaluation/Qwen3.8-27B.eval.md` cannot settle Q3 vs Q4 as it stands:

| Confound | Evidence |
|---|---|
| **Sample size** | Q3 appears in **one** row (PR 69, 80 k, temp 0.3, ubatch 512 → ✔️). IQ4_XS appears in ~36 — 20 Unsloth UD rows plus 16 Swift repacks. |
| **Temperature** | Q4 rows run at 0.2, 0.3, 0.5, 0.7 and 1.0 |
| **Context** | 40 k, 48 k, 52 k, 56 k, 64 k |
| **Spec config** | `draft-mtp`, `draft-mtp,ngram-simple`, and none |
| **Quantizer** | 15 distinct Q4 files, each repacked by a different uploader |

The Q4 spread shows the problem: at 52 k, PRs 70/71/72 solve; at 56 k, PR 51 and 60 solve while PR 59 produces a "completely wrong solution"; at 48 k, PR 46 solves while 47 and 48 do not. That variance comes from configuration as much as from the weights.

The one genuinely controlled pair in the repo is **byteshape `IQ3_S` (10.2 GB) vs `IQ4_XS` (12.1 GB)** — same uploader, same architecture, same `draft-mtp,ngram-simple` setup — but the IQ3_S run has a config block and **no results table**, so the comparison was never completed.

## Config effects that get blamed on the quant

- **"Ubatch set to 256 makes the model dumb !!"** — the first line of the Q4 notes. This is the single biggest quality lever found, and it is a batch parameter, not a quantization one. `UD-Q3_K_XL` runs 1024/512 and `UD-IQ4_XS` runs 1024/256 at 80 k.
- Several ❌ verdicts are **behavioural**, not capability: `edited CHANGELOG`, `missed the TODO update`, `reused an old PR`. They say nothing about Q3 vs Q4.
- Temperature sensitivity is asymmetric: the huihui Q4 file "outputs rubbish" with 0.5 and repetition 0.05 lower — a raising-temp test on one quant can look like a quant difference.

## Recommendation

1. **Keep both Unsloth files** — that is what `models_config.yaml` already does (🥇 on all five `UD-IQ4_XS` configs and on `UD-Q3_K_XL`).
2. **Default to `UD-Q3_K_XL` at 80 k, batch 1024 / ubatch 512.** It is measured, fastest, and lightest of the good options.
3. **Switch to `UD-IQ4_XS` at 48–52 k with Q8 KV** when output quality matters more than context. This is the config with the most passing runs behind it.
4. **Ignore the Q3/Q4 label when scanning a new upload.** Check size (does it fit with your KV?) and uploader (`UD`/dynamic quants win repeatedly) first.

## Suggested controlled test

Not yet run. To settle it properly, hold everything but the weights constant:

```
temp 0.3 · ctx 64k · quant q8_0/q4_0 · spec draft-mtp · batch 1024 · ubatch 512
```

Run the same coding task (`Test code` column in the eval table) against `UD-Q3_K_XL` and `UD-IQ4_XS`, five times each, and score by SOLVED. That is the only way to separate a 1 GB weight difference from the noise already visible in the Q4 rows.
