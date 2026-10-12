# Qwen 3.8 27B - Q4


Ubatch set to 256 makes the model dumb !!

| File                                                       | GB   | Result                                               |
|------------------------------------------------------------| ---- |------------------------------------------------------|
| Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf                         | 13.2 | ✔️ 64k: 20-40 t/s ✔️ Q8 80k: 20-30 t/s              |
| Swift-Qwen3.8-27b-i1-IQ4_XS-Smaller_ahmeddelkilami01.gguf  | 12.6 | ✔️   ❌Loop when ubatch is 512                      |
| Qwen3.8-27B-IQ4_XS-3.84bpw_byteshape.gguf                  | 12.1 | ✔️ 64k: 25-45 t/s  Good PR                           |
| Qwen3.8-27B-i1-IQ4_XS-GGUF-Smaller_jrell.gguf              | 12.6 | ✔️ 56k: 15-45 t/s Good                               |
| Qwen3.8-27B-ZB4.00-MIN-v5-IQ4_XS_tooltd.gguf               | 12.7 | ✔️ 64k: ? t/s                                        |
| Qwen3.8-27B-abliterated-UD-IQ4_XS_huihui.gguf              | 13.4 | ✔️ 64k: 15-40 t/s   Not good result in test. Slow.   |
| Qwen3.8-27B-UD-IQ4_XS_peculiar.gguf                        | 13.2 | Not tested in Pi                                     |
| Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666.gguf          | 13.2 | ✔️ 64k: 25-45 t/s . Loping ?  ❌ Miss update TODO.  |
| Qwen3.8-27B-heretic-ara.i1-IQ4_XS_mradermacher.gguf        | 14.2 | ❌ Slow. Max 32K.                           |
| Qwen3.8-27B-IQ4_XS_unsloth.gguf                            | 14.6 | ❌ Too slow. Unusable                       | 
| Swift-1.5-Qwen3.8-27B-IQ4_XS_ukisai.gguf                   | 14.4 | ❌ Max 44K at Q4. Q8 ma 32k                 |
| Qwen3.8-27B.i1-IQ4_KT-attn_qkv-IQ4_KS-MTP_chunter789.gguf  | 13.9 | ❌ attn_qkv- does not work                  |
| Qwen3.8-27B-ASCII-Condensed-IQ4_XS-3.84bpw_troed.gguf      | 11.4 |      |
| Swift-1.5-Qwen3.8-27B-MiniPlus-V3_isvalorum.gguf           | 13.1 | ❌ Too slow. Missed update TODO                       | 
| Swift-1.5-Qwen3.8-27b.i1-IQ4_XS_mrradermacher.gguf         | 14.2 | ❌ Too big. Too slow.                                 |
| Qwen3.8-27B-YMQ-M_zerodigest.gguf                          | 13.6 | ❌ Rubbish                                            |



## ✔️ Swift i1-IQ4_XS (ahmeddelkilami01)
Swift-Qwen3.8-27b-i1-IQ4_XS-Smaller_ahmeddelkilami01.gguf       12.6 GB
https://huggingface.co/ahmedelkilani01/Swift-Qwen3.8-27B-i1-IQ4_XS-Smaller-GGUF
Based on UkisAI's Swift-Qwen3.8-27B and tensor from jrell/Qwen3.8-27B-i1-IQ4_XS-GGUF-Smaller, 

## ✔️ IQ4_XS (byteshape)
Qwen3.8-27B-IQ4_XS-3.84bpw_byteshape.gguf                       12.1 GB
https://huggingface.co/byteshape/Qwen3.8-27B-GGUF

## ✔️ UD-IQ4_XS (huihui)
Qwen3.8-27B-abliterated-UD-IQ4_XS_huihui.gguf                   13.4 GB
https://huggingface.co/huihui-ai/Huihui-Qwen3.8-27B-abliterated-GGUF      
With Temperatur 0.5 and 0.05 point less in repetition  it output rubbish

## ✔️ IQ4_XS (tooltd)
Qwen3.8-27B-ZB4.00-MIN-v5-IQ4_XS_tooltd.gguf                    12.7 GB
https://huggingface.co/tooltd/Qwen3.8-27B-IQ4-XS-16GB-VRAM-GGUF

## ? UD-IQ4_XS (peculiar-ragdoll)
Qwen3.8-27B-UD-IQ4_XS_peculiar.gguf                             13.2 GB
https://huggingface.co/peculiar-ragdoll/Dirk-Qwen3.8-27B-GGUF
Not tested in Pi

## ✔️ IQ4_XS (jrell)
Qwen3.8-27B-i1-IQ4_XS-GGUF-Smaller_jrell.gguf                    12.6 GB
https://huggingface.co/jrell/Qwen3.8-27B-i1-IQ4_XS-GGUF-Smaller
Optimized for 16 GB

## ✔️ UD IQ4_XS (Unsloth)
Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf                               13.2 GB
3/4 give better speed in real coding tests in Pi, compared to 5/6.  
1024/256 Batch settings makes profiling load almost instantaneous.

