# Qwen 3.8 27B - Q3

| File                                                       | GB   | Result                                               |
|------------------------------------------------------------| ---- |------------------------------------------------------|
| Qwen3.8-27B-Uncensored-Aggressive-IQ3_M_HauhauCS.gguf      | 12.9 | ? 128k: 25-40 t/s. Good                              |
| Qwen3.8-27B-UD-Q3_K_XL_unsloth.gguf                        | 12.2 | ✔️ 80k: 20-45 t/s Super smart.                       |
| Swift-Qwen3.8-27B-IQ3_XS_ukisai.gguf                       | 12.1 | ✔️ 64k: 25-45 t/s ✔️ Good PR ❌ Miss update TODO.   |
| Qwen3.8-27B-Cold-Fusion-GAIN-V1.1-MTP-IQ3_M_davidau.gguf   | 13.5 | ✔️ 64k: 15-30 t/s Short reasoning                    |
| Qwen3.8-27B-Uncensored-IQ3_M_orcarouter.gguf               | 11.8 | ❌ Not for coding. ✔️ Really uncensored             |
| RVN-Q3_K_M-mtp_observerx.gguf                              | 12.8 | ❌ Slow. It seems to manage only 64k ?!              |
| RVN-Q3_K_L_observerx.gguf                                  | 13.3 | ❌ Slow. No MTP.                            |
| Swift-1.5-Qwen3.8-27B-GSQ-RCO-IQ3_S-mtp_ukisai.gguf        |      |                                             |
| Qwen3.8-27B-UD-Q2_K_XL_unsloth.gguf                        |  9.2 | ❌ It changed the CHANGELOG for the test PR |
| Qwen3.8-27B-Q3_K_S_unsloth.gguf                            | 11.7 | ❌ It changed the CHANGELOG for the test PR |
| Qwen3.8-27B-UD-IQ3_S_unsloth.gguf                          | 11.2 |                            |
| Swift-1.5-Qwen3.8-27B-IQ3_M_ukisai.gguf                    | 13.8 | ❌ Max 56K.                                 |
| Swift-Qwen3.8-27b-IQ3_M_bartowski.gguf                     | 13.8 | ❌ Max 32K.                                 |
| RVN-IQ3_M-mtp_Observerx.gguf                               | 12.1 | ❌ Tool call failed on first attempt        |
| Qwen3.8-27B-YMQ-M_zerodigest.gguf                          | 13.6 | ❌ Rubbish                                  |
| Qwen3.8-27B-IQ3_S-3.23bpw_byteshape.gguf                   | 10.2 |      |
| Qwen3.8-27B-GSQ-RCO-IQ3_S-mtp_ista.gguf                    | 11.2 |      |



# ✔️ GSQ-RCO-IQ3_S (ISTA-DASLAb)
Qwen3.8-27B-GSQ-RCO-IQ3_S-mtp_ista.gguf                         11.2 GB
https://huggingface.co/ISTA-DASLab/Qwen3.8-27B-GSQ-RCO-GGUF
mmproj:

# ✔️ IQ3_S (byteshape)
Qwen3.8-27B-IQ3_S-3.23bpw_byteshape.gguf                        10.2 GB
https://huggingface.co/byteshape/Qwen3.8-27B-GGUF

##
Swift-1.5-Qwen3.8-27B-GSQ-RCO-IQ3_S-mtp_ukisai.gguf              11.2 GB

## ✔️ Swift IQ3_XS (ukisai)                            
Swift-Qwen3.8-27B-IQ3_XS_ukisai.gguf                             12.1 GB
https://huggingface.co/ukisai/Swift-Qwen3.8-27B-GGUF

## ✔️ Uncensored-IQ3_M (orcarouter)
Qwen3.8-27B-Uncensored-IQ3_M_orcarouter.gguf
It answers questions about politics and sex.

## ❌ Swift 1.5 IQ3_M (ukisai)  [DELETED]
Swift-1.5-Qwen3.8-27B-IQ3_M_ukisai.gguf                          13.8 GB
Too big. max 56k
XX Produced rubbish instead of a good MD table at Q4_0

## Q3_K_S (Unsloth)
Qwen3.8-27B-Q3_K_S_unsloth.gguf                                   11.7 GB
https://huggingface.co/unsloth/Qwen3.8-27B-GGUF

## ✔️ IQ3_M (DavidAU)
Qwen3.8-27B-Cold-Fusion-GAIN-V1.1-MTP-IQ3_M_davidau.gguf          13.5 GB
https://huggingface.co/DavidAU/Qwen3.8-27B-Cold-Fusion-GAIN-V1.1-NM-DAU-NEO-MAX-MTP-GGUF

## ✔️ UD Q3_K_XL (Unsloth)
Qwen3.8-27B-UD-Q3_K_XL_unsloth.gguf                               12.2 GB

## ✔️ Q3_K_M (Observerx)
RVN-Q3_K_M-mtp_observerx.gguf                                     12.8 GB

## ❌ Ukisai Swift IQ3_M (bartowski)
Swift-Qwen3.8-27b-IQ3_M_bartowski.gguf
Max 32k (Q4 52k).

