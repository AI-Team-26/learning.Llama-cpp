# North Mini Code

MTP: No

| File                                                       | GB   | Result                                      |
|------------------------------------------------------------| ---- |---------------------------------------------|
| North-Mini-Code-1.0-UD-IQ4_XS_unsloth.gguf                 | 14.1 | ✔️ 64k: 45 t/s  Good PR                     |



## 
https://huggingface.co/unsloth/North-Mini-Code-1.0-GGUF



```bash


model=North-Mini-Code-1.0-UD-IQ4_XS_unsloth.gguf
ctx_k=64
gpu_layers=99
cpu_moe=0
quant=q8_0/q4_0
spec=ngram-simple
draft_model=none
predict_token=1/2
ngram_values=12/12/1
jinja=1
batch=1024
ubatch=512
_test_model


| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                       | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | -------------------------------------------- | --------- |------------------ |
|  58 t/s |  64 k |   0 | 50/50 | 15.7 | 14.2/0.1  | q8_0 (q8_0) |    885 |  15s | --                                           |  1024/512 |                   |
|  57 t/s |  64 k |   0 | 50/50 | 15.7 | 14.2/0.1  | q8_0 (q8_0) |    885 |  15s | N-gram 24/24/1 (---)                         |  1024/512 |                   |
|  55 t/s |  64 k |   0 | 50/50 | 15.7 | 14.2/0.1  | q8_0 (q8_0) |    823 |  15s | 12% = N-gram 16/16/1 (67%)                   |  1024/512 |                   |
|  46 t/s |  64 k |   2 | 50/50 | 15.4 | 13.9/0.1  | q8_0 (q8_0) |    719 |  16s | --                                           |  1024/512 |                   |

|  57 t/s |  32 k |   0 | 50/50 | 15.2 | 14.2/0.0  | q8_0 (q8_0) |    885 |  16s | --                                           |  1024/512 |                   |



```