## ✔️ IQ4-MIX-MTP (finx666)
Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666.gguf                13.2 GB

## ✔️ ASCII-Condensed-IQ4_XS-3 (troed)
Qwen3.8-27B-ASCII-Condensed-IQ4_XS-3.84bpw_troed.gguf
Qwen3.8-27B-ASCII-Condensed-DFlash2-Q2_K_S-MIX_troed.gguf
https://huggingface.co/troed/Qwen3.8-27B-ASCII-Condensed
required DFlash file too   spec-type = draft-dflash

## ❌ Swift-1.5 (isvalorum)   [DELETED]
Swift-1.5-Qwen3.8-27B-MiniPlus-V3_isvalorum.gguf                 13.1 GB
(renamed: Swift-1.5-Qwen3.8-27B-Uncensored-VAL-APEX-I-MiniPlus-V3_isvalorum.gguf)
https://huggingface.co/IsValorum/Swift-1.5-Qwen3.8-27B-Uncensored-MTP-VAL-APEX-I-MiniPlus-V3-GGUF
Missed update of TODO. Too slow.

## ❌ IQ4_KS (mradermacher)
Qwen3.8-27B-heretic-ara.i1-IQ4_XS_mradermacher.gguf              14.2 GB
https://huggingface.co/mradermacher/Qwen3.8-27B-heretic-ara-i1-GGUF
Max 32 K

## ❌ Swift 1.5 IQ4_XS (ukisai)  [DELETED]
Swift-1.5-Qwen3.8-27B-IQ4_XS_ukisai.gguf                         14.4 GB
Too big. max 32k

## ❌ IQ4_XS (Unsloth)
Qwen3.8-27B-IQ4_XS_unsloth.gguf                                  14.60 GB
16 t/s

## ❌ YMQ-M (zerodigest)
Qwen3.8-27B-YMQ-M_zerodigest.gguf                            13.6 GB
https://huggingface.co/zerodigest/Qwen3.8-27B-YMQ-MTP-GGUF
20 t/s. Not pass simple test (more than 2000 tokens)

## ❌ UD Q2_K_XL (Unsloth)
Qwen3.8-27B-UD-Q2_K_XL_unsloth.gguf
It changed the CHANGELOG for the test PR