## ❌ IQ3_M (Observerx)
RVN-IQ3_M-mtp_Observerx.gguf                                       12.1 GB
OpenAI tools compatibility    : ❌

## ❌ IQ3_M  (HauhauCS)
Qwen3.8-27B-Uncensored-Aggressive-IQ3_M_HauhauCS.gguf                11.9 GB 
https://huggingface.co/HauhauCS/Qwen3.8-27B-Uncensored-HauhauCS-Aggressive-MTP-GGUF
❌ Got stuck in its own reasoning

## ❌ Heretic Obliterated no-MTP (Observerx)
RVN-Q3_K_L_observerx.gguf                                   13.3 GB
https://huggingface.co/0bserverx/Qwen3.8-27B-Heretic-Abliterated-Uncensored-GGUF
MTP: NO   Max 64k, max 16 t/s

## ❌ YMQ-M (zerodigest)
Qwen3.8-27B-YMQ-M_zerodigest.gguf                            13.6 GB
https://huggingface.co/zerodigest/Qwen3.8-27B-YMQ-MTP-GGUF
20 t/s. Not pass simple test (more than 2000 tokens)

## ❌ UD Q2_K_XL (Unsloth)
Qwen3.8-27B-UD-Q2_K_XL_unsloth.gguf
It changed the CHANGELOG for the test PR


