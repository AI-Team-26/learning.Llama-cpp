# Inkling 2 Mini


| File                                                  | GB   | Result                                |
| ---                                                   | ---  | ---                                   |
| Inkling-2-Mini-Unbound-v3-IQ3_M_nokeeapi.gguf         | 12.3 | 15 t/s  40k                           |
| Inkling-2-Mini-UD-IQ3_XXS_unsloth.gguf                | 10.8 |                                       |
| Inkling-2-Mini-Prime-IQ3_M_nokeepai.gguf              | 12.3 |                                       |
| thinkingmachines_Inkling-2-Mini-IQ3_XS_bartowski.gguf | 


##
Inkling-2-Mini-UD-IQ3_XXS_unsloth.gguf                  10.8 GB
X Requires special llama.cpp build !!

## 
thinkingmachines_Inkling-2-Mini-IQ3_XS_bartowski.gguf   12.8 GB


```bash

model=Inkling-2-Mini-Prime-IQ3_M_nokeepai.gguf
ctx_k=48
gpu_layers=99
cpu_moe=3
quant=q4_0/q4_0
spec=ngram-simple
draft_model=none
predict_token=1/5
ngram_values=16/16
jinja=0
batch=1024
ubatch=512
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                       | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | -------------------------------------------- | --------- |------------------ |
|   9 t/s |  48 k |   2 | 49/49 | 15.7 | 11.2/0.2  | q4_0 (q4_0) |    737 |  79s | 15% = N-gram 16/16/1 (100%)                  |  1024/512 |                   |


model=Inkling-2-Mini-Unbound-v3-IQ3_M_nokeeapi.gguf
ctx_k=48
gpu_layers=99
cpu_moe=2
quant=q4_0/q4_0
spec=ngram-simple
draft_model=none
predict_token=1/5
ngram_values=16/16
jinja=0
batch=1024
ubatch=512
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                       | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | -------------------------------------------- | --------- |------------------ |
|  15 t/s |  40 k |   3 | 49/49 | 15.5 | 10.7/0.1  | q4_0 (q4_0) |    705 |  48s | 15% = N-gram 16/16/1 (100%)                  |  1024/512 |                   |
|  15 t/s |  40 k |   5 | 49/49 | 14.5 | 9.7/0.1   | q4_0 (q4_0) |    679 |  45s | --                                           |  1024/512 |                   |
|  13 t/s |  40 k |   4 | 49/49 | 15.0 | 10.2/0.1  | q4_0 (q4_0) |    674 |  50s | 15% = N-gram 16/16/1 (100%)                  |  1024/512 |                   |
|  11 t/s |  40 k |   0 | 49/49 | 15.7 | 12.2/0.1  | q8_0 (q8_0) |    711 |  64s | --                                           |  1024/512 |                   |


model=thinkingmachines_Inkling-2-Mini-IQ3_XS_bartowski.gguf
ctx_k=40
gpu_layers=99
cpu_moe=0
quant=q8_0/q4_0
spec=ngram-mod
draft_model=none
predict_token=1/3
jinja=0
batch=1024
ubatch=512
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                       | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | -------------------------------------------- | --------- |------------------ |
|   7 t/s |  40 k |   0 | 49/49 | 15.7 | 12.4/0.1  | q8_0 (q8_0) |    710 |  97s | --                                           |  1024/512 |                   |



model=Inkling-2-Mini-UD-IQ3_XXS_unsloth.gguf
ctx_k=64
gpu_layers=99
cpu_moe=0
quant=q8_0/q8_0
spec=draft-dspark
draft_model=none
predict_token=1/3
jinja=1
batch=1024
ubatch=512
_test_model

```