```bash

model=Qwen3.8-27B-ASCII-Condensed-IQ4_XS-3.84bpw_troed.gguf
draft_model=Qwen3.8-27B-ASCII-Condensed-DFlash2-Q2_K_S-MIX_troed.gguf
ctx_k=68
gpu_layers=99
cpu_moe=0
quant=q8_0/q8_0
spec=draft-flash,ngram-simple
predict_token=1/6
ngram_values=32/24/1
jinja=1
batch=1024
ubatch=512
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |------------------ |
|  53 t/s |  80 k |   0 | 66/66 | 15.2 | 10.9/0.2  | q4_0 (q4_0) |    553 |  10s | N-gram            N=32 M=24 min=1 (74%) | 1024/1024 | R: medium         |
|  52 t/s |  80 k |   0 | 66/66 | 14.6 | 10.9/0.1  | q4_0 (q4_0) |    578 |  12s | N-gram            N=32 M=24 min=1 (80%) |  1024/512 | R: medium         |


| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                       | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | -------------------------------------------- | --------- |------------------ |
|  55 t/s |  68 k |   0 | 66/66 | 15.6 | 10.9/0.1  | q8_0 (q8_0) |    582 |  10s | 68% = DFlash 1/6 (80%) N-gram 32/24/1 (---)  |  1024/512 | R: medium         |
|  53 t/s |  68 k |   0 | 66/66 | 15.5 | 10.9/0.1  | q8_0 (q8_0) |    563 |  11s | 75% = DFlash 1/5 (92%) N-gram 32/24/1 (---)  |  1024/512 | R: medium         |
|  54 t/s |  68 k |   0 | 66/66 | 15.5 | 10.9/0.1  | q8_0 (q8_0) |    563 |  10s | 75% = DFlash 1/5 (---) N-gram 32/24/1 (---)  |  1024/512 | R: medium         |

|  52 t/s |  64 k |   0 | 66/66 | 15.3 | 10.9/0.1  | q8_0 (q8_0) |    563 |  11s | 75% = DFlash 1/5 (---) N-gram 32/24/1 (---)  |  1024/512 | R: medium         |
|  52 t/s |  64 k |   0 | 66/66 | 15.1 | 10.9/0.0  | q8_0 (q8_0) |    601 |  12s | 72% = DFlash 1/5 (---) N-gram 32/24/1 (---)  |  1024/256 | R: medium         |

|  54 t/s |  64 k |   0 | 66/66 | 15.3 | 10.9/0.1  | q8_0 (q4_0) |    563 |  11s | 75% = DFlash 1/5 (---) N-gram 32/24/1 (---)  |  1024/512 | R: medium         |
|  52 t/s |  64 k |   0 | 66/66 | 15.3 | 10.9/0.0  | q8_0 (q4_0) |    601 |  12s | 64% = DFlash 1/6 (---) N-gram 32/24/1 (---)  |  1024/256 | R: medium         |
|  52 t/s |  64 k |   0 | 66/66 | 15.1 | 10.9/0.0  | q8_0 (q4_0) |    601 |  12s | 71% = DFlash 1/5 (---) N-gram 32/24/1 (---)  |  1024/256 | R: medium         |
|  52 t/s |  64 k |   0 | 66/66 | 15.2 | 10.9/0.1  | q8_0 (q4_0) |    563 |  11s | 80% = DFlash 1/4 (---) N-gram 32/24/1 (---)  |  1024/512 | R: medium         |
|  52 t/s |  64 k |   0 | 66/66 | 15.2 | 10.9/0.1  | q8_0 (q4_0) |    563 |  10s | 80% = DFlash 1/4 (---) N-gram 24/16/1 (---)  |  1024/512 | R: medium         |
|  52 t/s |  64 k |   0 | 66/66 | 14.2 | 10.9/0.1  | q4_0 (q4_0) |    578 |  11s | 80% = DFlash 1/4 (---) N-gram 24/16/1 (---)  |  1024/512 | R: medium         |
|  46 t/s |  64 k |   0 | 66/66 | 15.2 | 10.9/0.1  | q8_0 (q4_0) |    563 |  13s | 73% = DFlash 1/4 (---) N-gram 16/16/1 (---)  |  1024/512 | R: medium         |
|  45 t/s |  64 k |   0 | 66/66 | 14.0 | 10.9/0.1  | q4_0 (q4_0) |    592 |  13s | 83% = DFlash 1/3 (---) N-gram 24/16/1 (---)  |  1024/512 | R: medium         |



model=Qwen3.8-27B-GSQ-RCO-IQ3_S-mtp_ista.gguf
ctx_k=60
gpu_layers=99
cpu_moe=0
quant=q8_0/q4_0
spec=draft-mtp,ngram-simple
draft_model=none
predict_token=1/4
ngram_values=32/24/1
jinja=1
batch=1024
ubatch=512
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |------------------ |
|  44 t/s |  64 k |   0 | 66/66 | 15.1 | 10.9/0.1  | q8_0 (q8_0) |    551 |  13s | MTP        min=1 max=4 p_min=0.20 (89%) |  1024/512 | R: medium         |
|  43 t/s |  60 k |   0 | 66/66 | 14.9 | 10.9/0.1  | q8_0 (q8_0) |    551 |  13s | MTP        min=1 max=4 p_min=0.20 (89%) |  1024/512 | R: medium         |
|  42 t/s |  68 k |   0 | 66/66 | 15.2 | 10.9/0.1  | q8_0 (q8_0) |    551 |  13s | MTP        min=1 max=4 p_min=0.20 (87%) |  1024/512 | R: medium         |



model=Swift-1.5-Qwen3.8-27B-MiniPlus-V3_isvalorum.gguf
ctx_k=48
gpu_layers=99
cpu_moe=0
quant=q8_0/q4_0
spec=draft-mtp,ngram-simple
draft_model=none
predict_token=1/2
ngram_values=24/24
jinja=1
batch=1024
ubatch=256
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                       | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | -------------------------------------------- | --------- |------------------ |
|  26 t/s |  48 k |   0 | 66/66 | 15.3 | 12.5/0.0  | q8_0 (q4_0) |   2048 |  78s | 64% = MTP 1/2 (74%) N-gram 24/24/1 (100%)    |  1024/256 |                   |
|  26 t/s |  32 k |   0 | 66/66 | 14.9 | 12.5/0.0  | q8_0 (q4_0) |   1398 |  53s | 51% = MTP 1/4 (74%) N-gram 24/24/1 (100%)    |  1024/256 |                   |


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


model=Swift-Qwen3.8-27b-i1-IQ4_XS-Smaller_ahmeddelkilami01.gguf
ctx_k=60
gpu_layers=99
cpu_moe=0
quant=q8_0/q4_0
spec=draft-mtp,ngram-simple
draft_model=none
predict_token=1/4
ngram_values=16/16
jinja=0
batch=1024
ubatch=256
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                       | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | -------------------------------------------- | --------- |------------------ |
|  45 t/s |  56 k |   0 | 66/66 | 15.5 | 12.0/0.0  | q8_0 (q4_0) |    551 |  12s | 89% = MTP 1/5 (97%) N-gram 16/16/1 (100%)    |  1024/256 |                   |
|  44 t/s |  60 k |   0 | 66/66 | 15.6 | 12.0/0.0  | q8_0 (q4_0) |    551 |  13s | 92% = MTP 1/4 (98%) N-gram 24/24/1 (---)     |  1024/256 |                   |
|  38 t/s |  60 k |   0 | 66/66 | 15.6 | 12.0/0.0  | q8_0 (q4_0) |    551 |  14s | 82% = MTP 1/4 (98%) N-gram 16/16/1 (100%)    |  1024/256 |                   |
|  33 t/s |  64 k |   0 | 66/66 | 15.7 | 12.0/0.0  | q8_0 (q4_0) |    551 |  17s | 82% = MTP 1/4 (98%) N-gram 16/16/1 (100%)    |  1024/256 |                   |
|  18 t/s |  80 k |   0 | 66/66 | 15.7 | 12.0/0.0  | q8_0 (q4_0) |    551 |  31s | 82% = MTP 1/4 (98%) N-gram 16/16/1 (100%)    |  1024/256 |                   |


| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |------------------ |
|  33 t/s |  68 k |   0 | 66/66 | 15.7 | 12.0/0.1  | q8_0 (q4_0) |    551 |  16s | MTP        min=1 max=3 p_min=0.20 (94%) |  1024/512 |                   |
|  26 t/s |  68 k |   0 | 66/66 | 15.7 | 12.0/0.1  | q8_0 (q4_0) |    551 |  20s | MTP        min=1 max=2 p_min=0.20 (98%) |  1024/512 |                   |

|  39 t/s |  64 k |   0 | 66/66 | 15.6 | 12.0/0.0  | q8_0 (q4_0) |    551 |  14s | MTP        min=1 max=3 p_min=0.20 (93%) |   768/256 |                   |
|  39 t/s |  64 k |   0 | 66/66 | 15.7 | 12.0/0.1  | q8_0 (q4_0) |    551 |  14s | MTP        min=1 max=3 p_min=0.20 (94%) |  1024/512 |                   |
|  37 t/s |  64 k |   0 | 66/66 | 15.7 | 12.0/0.0  | q8_0 (q4_0) |    551 |  15s | MTP        min=1 max=4 p_min=0.20 (92%) |   768/256 |                   |
|  33 t/s |  64 k |   0 | 66/66 | 15.7 | 12.0/0.1  | q8_0 (q4_0) |    551 |  17s | MTP        min=1 max=3 p_min=0.20 (94%) |  2048/512 |                   |
|  24 t/s |  64 k |   0 | 66/66 | 15.7 | 12.0/0.2  | q8_0 (q4_0) |    557 |  23s | MTP        min=1 max=3 p_min=0.20 (93%) | 1024/1024 |                   |


|  34 t/s |  64 k |   0 | 66/66 | 15.7 | 12.0/0.1  | q8_0 (q8_0) |    551 |  16s | MTP        min=1 max=3 p_min=0.20 (95%) |  1024/512 |                   |

|  40 t/s |  64 k |   0 | 66/66 | 15.7 | 12.0/0.1  | q8_0 (q4_0) |    551 |  14s | MTP        min=1 max=3 p_min=0.20 (94%) |  1024/512 |                   |
|  36 t/s |  64 k |   0 | 66/66 | 15.7 | 12.0/0.1  | q8_0 (q4_0) |    551 |  15s | MTP        min=1 max=4 p_min=0.20 (92%) |  1024/512 |                   |

|  41 t/s |  64 k |   0 | 66/66 | 14.9 | 12.0/0.1  | q4_0 (q4_0) |    546 |  14s | MTP        min=1 max=4 p_min=0.20 (84%) |  1024/512 |                   |

|  43 t/s |  60 k |   0 | 66/66 | 15.7 | 12.0/0.1  | q8_0 (q4_0) |    551 |  13s | MTP        min=1 max=4 p_min=0.20 (92%) |  1024/512 |                   |
|  32 t/s |  60 k |   0 | 66/66 | 15.7 | 12.0/0.2  | q8_0 (q4_0) |    557 |  17s | MTP        min=1 max=3 p_min=0.20 (93%) | 1024/1024 |                   |
|  44 t/s |  56 k |   0 | 66/66 | 15.5 | 12.0/0.1  | q8_0 (q4_0) |    551 |  13s | MTP        min=1 max=4 p_min=0.20 (92%) |   768/512 |                   |


model=Swift-1.5-Qwen3.8-27b.i1-IQ4_XS_mrradermacher.gguf
ctx_k=48
gpu_layers=64
cpu_moe=0
quant=q4_0/q4_0
spec=draft-mtp
draft_model=none
predict_token=1/2
jinja=1
batch=1024
ubatch=256
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                       | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | -------------------------------------------- | --------- |------------------ |
|  12 t/s |  48 k |   0 | 64/66 | 15.3 | 13.2/0.1  | q4_0 (q4_0) |    570 |  48s | 98% = MTP 1/2 (99%)                          |  1024/256 |                   |



model=Swift-1.5-Qwen3.8-27B-IQ4_XS_ukisai.gguf
ctx_k=44
gpu_layers=64
cpu_moe=0
quant=q4_0/q4_0
spec=draft-mtp,ngram-simple
draft_model=none
predict_token=1/2
ngram_values=16/16
jinja=1
batch=512
ubatch=256
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                       | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | -------------------------------------------- | --------- |------------------ |
|  15 t/s |  56 k |   0 | 66/66 | 15.7 | 13.8/0.0  | q4_0 (q4_0) |    554 |  37s | 87% = MTP 1/2 (98%) N-gram 16/16/1 (100%)    |  1024/256 |                   |

|  30 t/s |  44 k |   0 | 66/66 | 15.7 | 13.8/0.0  | q4_0 (q4_0) |    554 |  19s | 87% = MTP 1/2 (98%) N-gram 16/16/1 (100%)    |   512/256 |                   |
|  29 t/s |  32 k |   0 | 66/66 | 15.5 | 13.8/0.0  | q4_0 (q4_0) |    554 |  19s | 73% = MTP 1/3 (97%) N-gram 12/12/1 (100%)    |  1024/256 |                   |
|  26 t/s |  32 k |   0 | 66/66 | 15.4 | 13.8/0.0  | q4_0 (q4_0) |    554 |  21s | 73% = MTP 1/2 (98%) N-gram 12/12/1 (100%)    |  1024/256 |                   |

|  25 t/s |  16 k |   0 | 66/66 | 15.3 | 13.8/0.0  | q8_0 (q4_0) |    585 |  24s | 70% = MTP 1/2 (98%) N-gram 12/12/1 (100%)    |  1024/512 |                   |


model=Qwen3.8-27B-ZB4.00-MIN-v5-IQ4_XS_tooltd.gguf
ctx_k=64
gpu_layers=99
cpu_moe=0
quant=q4_0
spec=draft-mtp
draft_model=none
predict_token=1/7
jinja=0
batch=1024
ubatch=512
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |------------------ |
|  45 t/s |  64 k |   0 | 66/66 | 15.3 | 12.3/0.1  | q4_0 (q4_0) |    574 |  12s | MTP        min=1 max=5 p_min=0.20 (86%) |  1024/512 | R: medium         |
|  44 t/s |  64 k |   0 | 66/66 | 15.7 | 12.3/0.2  | q4_0 (q4_0) |    564 |  13s | MTP        min=1 max=7 p_min=0.20 (77%) | 1024/1024 | R: medium         |


model=Qwen3.8-27B-i1-IQ4_XS-GGUF-Smaller_jrell.gguf
ctx_k=56
gpu_layers=99
cpu_moe=0
quant=q8_0/q8_0
spec=draft-mtp
draft_model=none
predict_token=2/7
jinja=0
batch=1024
ubatch=256
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |------------------ |
|  42 t/s |  56 k |   0 | 66/66 | 15.7 | 12.0/0.1  | q8_0 (q8_0) |    542 |  13s | MTP        min=2 max=7 p_min=0.20 (83%) |  1024/512 | R: medium         |
|  44 t/s |  52 k |   0 | 66/66 | 15.7 | 12.0/0.0  | q8_0 (q8_0) |    542 |  13s | MTP        min=2 max=7 p_min=0.20 (82%) |  1024/256 | R: medium         |
|  44 t/s |  48 k |   0 | 66/66 | 15.5 | 12.0/0.0  | q8_0 (q4_0) |    542 |  13s | MTP        min=2 max=7 p_min=0.20 (82%) |  1024/256 | R: medium         |
|  41 t/s |  64 k |   0 | 66/66 | 14.9 | 12.0/0.1  | q4_0 (q4_0) |    568 |  14s | MTP        min=3 max=4 p_min=0.20 (88%) |  1024/512 | R: medium         |
|  41 t/s |  64 k |   0 | 66/66 | 15.3 | 12.0/0.1  | q4_0 (q4_0) |    566 |  14s | MTP        min=2 max=7 p_min=0.20 (75%) |  1024/512 | R: medium         |
|  19 t/s |  64 k |   0 | 66/66 | 15.7 | 12.0/0.1  | q8_0 (q4_0) |    542 |  28s | MTP        min=2 max=7 p_min=0.20 (82%) |  1024/512 | R: medium         |
|  19 t/s |  64 k |   0 | 66/66 | 15.7 | 12.0/0.0  | q8_0 (q4_0) |    542 |  28s | MTP        min=2 max=7 p_min=0.20 (82%) |  1024/256 | R: medium         |


model=Qwen3.8-27B-UD-IQ4_XS_peculiar.gguf
ctx_k=64
gpu_layers=99
cpu_moe=0
quant=q4_0
spec=draft-mtp
draft_model=none
predict_token=1/4
ngram_values=none
jinja=0
batch=1024
ubatch=512
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |------------------ |
|  44 t/s |  64 k |   0 | 66/66 | 15.5 | 12.8/0.0  | q4_0 (q4_0) |    510 |  11s | MTP        min=3 max=4 p_min=0.20 (94%) |  1024/256 | R: medium         |
|  43 t/s |  64 k |   0 | 66/66 | 15.7 | 12.8/0.1  | q4_0 (q4_0) |    485 |  11s | MTP        min=5 max=5 p_min=0.20 (92%) |  1024/512 | R: medium         |
|  43 t/s |  64 k |   0 | 66/66 | 15.7 | 12.8/0.1  | q4_0 (q4_0) |    485 |  11s | MTP        min=4 max=5 p_min=0.20 (92%) |  1024/512 | R: medium         |
|  43 t/s |  64 k |   0 | 66/66 | 15.7 | 12.8/0.1  | q4_0 (q4_0) |    485 |  11s | MTP        min=4 max=4 p_min=0.20 (94%) |  1024/512 | R: medium         |
|  38 t/s |  64 k |   0 | 66/66 | 15.7 | 12.8/0.1  | q4_0 (q4_0) |    498 |  13s | MTP        min=4 max=6 p_min=0.20 (87%) |  1024/512 | R: medium         |
|  38 t/s |  64 k |   0 | 66/66 | 15.7 | 12.8/0.1  | q4_0 (q4_0) |    485 |  13s | MTP        min=3 max=5 p_min=0.20 (92%) |  1024/512 | R: medium         |
|  38 t/s |  64 k |   0 | 66/66 | 15.5 | 12.8/0.1  | q4_0 (q4_0) |    558 |  14s | MTP        min=2 max=3 p_min=0.20 (88%) |  1024/512 | R: medium         |
|  15 t/s |  64 k |   0 | 66/66 | 15.7 | 12.8/0.0  | q8_0 (q4_0) |    503 |  35s | MTP        min=1 max=2 p_min=0.20 (96%) |  1024/256 |                   |
|   8 t/s |  64 k |   0 | 66/66 | 15.7 | 12.8/0.0  | q8_0 (q4_0) |    486 |  58s | MTP        min=2 max=3 p_min=0.20 (94%) |  1024/256 |                   |


model=Qwen3.8-27B-abliterated-UD-IQ4_XS_huihui.gguf
ctx_k=64
gpu_layers=99
cpu_moe=0
quant=q4_0
spec=draft-mtp,ngram-simple
draft_model=none
predict_token=1/4
ngram_values=24/16/1
jinja=0
batch=1024
ubatch=256
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |------------------ |
|  42 t/s |  64 k |   0 | 66/66 | 15.6 | 12.9/0.0  | q4_0 (q4_0) |    570 |  13s | MTP        min=1 max=4 p_min=0.20 (89%) |  1024/256 | R: medium         |
|  38 t/s |  64 k |   0 | 66/66 | 15.5 | 12.9/0.0  | q4_0 (q4_0) |    557 |  14s | MTP        min=1 max=3 p_min=0.20 (92%) |  1024/256 | R: medium         |
|  38 t/s |  64 k |   0 | 66/66 | 15.7 | 12.9/0.1  | q4_0 (q4_0) |    585 |  15s | MTP        min=2 max=3 p_min=0.20 (94%) |  1024/512 | R: medium         |
|  34 t/s |  64 k |   0 | 66/66 | 15.7 | 12.9/0.1  | q4_0 (q4_0) |    604 |  17s | MTP        min=3 max=4 p_min=0.20 (88%) |  1024/512 | R: medium         |
|  33 t/s |  64 k |   0 | 66/66 | 15.5 | 12.9/0.1  | q4_0 (q4_0) |    587 |  18s | MTP        min=1 max=2 p_min=0.20 (96%) |  1024/512 | R: medium         |


model=Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666.gguf
ctx_k=64
gpu_layers=99
cpu_moe=0
quant=q8_0/q4_0
spec=draft-mtp
draft_model=none
predict_token=1/4
ngram_values=none
jinja=0
batch=1024
ubatch=256
_test_model

model=Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666.gguf
ctx_k=60
gpu_layers=99
cpu_moe=0
quant=q4_0
spec=draft-mtp
draft_model=none
predict_token=1/3
ngram_values=none
jinja=0
batch=1024
ubatch=512
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |------------------ |
|  36 t/s |  64 k |   0 | 66/66 | 15.7 | 12.9/0.1  | q4_0 (q4_0) |    559 |  16s | MTP        min=1 max=4 p_min=0.20 (92%) |  1024/512 | R: medium         |

|  47 t/s |  64 k |   0 | 66/66 | 15.7 | 12.9/0.0  | q4_0 (q4_0) |    513 |  11s | MTP        min=1 max=5 p_min=0.20 (92%) |  1024/256 | R: medium         |
|  44 t/s |  64 k |   0 | 66/66 | 15.6 | 12.9/0.0  | q4_0 (q4_0) |    513 |  12s | MTP        min=1 max=4 p_min=0.20 (94%) |  1024/256 | R: medium         |
|  44 t/s |  64 k |   0 | 66/66 | 15.6 | 12.9/0.0  | q4_0 (q4_0) |    513 |  11s | MTP        min=3 max=4 p_min=0.20 (94%) |  1024/256 | R: medium         |
|  44 t/s |  64 k |   0 | 66/66 | 15.7 | 12.9/0.1  | q4_0 (q4_0) |    559 |  13s | MTP        min=3 max=4 p_min=0.20 (92%) |  1024/512 | R: medium         |
|  38 t/s |  64 k |   0 | 66/66 | 15.7 | 12.9/0.1  | q4_0 (q4_0) |    559 |  15s | MTP        min=3 max=5 p_min=0.20 (86%) |  1024/512 | R: medium         |
|  21 t/s |  80 k |   0 | 66/66 | 15.7 | 12.9/0.1  | q4_0 (q4_0) |    559 |  27s | MTP        min=3 max=4 p_min=0.20 (92%) |  1024/512 | R: medium         |
|  19 t/s |  80 k |   0 | 66/66 | 15.7 | 12.9/0.1  | q4_0 (q4_0) |    559 |  29s | MTP        min=4 max=5 p_min=0.20 (86%) |  1024/512 | R: medium         |


model=Qwen3.8-27B-heretic-ara.i1-IQ4_XS_mradermacher.gguf
ctx_k=48
gpu_layers=99
cpu_moe=0
spec=draft-mtp
draft_model=none
predict_token=4/5
jinja=0
batch=1024
ubatch=512
#extra=""
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note       |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |----------- |
|  45 t/s |  32 k |   0 | 66/66 | 15.7 | 13.6/0.1  | q4_0 (q4_0) |    541 |  12s | MTP        min=4 max=5 p_min=0.20 (88%) |  1024/512 | R: medium  |
|  16 t/s |  64 k |   0 | 66/66 | 15.3 | 13.4/0.1  | q4_0 (none) |    533 |  33s | none                                 -- |  1024/512 |            |


model=Qwen3.8-27B-YMQ-M_zerodigest.gguf
ctx_k=64
gpu_layers=99
cpu_moe=0
spec=draft-mtp
draft_model=none
predict_token=3/4
jinja=0
batch=1024
ubatch=256
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | Cache | Tokens | Time | Prediction                       | Batch/Ub. | Note       |
| ------- | ----- | --- | ----- | ---- | --------- | ----- | ------ | ---- | -------------------------------- | --------- |----------- |
|  23 t/s |  64 k |   0 | 66/66 | 15.7 | 13.1/0.0  | q4_0  |   2048 |  90s | MTP     2/4             -- (87%) |   512/256 |            |
|  17 t/s |  64 k |   0 | 66/66 | 15.7 | 13.1/0.0  | q4_0  |   2048 | 122s | MTP     3/4             -- (75%) |  1024/256 |            |
|  16 t/s |  64 k |   0 | 66/66 | 14.9 | 13.1/0.0  | q4_0  |   1468 |  89s | none                          -- |   512/256 |            |

# Q4
model=Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf 
ctx_k=64
gpu_layers=99
cpu_moe=0
quant=q4_0/q4_0
spec=draft-mtp
draft_model=none
predict_token=1/5
jinja=0
batch=1024
ubatch=512
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |------------------ |
|  44 t/s |  80 k |   0 | 66/66 | 15.7 | 12.8/0.0  | q4_0 (q4_0) |    568 |  13s | MTP        min=4 max=4 p_min=0.20 (91%) |  1024/256 | R: medium         |
|  43 t/s |  80 k |   0 | 66/66 | 15.7 | 12.8/0.0  | q4_0 (q4_0) |    568 |  13s | MTP        min=3 max=4 p_min=0.20 (91%) |  1024/256 | R: medium         |
|  43 t/s |  80 k |   0 | 66/66 | 15.7 | 12.8/0.0  | q4_0 (q4_0) |    568 |  13s | MTP        min=2 max=4 p_min=0.20 (91%) |  1024/256 | R: medium         |
|  33 t/s |  80 k |   0 | 66/66 | 15.7 | 12.8/0.0  | q4_0 (q4_0) |    584 |  18s | MTP        min=2 max=3 p_min=0.20 (95%) |  1024/256 | R: medium         |
|  33 t/s |  80 k |   0 | 66/66 | 15.7 | 12.8/0.0  | q4_0 (q4_0) |    584 |  17s | MTP        min=3 max=3 p_min=0.20 (93%) |   512/128 | R: medium         |
|  23 t/s |  80 k |   0 | 66/66 | 15.7 | 12.8/0.1  | q4_0 (q4_0) |    566 |  25s | MTP        min=1 max=4 p_min=0.20 (87%) |  1024/512 | R: medium         |
|  21 t/s |  80 k |   0 | 66/66 | 15.7 | 12.8/0.1  | q4_0 (q4_0) |    570 |  28s | MTP        min=2 max=5 p_min=0.20 (88%) |  1024/512 | R: medium         |
|  20 t/s |  80 k |   0 | 66/66 | 15.7 | 12.8/0.0  | q4_0 (q4_0) |    584 |  30s | MTP        min=5 max=6 p_min=0.20 (84%) |  1024/256 | R: medium         |
|  20 t/s |  80 k |   0 | 66/66 | 15.7 | 12.8/0.2  | q4_0 (q4_0) |    560 |  28s | MTP        min=3 max=4 p_min=0.20 (90%) | 2048/1024 | R: medium         |
|  20 t/s |  80 k |   0 | 66/66 | 15.7 | 12.8/0.2  | q4_0 (q4_0) |    560 |  28s | MTP        min=1 max=4 p_min=0.20 (90%) | 2048/1024 | R: medium         |
|  19 t/s |  80 k |   0 | 65/66 | 15.5 | 12.6/0.0  | q4_0 (q4_0) |    586 |  31s | MTP        min=2 max=2 p_min=0.20 (94%) |   512/128 | R: medium         |

|  42 t/s |  64 k |   0 | 66/66 | 15.7 | 12.8/0.1  | q4_0 (q4_0) |    566 |  14s | MTP        min=3 max=4 p_min=0.20 (87%) |  1024/512 | R: medium         |
|  44 t/s |  64 k |   0 | 66/66 | 15.5 | 12.8/0.0  | q4_0 (q4_0) |    568 |  13s | MTP        min=3 max=4 p_min=0.20 (91%) |  1024/256 | R: medium         |
|  33 t/s |  64 k |   0 | 66/66 | 15.4 | 12.8/0.1  | q4_0 (q4_0) |    544 |  16s | MTP        min=1 max=2 p_min=0.20 (96%) |  1024/512 | R: medium         |
|  33 t/s |  64 k |   0 | 66/66 | 15.2 | 12.8/0.0  | q4_0 (q4_0) |    584 |  18s | MTP        min=1 max=2 p_min=0.20 (96%) |  1024/256 | R: medium         |
|  38 t/s |  48 k |   0 | 66/66 | 15.1 | 12.8/0.1  | q4_0 (q4_0) |    544 |  15s | MTP        min=2 max=3 p_min=0.20 (95%) |  1024/512 | R: medium         |

# Q8
model=Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf 
ctx_k=53
gpu_layers=99
cpu_moe=0
quant=q8_0/q4_0
spec=draft-mtp
draft_model=none
predict_token=1/3
ngram_values=none
jinja=0
batch=1024
ubatch=256
_test_model


| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |------------------ |
|  43 t/s |  48 k |   0 | 66/66 | 15.7 | 12.8/0.0  | q8_0 (q4_0) |    548 |  13s | MTP        min=4 max=4 p_min=0.20 (91%) |   512/128 | R: medium         |
|  43 t/s |  40 k |   0 | 66/66 | 15.7 | 12.8/0.0  | q8_0 (q8_0) |    584 |  14s | MTP        min=2 max=5 p_min=0.20 (85%) |  1024/256 | R: medium         |
|  42 t/s |  44 k |   0 | 66/66 | 15.6 | 12.8/0.0  | q8_0 (q4_0) |    611 |  14s | MTP        min=4 max=4 p_min=0.20 (89%) |  1024/256 | R: medium         |
|  42 t/s |  40 k |   0 | 66/66 | 15.5 | 12.8/0.0  | q8_0 (q4_0) |    611 |  14s | MTP        min=4 max=4 p_min=0.20 (89%) |  1024/256 | R: medium         |
|  42 t/s |  36 k |   0 | 66/66 | 15.3 | 12.8/0.0  | q8_0 (q4_0) |    611 |  15s | MTP        min=4 max=4 p_min=0.20 (89%) |  1024/256 | R: medium         |
|  42 t/s |  32 k |   0 | 66/66 | 15.2 | 12.8/0.1  | q8_0 (q4_0) |    585 |  14s | MTP        min=4 max=4 p_min=0.20 (90%) |  1024/512 | R: medium         |
|  25 t/s |  48 k |   0 | 66/66 | 15.7 | 12.7/0.1  | q8_0 (q4_0) |    602 |  24s | MTP        min=4 max=4 p_min=0.20 (91%) |  1024/512 | R: medium         |


|  34 t/s |  52 k |   0 | 66/66 | 15.7 | 12.8/0.0  | q8_0 (q4_0) |    548 |  16s | MTP        min=1 max=4 p_min=0.20 (91%) |  1024/128 | R: medium         |
|  33 t/s |  48 k |   0 | 66/66 | 15.6 | 12.8/0.0  | q8_0 (q5_0) |    548 |  16s | MTP        min=4 max=4 p_min=0.20 (90%) |   512/128 | R: medium         |

model=Qwen3.8-27B-IQ4_XS_unsloth.gguf 
ctx_k=48
gpu_layers=99
cpu_moe=0
spec=draft-mtp
draft_model=none
predict_token=1/3
jinja=0
batch=1024
ubatch=512
_test_model


| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |----------=======- |
|  25 t/s |  80 k |   0 | 66/66 | 15.7 | 12.8/0.1  | q4_0 (q4_0) |    570 |  24s | MTP        min=4 max=4 p_min=0.20 (88%) |  1024/512 | R: medium         |
|  16 t/s |  48 k |   0 | 66/66 | 15.7 | 14.0/0.1  | q4_0 (q4_0) |    610 |  39s | MTP        min=2 max=3 p_min=0.20 (90%) |  1024/512 |                   |
|  15 t/s |  48 k |   0 | 66/66 | 15.7 | 14.0/0.1  | q4_0 (q4_0) |    602 |  39s | MTP        min=1 max=2 p_min=0.20 (95%) |  1024/512 | R: medium         |
|  32 t/s |  32 k |   0 | 66/66 | 15.7 | 14.0/0.1  | q4_0 (q4_0) |    602 |  19s | MTP        min=1 max=2 p_min=0.20 (95%) |  1024/512 | R: medium         |


```