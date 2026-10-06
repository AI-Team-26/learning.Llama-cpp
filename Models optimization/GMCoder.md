# GMCoder


```bash
model=gmcoder.Q8_0_emperorofrome.gguf
ctx_k=256
gpu_layers=99
cpu_moe=0
quant=q8_0
spec=ngram-simple
draft_model=none
predict_token=1/4
ngram_values=24/24
jinja=1
batch=2048
ubatch=512
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                       | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | -------------------------------------------- | --------- |------------------ |
|  27 t/s | 256 k |   0 | 34/34 | 13.6 | 7.9/0.3   | q8_0 (none) |    507 |  19s | --                                           |  2048/512 |                   |
|  26 t/s | 256 k |   0 | 34/34 | 13.6 | 7.9/0.3   | q8_0 (none) |    507 |  20s | 8% = N-gram 16/16/1 (100%)                   |  2048/512 |                   |
|  27 t/s | 128 k |   0 | 34/34 | 10.9 | 7.9/0.1   | q8_0 (none) |    507 |  19s | --                                           |  1024/512 |                   |
|  27 t/s |  64 k |   0 | 34/34 |  9.5 | 7.9/0.1   | q8_0 (none) |    507 |  18s | --                                           |  1024/512 |                   |


```
