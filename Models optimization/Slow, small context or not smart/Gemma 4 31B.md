# Gemma 4 31B

Not reched the speed of 10 t/s !

## ❌ Q3_K_M (by otter)
Gemma4-31B-Q3_K_M_v3_otter.gguf                    13.7 GB


## ❌ Q3_K_M
https://huggingface.co/Jackrong/Gemopus-4-31B-it-GGUF
Gemopus-4-31B-it-Q3_K_M_jackrong.gguf               14.2GB

```bash

model=Gemma4-31B-Q3_K_M_v3_otter.gguf
ctx_k=32
gpu_layers=99
cpu_moe=3
quant=q8_0/q4_0
spec=none
draft_model=none
predict_token=0/0
jinja=1
batch=512
ubatch=128
_test_model

model=Gemopus-4-31B-it-Q3_K_M_jackrong.gguf
ctx_k=64
cpu_moe=5
gpu_layers=-1
spec=0
draft_model=none
predict_token=0/0
jinja=0
batch=1024
ubatch=256


```