```bash

model=Qwen3.8-27B-GSQ-RCO-IQ3_S-mtp_ista.gguf
ctx_k=68
gpu_layers=99
cpu_moe=0
quant=q8_0/q8_0
spec=draft-mtp,ngram-simple
draft_model=none
predict_token=1/4
ngram_values=24/24/1
jinja=1
batch=1024
ubatch=512
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                       | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | -------------------------------------------- | --------- |------------------ |
|  44 t/s |  68 k |   0 | 66/66 | 15.2 | 10.9/0.1  | q8_0 (q8_0) |    551 |  12s | 89% = MTP 1/4 (95%) N-gram 24/24/1 (---)     |  1024/512 | R: medium         |

|  45 t/s |  68 k |   0 | 66/66 | 15.3 | 10.9/0.1  | q8_0 (q4_0) |    551 |  12s | 85% = MTP 1/5 (95%) N-gram 24/24/1 (---)     |  1024/512 | R: medium         |
|  44 t/s |  68 k |   0 | 66/66 | 15.2 | 10.9/0.1  | q8_0 (q4_0) |    551 |  12s | 89% = MTP 1/4 (95%) N-gram 32/24/1 (---)     |  1024/512 | R: medium         |
|  43 t/s |  64 k |   0 | 66/66 | 15.0 | 10.9/0.1  | q8_0 (q4_0) |    551 |  13s | 89% = MTP 1/4 (95%) N-gram 24/24/1 (---)     |  1024/512 | R: medium         |
|  43 t/s |  60 k |   0 | 66/66 | 14.8 | 10.9/0.1  | q8_0 (q4_0) |    551 |  13s | 89% = MTP 1/4 (95%) N-gram 32/24/1 (---)     |  1024/512 | R: medium         |




model=Qwen3.8-27B-IQ3_S-3.23bpw_byteshape.gguf
ctx_k=64
gpu_layers=99
cpu_moe=0
quant=q8_0/q8_0
spec=draft-mtp
draft_model=none
predict_token=1/4
ngram_values=none
jinja=1
batch=1024
ubatch=512
_test_model

model=Qwen3.8-27B-IQ4_XS-3.84bpw_byteshape.gguf
ctx_k=72
gpu_layers=99
cpu_moe=0
quant=q8_0/q8_0
spec=draft-mtp,ngram-simple
draft_model=none
predict_token=1/6
ngram_values=32/24
jinja=1
batch=512
ubatch=256
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                       | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | -------------------------------------------- | --------- |------------------ |
|  43 t/s |  72 k |   0 | 66/66 | 15.7 | 11.5/0.0  | q8_0 (q8_0) |    601 |  14s | 76% = MTP 1/6 (95%) N-gram 24/24/1 (---)     |   512/256 | R: medium         |
|  42 t/s |  72 k |   0 | 66/66 | 15.7 | 11.5/0.0  | q8_0 (q8_0) |    601 |  14s | 76% = MTP 1/6 (95%) N-gram 24/24/1 (---)     |  1024/256 | R: medium         |
|  43 t/s |  72 k |   0 | 66/66 | 15.6 | 11.5/0.0  | q8_0 (q4_0) |    601 |  14s | 87% = MTP 1/4 (96%)                          |   512/256 | R: medium         |
|  37 t/s |  72 k |   0 | 66/66 | 15.7 | 11.5/0.1  | q8_0 (q4_0) |    563 |  15s | 87% = MTP 1/4 (96%)                          |   512/512 | R: medium         |
|  36 t/s |  72 k |   0 | 66/66 | 15.7 | 11.5/0.1  | q8_0 (q8_0) |    563 |  15s | 90% = MTP 1/4 (96%)                          |  1024/512 | R: medium         |
|  35 t/s |  72 k |   0 | 66/66 | 15.7 | 11.5/0.1  | q8_0 (q4_0) |    563 |  16s | 87% = MTP 1/4 (94%)                          |  1024/512 | R: medium         |
|  32 t/s |  72 k |   0 | 66/66 | 15.4 | 11.5/0.0  | q8_0 (q4_0) |    601 |  19s | 87% = MTP 1/2 (97%) N-gram 16/16/1 (100%)    |  1024/256 | R: medium         |

|  44 t/s |  68 k |   0 | 66/66 | 15.7 | 11.5/0.1  | q8_0 (q8_0) |    563 |  12s | 90% = MTP 1/4 (96%)                          |  1024/512 | R: medium         |

|  43 t/s |  64 k |   0 | 66/66 | 15.4 | 11.5/0.0  | q8_0 (q4_0) |    601 |  14s | 84% = MTP 1/5 (96%) N-gram 24/24/1 (---)     |  1024/256 | R: medium         |
|  42 t/s |  64 k |   0 | 66/66 | 15.3 | 11.5/0.0  | q8_0 (q4_0) |    601 |  15s | 87% = MTP 1/4 (96%) N-gram 24/24/1 (---)     |  1024/256 | R: medium         |
|  40 t/s |  64 k |   0 | 66/66 | 15.7 | 11.5/0.0  | q8_0 (q8_0) |    601 |  15s | 72% = MTP 1/6 (95%) N-gram 16/16/1 (100%)    |  1024/256 | R: medium         |
|  40 t/s |  68 k |   0 | 66/66 | 15.5 | 11.5/0.0  | q8_0 (q4_0) |    601 |  15s | 83% = MTP 1/4 (96%) N-gram 16/16/1 (100%)    |  1024/256 | R: medium         |
|  38 t/s |  68 k |   0 | 66/66 | 15.6 | 11.5/0.0  | q8_0 (q4_0) |    583 |  16s | 74% = MTP 1/5 (95%) N-gram 16/16/1 (100%)    |  1024/256 | R: medium         |


|  44 t/s |  64 k |   0 | 66/66 | 15.7 | 11.5/0.1  | q8_0 (q8_0) |    563 |  13s | 85% = MTP 1/5 (94%)                          |  1024/512 | R: medium         |
|  43 t/s |  64 k |   0 | 66/66 | 15.3 | 11.5/0.0  | q8_0 (q4_0) |    601 |  14s | 87% = MTP 1/4 (96%)                          |   512/256 | R: medium         |
|  43 t/s |  64 k |   0 | 66/66 | 15.5 | 11.5/0.1  | q8_0 (q8_0) |    563 |  13s | 90% = MTP 1/4 (96%)                          |  1024/512 | R: medium         |
|  43 t/s |  64 k |   0 | 66/66 | 15.6 | 11.5/0.1  | q8_0 (q4_0) |    563 |  13s | 84% = MTP 1/5 (94%)                          |  1024/512 | R: medium         |
|  39 t/s |  64 k |   0 | 66/66 | 15.7 | 11.5/0.1  | q8_0 (q8_0) |    563 |  15s | 80% = MTP 1/6 (95%)                          |  1024/512 | R: medium         |

|  18 t/s |  64 k |   0 | 66/66 | 14.1 | 11.4/0.0  | q8_0 (none) |    537 |  30s | --                                           |  1024/256 | R: medium         |


model=Swift-Qwen3.8-27b-IQ3_M_bartowski.gguf
ctx_k=36
gpu_layers=99
cpu_moe=0
quant=q4_0
spec=draft-mtp
draft_model=none
predict_token=1/4
ngram_values=12/8
jinja=0
batch=1024
ubatch=512
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |------------------ |
|  35 t/s |  32 k |   0 | 66/66 | 15.7 | 13.2/0.1  | q8_0 (q8_0) |    511 |  15s | MTP        min=1 max=4 p_min=0.20 (94%) | 1024/1024 |                   |

|  42 t/s |  36 k |   0 | 66/66 | 15.3 | 13.2/0.1  | q4_0 (q4_0) |    528 |  12s | MTP        min=1 max=4 p_min=0.20 (90%) |  1024/512 |                   |

|  43 t/s |  32 k |   0 | 66/66 | 15.7 | 13.2/0.1  | q8_0 (q4_0) |    533 |  12s | MTP        min=1 max=4 p_min=0.20 (92%) |  1024/512 |                   |
|  38 t/s |  32 k |   0 | 66/66 | 15.5 | 13.2/0.1  | q8_0 (q4_0) |    533 |  14s | MTP        min=1 max=3 p_min=0.20 (93%) |  1024/512 |                   |
|  36 t/s |  32 k |   0 | 66/66 | 15.7 | 13.2/0.1  | q8_0 (q4_0) |    533 |  15s | MTP        min=1 max=5 p_min=0.20 (88%) |  1024/512 |                   |

|  37 t/s |  52 k |   0 | 66/66 | 15.6 | 13.2/0.1  | q4_0 (q4_0) |    524 |  14s | MTP        min=1 max=3 p_min=0.20 (91%) |  1024/512 |                   |

|  37 t/s |  56 k |   0 | 66/66 | 15.7 | 13.2/0.1  | q4_0 (q4_0) |    524 |  14s | MTP        min=1 max=3 p_min=0.20 (91%) |  1024/512 |                   |
|  42 t/s |  48 k |   0 | 66/66 | 15.6 | 13.2/0.1  | q4_0 (q4_0) |    528 |  13s | MTP        min=1 max=4 p_min=0.20 (90%) |  1024/512 |                   |
|  42 t/s |  36 k |   0 | 66/66 | 15.3 | 13.2/0.1  | q4_0 (q4_0) |    528 |  13s | MTP        min=1 max=4 p_min=0.20 (90%) |  1024/512 |                   |


model=Swift-1.5-Qwen3.8-27B-GSQ-RCO-IQ3_S-mtp_ukisai.gguf
ctx_k=64
gpu_layers=99
cpu_moe=0
quant=q8_0/q4_0
spec=draft-mtp,ngram-simple
draft_model=none
predict_token=1/4
ngram_values=24/24
jinja=1
batch=1024
ubatch=512
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                       | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | -------------------------------------------- | --------- |------------------ |
|  45 t/s |  64 k |   0 | 66/66 | 15.0 | 10.9/0.1  | q8_0 (q4_0) |    555 |  13s | 90% = MTP 1/4 (97%) N-gram 24/24/1 (---)     |  1024/512 |                   |
|  36 t/s |  64 k |   0 | 66/66 | 15.1 | 10.9/0.1  | q8_0 (q4_0) |    557 |  15s | 82% = MTP 1/5 (96%) N-gram 16/16/1 (100%)    |  1024/512 |                   |
|  37 t/s |  64 k |   0 | 66/66 | 15.0 | 10.9/0.1  | q8_0 (q4_0) |    557 |  15s | 78% = MTP 1/4 (97%) N-gram 16/16/1 (100%)    |  1024/512 |                   |
|  37 t/s |  64 k |   0 | 66/66 | 14.8 | 10.9/0.1  | q8_0 (q4_0) |    555 |  14s | 85% = MTP 1/3 (98%) N-gram 16/16/1 (100%)    |  1024/512 |                   |
|  35 t/s |  64 k |   0 | 66/66 | 14.7 | 10.9/0.1  | q8_0 (q4_0) |    555 |  16s | 96% = MTP 1/2 (97%) N-gram 24/24/1 (---)     |  1024/512 |                   |
|  32 t/s |  64 k |   0 | 66/66 | 14.7 | 10.9/0.1  | q8_0 (q4_0) |    555 |  17s | 84% = MTP 1/2 (98%) N-gram 16/16/1 (100%)    |  1024/512 |                   |

|  45 t/s |  64 k |   0 | 66/66 | 13.8 | 10.9/0.0  | q4_0 (q4_0) |    586 |  13s | 92% = MTP 1/4 (98%) N-gram 24/24/1 (---)     |  1024/256 |                   |
|  41 t/s |  64 k |   0 | 66/66 | 13.8 | 10.9/0.0  | q4_0 (q4_0) |    586 |  14s | 83% = MTP 1/4 (98%) N-gram 16/16/1 (100%)    |  1024/256 |                   |
|  33 t/s |  64 k |   0 | 66/66 | 13.5 | 10.9/0.0  | q4_0 (q4_0) |    586 |  18s | 88% = MTP 1/2 (99%) N-gram 16/16/1 (100%)    |  1024/256 |                   |


model=Swift-1.5-Qwen3.8-27B-IQ3_M_ukisai.gguf
ctx_k=60
gpu_layers=99
cpu_moe=0
quant=q4_0
spec=draft-mtp,ngram-simple
draft_model=none
predict_token=1/2
ngram_values=16/16
jinja=1
batch=1024
ubatch=256
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                       | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | -------------------------------------------- | --------- |------------------ |
|  31 t/s |  56 k |   0 | 66/66 | 15.4 | 13.2/0.0  | q4_0 (q4_0) |    576 |  18s | 90% = MTP 1/2 (98%) N-gram 16/16/1 (100%)    |   512/256 |                   |
|  31 t/s |  48 k |   0 | 66/66 | 15.2 | 13.2/0.0  | q4_0 (q4_0) |    576 |  19s | 90% = MTP 1/2 (98%) N-gram 16/16/1 (100%)    |   512/256 |                   |
|  17 t/s |  48 k |   0 | 66/66 | 15.7 | 13.2/0.0  | q8_0 (q4_0) |    558 |  32s | 89% = MTP 1/3 (98%) N-gram 16/16/1 (100%)    |  1024/256 |                   |
|  34 t/s |  32 k |   0 | 66/66 | 15.5 | 13.2/0.0  | q8_0 (q4_0) |    558 |  17s | 79% = MTP 1/4 (97%) N-gram 12/12/1 (100%)    |  1024/256 |                   |


model=Swift-Qwen3.8-27B-IQ3_XS_ukisai.gguf
ctx_k=68
gpu_layers=99
cpu_moe=0
quant=q8_0/q4_0
spec=draft-mtp,ngram-simple
draft_model=none
predict_token=1/4
ngram_values=12/12
jinja=1
batch=1024
ubatch=256
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                       | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | -------------------------------------------- | --------- |------------------ |
|  37 t/s |  68 k |   0 | 66/66 | 15.7 | 11.4/0.2  | q8_0 (q8_0) |    568 |  15s | 91% = MTP 1/4 (97%)                          | 1024/1024 |                   |
|  31 t/s |  68 k |   0 | 66/66 | 15.4 | 11.4/0.0  | q8_0 (q8_0) |    533 |  18s | 70% = MTP 1/4 (95%) N-gram 12/12/1 (100%)    |  1024/256 |                   |


| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |------------------ |
|  32 t/s |  80 k |   0 | 66/66 | 15.7 | 11.4/0.1  | q8_0 (q4_0) |    568 |  18s | MTP        min=1 max=3 p_min=0.20 (93%) |  1024/512 | R: medium         |
|  43 t/s |  64 k |   0 | 66/66 | 15.6 | 11.4/0.1  | q8_0 (q8_0) |    568 |  13s | MTP        min=1 max=5 p_min=0.20 (87%) |  1024/512 | R: medium         |
|  43 t/s |  64 k |   0 | 66/66 | 15.4 | 11.4/0.1  | q8_0 (q8_0) |    568 |  13s | MTP        min=1 max=4 p_min=0.20 (89%) |  1024/512 | R: medium         |
|  43 t/s |  64 k |   0 | 66/66 | 15.4 | 11.4/0.1  | q8_0 (q4_0) |    568 |  13s | MTP        min=1 max=4 p_min=0.20 (89%) |  1024/512 | R: medium         |
|  39 t/s |  64 k |   0 | 66/66 | 15.2 | 11.4/0.1  | q8_0 (q4_0) |    568 |  15s | MTP        min=1 max=3 p_min=0.20 (93%) |  1024/512 | R: medium         |
|  39 t/s |  64 k |   0 | 66/66 | 15.3 | 11.4/0.1  | q8_0 (q8_0) |    568 |  15s | MTP        min=1 max=3 p_min=0.20 (92%) |  1024/512 | R: medium         | + ngram
|  39 t/s |  64 k |   0 | 66/66 | 15.2 | 11.4/0.1  | q8_0 (q4_0) |    568 |  14s | MTP        min=1 max=3 p_min=0.20 (93%) |  1024/512 | R: medium         |


model=Qwen3.8-27B-Uncensored-IQ3_M_orcarouter.gguf
ctx_k=64
gpu_layers=99
cpu_moe=0
quant=q8_0/q8_0
spec=draft-mtp,ngram-simple
draft_model=none
predict_token=1/3
ngram_values=12/8
jinja=0
batch=1024
ubatch=512
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |------------------ |
|  32 t/s |  80 k |   0 | 66/66 | 15.7 | 11.4/0.1  | q8_0 (q8_0) |    544 |  17s | MTP        min=1 max=3 p_min=0.20 (91%) |  1024/512 | R: medium         |
|  30 t/s |  80 k |   0 | 66/66 | 15.7 | 11.4/0.1  | q8_0 (q8_0) |    544 |  18s | MTP        min=1 max=2 p_min=0.20 (95%) |  1024/512 | R: medium         |

|  39 t/s |  64 k |   0 | 66/66 | 15.2 | 11.4/0.1  | q8_0 (q8_0) |    544 |  14s | MTP        min=1 max=3 p_min=0.20 (91%) |  1024/512 | R: medium         |
|  38 t/s |  64 k |   0 | 66/66 | 15.2 | 11.4/0.1  | q8_0 (q8_0) |    544 |  15s | MTP        min=1 max=3 p_min=0.20 (91%) |  1024/512 | R: medium ngram 16/12 |
|  36 t/s |  64 k |   0 | 66/66 | 15.2 | 11.4/0.1  | q8_0 (q8_0) |    544 |  15s | MTP        min=1 max=3 p_min=0.20 (86%) |  1024/512 | R: medium ngram 16/16 |
|  33 t/s |  64 k |   0 | 66/66 | 15.0 | 11.4/0.1  | q8_0 (q8_0) |    544 |  17s | MTP        min=1 max=2 p_min=0.20 (95%) |  1024/512 | R: medium         |
|  32 t/s |  80 k |   0 | 66/66 | 15.7 | 11.4/0.1  | q8_0 (q8_0) |    544 |  17s | MTP        min=1 max=3 p_min=0.20 (91%) |  1024/512 | R: medium         |

|  38 t/s |  64 k |   0 | 66/66 | 15.1 | 11.4/0.1  | q8_0 (q4_0) |    544 |  14s | MTP        min=1 max=3 p_min=0.20 (91%) |  1024/512 | R: medium         |
|  33 t/s |  80 k |   0 | 66/66 | 15.7 | 11.4/0.1  | q8_0 (q4_0) |    544 |  17s | MTP        min=1 max=3 p_min=0.20 (91%) |  1024/512 | R: medium         |

|  37 t/s |  64 k |   0 | 66/66 | 14.1 | 11.4/0.1  | q4_0 (q4_0) |    549 |  15s | MTP        min=1 max=3 p_min=0.20 (88%) |  1024/512 | R: medium         |


model=Qwen3.8-27B-UD-IQ3_S_unsloth.gguf
ctx_k=128
gpu_layers=99
cpu_moe=0
quant=q8_0/q4_0
spec=draft-mtp
draft_model=none
predict_token=1/4
jinja=0
batch=1024
ubatch=512
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |------------------ |
|  46 t/s | 128 k |   0 | 66/66 | 15.2 | 10.7/0.1  | q4_0 (q4_0) |    510 |  11s | MTP        min=1 max=4 p_min=0.20 (94%) |  1024/256 | R: medium         |
|  45 t/s | 128 k |   0 | 66/66 | 15.4 | 10.7/0.1  | q4_0 (q4_0) |    521 |  11s | MTP        min=1 max=4 p_min=0.20 (94%) |  1024/512 | R: medium         |


model=Qwen3.8-27B-Q3_K_S_unsloth.gguf
ctx_k=128
gpu_layers=99
cpu_moe=0
quant=q8_0/q8_0
spec=draft-mtp
draft_model=none
predict_token=1/4
jinja=0
batch=1024
ubatch=256
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |------------------ |
| Q8_0 / Q8_0
|  37 t/s |  64 k |   0 | 66/66 | 15.2 | 11.2/0.1  | q8_0 (q8_0) |    633 |  18s | MTP        min=1 max=4 p_min=0.20 (84%) |  1024/512 | R: medium         |
|  36 t/s |  64 k |   0 | 66/66 | 15.3 | 11.2/0.1  | q8_0 (q8_0) |    633 |  18s | MTP        min=1 max=5 p_min=0.20 (76%) |  1024/512 | R: medium         |
|  34 t/s |  64 k |   0 | 66/66 | 15.0 | 11.2/0.1  | q8_0 (q8_0) |    529 |  16s | MTP        min=1 max=3 p_min=0.20 (92%) |  1024/512 | R: medium         |
| Q8_0 / Q4_0
|  16 t/s |  96 k |   0 | 66/66 | 15.7 | 11.2/0.1  | q8_0 (q4_0) |    529 |  33s | MTP        min=1 max=3 p_min=0.20 (92%) |  1024/512 | R: medium         |
|  36 t/s |  80 k |   0 | 66/66 | 15.7 | 11.2/0.1  | q8_0 (q4_0) |    633 |  17s | MTP        min=1 max=4 p_min=0.20 (83%) |  1024/512 | R: medium         |
|  30 t/s |  80 k |   0 | 66/66 | 15.5 | 11.2/0.1  | q8_0 (q4_0) |    601 |  20s | MTP        min=1 max=2 p_min=0.20 (93%) |  1024/512 | R: medium         |
|  37 t/s |  64 k |   0 | 66/66 | 15.1 | 11.2/0.1  | q8_0 (q4_0) |    633 |  17s | MTP        min=1 max=4 p_min=0.20 (83%) |  1024/512 | R: medium         |
|  30 t/s |  64 k |   0 | 66/66 | 14.8 | 11.2/0.1  | q8_0 (q4_0) |    601 |  20s | MTP        min=1 max=2 p_min=0.20 (93%) |  1024/512 | R: medium         |
| Q4_0 / Q4_0
|  38 t/s | 128 k |   0 | 66/66 | 15.7 | 11.2/0.1  | q4_0 (q4_0) |    579 |  15s | MTP        min=3 max=4 p_min=0.20 (89%) |  1024/256 | R: medium         |
|  39 t/s |  96 k |   0 | 66/66 | 14.9 | 11.2/0.1  | q4_0 (q4_0) |    579 |  15s | MTP        min=3 max=4 p_min=0.20 (89%) |  1024/256 | R: medium         |
|  39 t/s |  32 k |   0 | 66/66 | 13.1 | 11.2/0.0  | q4_0 (q4_0) |    579 |  15s | MTP        min=3 max=4 p_min=0.20 (89%) |  1024/256 | R: medium         |
|  34 t/s | 128 k |   0 | 66/66 | 15.6 | 11.2/0.1  | q4_0 (q4_0) |    579 |  17s | MTP        min=2 max=3 p_min=0.20 (93%) |  1024/256 | R: medium         |
|  19 t/s | 128 k |   0 | 66/66 | 15.7 | 11.2/0.1  | q4_0 (q4_0) |    579 |  30s | MTP        min=4 max=5 p_min=0.20 (84%) |  1024/256 | R: medium         |


model=Qwen3.8-27B-Cold-Fusion-GAIN-V1.1-NM-DAU-NEO-MAX-NEO-MTP-IQ3_M_davidau.gguf
ctx_k=64
gpu_layers=99
cpu_moe=0
spec=draft-mtp
draft_model=none
predict_token=3/4
jinja=0
batch=1024
ubatch=512
#extra=""
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note       |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |----------- |
|  26 t/s |  64 k |   0 | 66/66 | 15.7 | 13.0/0.1  | q4_0 (q4_0) |    575 |  22s | MTP        min=3 max=4 p_min=0.20 (86%) |  1024/512 | R: medium  |
|  20 t/s |  64 k |   0 | 66/66 | 15.7 | 13.0/0.1  | q4_0 (q4_0) |    575 |  28s | MTP        min=4 max=5 p_min=0.20 (80%) |  1024/512 | R: medium  |


model=Qwen3.8-27B-Uncensored-Aggressive-IQ3_M_HauhauCS.gguf
ctx_k=128
gpu_layers=99
cpu_moe=0
spec=draft-mtp
draft_model=none
predict_token=3/4
jinja=0
batch=1024
ubatch=256
#extra=""
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note       |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |----------- |
|  41 t/s | 128 k |   0 | 66/66 | 15.7 | 11.4/0.1  | q4_0 (q4_0) |    544 |  14s | MTP        min=3 max=4 p_min=0.20 (90%) |  1024/256 | R: medium  |
|  41 t/s | 128 k |   0 | 66/66 | 15.7 | 11.4/0.1  | q4_0 (q4_0) |    544 |  13s | MTP        min=5 max=6 p_min=0.20 (82%) |  1024/256 | R: medium  |
|  24 t/s | 128 k |   0 | 66/66 | 15.7 | 11.4/0.1  | q4_0 (q4_0) |    540 |  23s | MTP        min=5 max=6 p_min=0.20 (82%) |  1024/512 | R: medium  |
|  21 t/s | 128 k |   0 | 66/66 | 15.7 | 11.4/0.1  | q4_0 (q4_0) |    542 |  26s | MTP        min=6 max=7 p_min=0.20 (78%) |  1024/256 | R: medium  |
|
|  43 t/s |  96 k |   0 | 66/66 | 15.5 | 11.4/0.1  | q4_0 (q4_0) |    540 |  12s | MTP        min=5 max=6 p_min=0.20 (82%) |  1024/512 | R:medium   |
|  43 t/s |  96 k |   0 | 66/66 | 15.2 | 11.4/0.1  | q4_0 (q4_0) |    563 |  14s | MTP        min=3 max=4 p_min=0.20 (88%) |  1024/512 | R:medium   |
|  42 t/s |  96 k |   0 | 66/66 | 15.4 | 11.4/0.1  | q4_0 (q4_0) |    563 |  13s | MTP        min=4 max=5 p_min=0.20 (82%) |  1024/512 |            |
|  35 t/s |  96 k |   0 | 66/66 | 15.7 | 11.4/0.1  | q4_0 (q4_0) |    567 |  16s | MTP        min=6 max=8 p_min=0.20 (70%) |  1024/512 | R:medium   |


model=Qwen3.8-27B-UD-Q3_K_XL_unsloth.gguf
ctx_k=80
gpu_layers=99
cpu_moe=0
quant=q4_0/q4_0
spec=draft-mtp
draft_model=none
predict_token=1/4
jinja=0
batch=1024
ubatch=512
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |------------------ |
|  47 t/s |  80 k |   0 | 66/66 | 15.4 | 11.7/0.1  | q4_0 (q4_0) |    559 |  12s | MTP        min=5 max=6 p_min=0.20 (86%) |  1024/512 | R: medium         |
|  46 t/s |  80 k |   0 | 66/66 | 15.4 | 11.7/0.1  | q4_0 (q4_0) |    559 |  12s | MTP        min=6 max=6 p_min=0.20 (86%) |  1024/512 | R: medium         |
|  46 t/s |  80 k |   0 | 66/66 | 15.7 | 11.7/0.1  | q4_0 (q4_0) |    586 |  12s | MTP        min=6 max=8 p_min=0.20 (78%) |  1024/512 | R: medium         |
|  43 t/s |  80 k |   0 | 66/66 | 15.2 | 11.7/0.1  | q4_0 (q4_0) |    567 |  14s | MTP        min=4 max=5 p_min=0.20 (82%) |  1024/512 | R: medium         |
|  43 t/s |  80 k |   0 | 66/66 | 15.1 | 11.7/0.1  | q4_0 (q4_0) |    567 |  13s | MTP        min=3 max=4 p_min=0.20 (89%) |  1024/512 | R: medium         |
|  40 t/s |  80 k |   0 | 66/66 | 14.9 | 11.7/0.1  | q4_0 (q4_0) |    530 |  13s | MTP        min=2 max=3 p_min=0.20 (95%) |  1024/512 | R: medium         |
|  18 t/s |  80 k |   0 | 66/66 | 15.7 | 11.7/0.1  | q8_0 (q4_0) |    535 |  29s | MTP        min=3 max=4 p_min=0.20 (90%) |  1024/512 | R: medium         |

|  46 t/s |  64 k |   0 | 66/66 | 14.9 | 11.7/0.1  | q4_0 (q4_0) |    559 |  12s | MTP        min=6 max=6 p_min=0.20 (86%) |  1024/512 | R: medium         |
|  46 t/s |  64 k |   0 | 66/66 | 14.9 | 11.7/0.1  | q4_0 (q4_0) |    559 |  12s | MTP        min=4 max=6 p_min=0.20 (86%) |  1024/512 | R: medium         |
|  43 t/s |  64 k |   0 | 66/66 | 14.8 | 11.7/0.1  | q4_0 (q4_0) |    567 |  13s | MTP        min=3 max=5 p_min=0.20 (82%) |  1024/512 | R: medium         |
|  21 t/s |  64 k |   0 | 66/66 | 15.7 | 11.7/0.1  | q8_0 (q4_0) |    535 |  26s | MTP        min=6 max=8 p_min=0.20 (82%) |  1024/512 | R: medium         |


model=RVN-Q3_K_M-mtp_observerx.gguf
ctx_k=96
gpu_layers=99
cpu_moe=0
spec=draft-mtp
draft_model=none
predict_token=3/4
jinja=0
batch=1024
ubatch=512
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note       |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |----------- |
|  20 t/s |  96 k |   0 | 66/66 | 15.7 | 12.3/0.1  | q4_0 (q4_0) |    837 |  41s | MTP        min=3 max=4 p_min=0.20 (82%) |  1024/512 |            |
|  19 t/s |  96 k |   0 | 66/66 | 15.7 | 12.3/0.1  | q4_0 (q4_0) |    831 |  43s | MTP        min=4 max=5 p_min=0.20 (76%) |  1024/512 |            |
|  35 t/s |  64 k |   0 | 66/66 | 15.1 | 12.3/0.0  | q4_0 (q4_0) |    881 |  25s | MTP        min=3 max=4 p_min=0.20 (80%) |  1024/256 |            |
|  18 t/s |  64 k |   0 | 66/66 | 15.7 | 12.3/0.0  | q8_0 (q4_0) |    784 |  44s | MTP        min=3 max=4 p_min=0.20 (85%) |  1024/256 |            |
|  17 t/s |  64 k |   0 | 66/66 | 15.7 | 12.3/0.0  | q8_0 (q4_0) |    784 |  45s | MTP        min=3 max=4 p_min=0.20 (85%) |  2048/256 |            |
|  24 t/s |  64 k |   0 | 66/66 | 15.7 | 12.3/0.0  | q8_0 (q8_0) |    784 |  33s | MTP        min=3 max=4 p_min=0.20 (84%) |  2048/256 | K: q8_0    |
|  36 t/s |  32 k |   0 | 66/66 | 14.7 | 12.3/0.0  | q8_0 (q4_0) |    784 |  21s | MTP        min=3 max=4 p_min=0.20 (85%) |  2048/256 |            |
|  29 t/s |  32 k |   0 | 66/66 | 14.6 | 12.3/0.0  | q8_0 (q8_0) |    784 |  27s | MTP        min=3 max=4 p_min=0.20 (84%) |  2048/256 | K: q8_0    |


model=RVN-IQ3_M-mtp_Observerx.gguf
ctx_k=96
gpu_layers=99
cpu_moe=0
spec=draft-mtp
draft_model=none
predict_token=3/4
jinja=0
batch=1024
ubatch=512
_test_model

OpenAI tools compatibility    : ❌


model=Qwen3.8-27B-UD-Q2_K_XL_unsloth.gguf
ctx_k=128
gpu_layers=99
cpu_moe=0
quant=q8_0/q4_0
spec=draft-mtp
draft_model=none
predict_token=1/4
jinja=0
batch=1024
ubatch=512
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |------------------ |
|  49 t/s | 128 k |   0 | 66/66 | 15.7 | 8.8/0.1   | q8_0 (q8_0) |    563 |  11s | MTP        min=1 max=4 p_min=0.20 (93%) |  1024/512 | R: medium         |
|  43 t/s | 128 k |   0 | 66/66 | 15.6 | 8.8/0.1   | q8_0 (q8_0) |    557 |  13s | MTP        min=1 max=3 p_min=0.20 (92%) |  1024/512 | R: medium         |
|  39 t/s | 128 k |   0 | 66/66 | 15.7 | 8.8/0.1   | q8_0 (q8_0) |    563 |  14s | MTP        min=1 max=5 p_min=0.20 (87%) |  1024/512 | R: medium         |
|  49 t/s |  96 k |   0 | 66/66 | 14.3 | 8.8/0.1   | q8_0 (q8_0) |    563 |  11s | MTP        min=1 max=4 p_min=0.20 (93%) |  1024/512 | R: medium         |

|  49 t/s | 128 k |   0 | 66/66 | 15.6 | 8.8/0.1   | q8_0 (q4_0) |    563 |  11s | MTP        min=1 max=4 p_min=0.20 (93%) |  1024/512 | R: medium         |
|  49 t/s | 128 k |   0 | 66/66 | 15.6 | 8.8/0.1   | q8_0 (q4_0) |    563 |  12s | MTP        min=4 max=4 p_min=0.20 (93%) |  1024/512 | R: medium         |
|  45 t/s | 128 k |   0 | 66/66 | 15.7 | 8.8/0.1   | q8_0 (q4_0) |    563 |  12s | MTP        min=2 max=5 p_min=0.20 (88%) |  1024/512 | R: medium         |

|  46 t/s |  96 k |   0 | 66/66 | 12.7 | 8.8/0.1   | q4_0 (q4_0) |    569 |  12s | MTP        min=1 max=4 p_min=0.20 (86%) |  1024/512 | R: medium         |


```