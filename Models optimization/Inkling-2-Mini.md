# Inkling 2 Mini


| File                                                  | GB   | Result                                |
| ---                                                   | ---  | ---                                   |
| Inkling-2-Mini-UD-IQ3_XXS_unsloth.gguf                | 10.8 |                                       |
| thinkingmachines_Inkling-2-Mini-IQ3_XS_bartowski.gguf | 


##
Inkling-2-Mini-UD-IQ3_XXS_unsloth.gguf                  10.8 GB
X Requires special llama.cpp build !!

## 
thinkingmachines_Inkling-2-Mini-IQ3_XS_bartowski.gguf   12.8 GB


```bash

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