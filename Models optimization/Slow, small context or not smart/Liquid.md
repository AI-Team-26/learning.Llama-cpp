# Liquid

❌ Super fast but completely wrong.  
Neither able to find the TODO.  
Completely out of scope action... and many many actions...  
It said it has to create a branch and never created it.  
Totally uncapable to implement a simple feature that required to add ESlint and prettier to the repo of a Chrome extension.  


## LFM2.5 24B A2B Base
LFM2.5-24B-A2B-UD-Q4_K_XL_unsloth.gguf          13.8 GB


```bash

model=LFM2.5-24B-A2B-UD-Q4_K_XL_unsloth.gguf
ctx_k=96
gpu_layers=99
cpu_moe=0
quant=q8_0
spec=none
draft_model=none
predict_token=0/0
jinja=1
batch=1024
ubatch=1024
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                       | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | -------------------------------------------- | --------- |------------------ |
|  92 t/s |  96 k |   0 | 41/41 | 15.4 | 13.8/0.2  | q8_0 (none) |   1040 |  11s | --                                           | 1024/1024 |                   |
|  90 t/s |  96 k |   0 | 41/41 | 15.4 | 13.8/0.2  | q8_0 (none) |   1040 |  12s | --                                           | 2048/1024 |                   |
|  89 t/s |  96 k |   0 | 41/41 | 15.4 | 13.8/0.2  | q8_0 (none) |   1040 |  11s | --                                           | 2048/1024 |                   |
|  89 t/s |  96 k |   0 | 41/41 | 15.3 | 13.8/0.1  | q8_0 (none) |   1063 |  12s | --                                           |  1024/512 |                   |
|  91 t/s |  32 k |   0 | 41/41 | 14.5 | 13.8/0.0  | q8_0 (none) |   1063 |  12s | --                                           |  1024/512 | small checkpoints |
|  89 t/s |  32 k |   0 | 41/41 | 14.5 | 13.8/0.0  | q8_0 (none) |   1063 |  12s | --                                           |  1024/512 |                   |


```