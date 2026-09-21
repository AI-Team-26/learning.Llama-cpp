# Tiel-Coder


| File                                                 | GB   | Result                                |
| ---                                                  | ---  | ---                                   |
| Cyber-Tiel-Coder-35B-A3B-MTP-UD-IQ4_XS_peculiar.gguf | 16.8 | X Slow. Too chatty.                   |
| Tiel-Coder-35B-A3B-UD-IQ4_XS_peculiar.gguf           |      |                                       |



##
Cyber-Tiel-Coder-35B-A3B-MTP-UD-IQ4_XS_peculiar.gguf             16.8 GB

## Tiel-Coder-35B-A3B (peculiar-ragdoll)
Tiel-Coder-35B-A3B-UD-IQ4_XS_peculiar.gguf        
https://huggingface.co/peculiar-ragdoll/Tiel-Coder-35B-A3B-GGUF
MTP: no
Fast but:
- long time/tokens
- many errors and got stuck with error "Response was truncated before completion."
- hasn't create the PR of feat/12. When asked to create, it did, but no link.


```bash

model=Cyber-Tiel-Coder-35B-A3B-MTP-UD-IQ4_XS_peculiar.gguf
ctx_k=60
gpu_layers=99
cpu_moe=7
quant=q8_0/q4_0
spec=draft-mtp
draft_model=none
predict_token=1/3
jinja=0
batch=512
ubatch=256
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |------------------ |
|  33 t/s |  64 k |   5 | 42/42 | 15.4 | 14.3/0.0  | q8_0 (none) |    490 |  14s | none                                 -- |  1024/256 |                   |
|  31 t/s |  64 k |   6 | 42/42 | 15.2 | 13.9/0.1  | q8_0 (none) |    541 |  17s | none                                 -- |  1024/512 |                   |
|  31 t/s |  64 k |   7 | 42/42 | 15.4 | 13.9/0.0  | q8_0 (q4_0) |    505 |  16s | MTP        min=1 max=2 p_min=0.20 (88%) |   512/256 |                   |
|  30 t/s |  64 k |   7 | 42/42 | 15.6 | 13.9/0.1  | q8_0 (q4_0) |    528 |  17s | MTP        min=1 max=2 p_min=0.20 (84%) |  1024/512 |                   |
|  22 t/s |  64 k |   4 | 42/42 | 15.7 | 14.6/0.1  | q8_0 (none) |    521 |  23s | none                                 -- |  1024/512 |                   |
|  20 t/s |  64 k |   5 | 42/42 | 15.7 | 14.6/0.0  | q8_0 (q4_0) |    480 |  24s | MTP        min=1 max=2 p_min=0.20 (91%) |  1024/256 |                   |
|  20 t/s |  64 k |   6 | 42/42 | 15.7 | 14.3/0.1  | q8_0 (q4_0) |    507 |  26s | MTP        min=1 max=2 p_min=0.20 (89%) |  1024/512 |                   |


model=Tiel-Coder-35B-A3B-UD-IQ4_XS_peculiar.gguf
ctx_k=64
gpu_layers=99
cpu_moe=5
spec=none
draft_model=none
predict_token=12/16
jinja=0
batch=1024
ubatch=256
_test_model

| Speed   | Ctx   | MoE | GPU   | VRAM | VRAM/RAM  | CH  (draft) | Tokens | Time | Speculative Prediction                  | Batch/Ub. | Note              |
| ------- | ----- | --- | ----- | ---- | --------- | ----------- | ------ | ---- | --------------------------------------- | --------- |------------------ |
|  39 t/s |  64 k |   3 | 41/41 | 15.7 | 15.0/0.0  | q4_0 (none) |    596 |  15s | none                                 -- |  1024/256 |                   |
|  35 t/s |  64 k |   4 | 41/41 | 15.5 | 14.6/0.0  | q4_0 (none) |    577 |  17s | none                                 -- |  1024/256 |                   |
|  33 t/s |  64 k |   5 | 41/41 | 15.4 | 14.3/0.0  | q8_0 (none) |    589 |  18s | none                                 -- |  1024/256 |                   |
|  30 t/s |  64 k |   6 | 41/41 | 15.1 | 13.9/0.0  | q8_0 (none) |    490 |  16s | none                                 -- |  1024/256 |                   |
|  29 t/s |  64 k |   5 | 40/41 | 15.1 | 14.2/0.0  | q4_0 (none) |    577 |  20s | none                                 -- |  1024/256 |                   |
|  28 t/s |  64 k |   7 | 41/41 | 14.8 | 13.6/0.0  | q8_0 (none) |    554 |  20s | none                                 -- |  1024/256 |                   |
|  28 t/s |  64 k |   4 | 39/41 | 15.4 | 14.5/0.0  | q4_0 (none) |    578 |  20s | none                                 -- |  1024/256 |                   |
|  27 t/s |  64 k |   6 | 40/41 | 15.1 | 13.9/0.0  | q8_0 (none) |    578 |  22s | none                                 -- |  1024/256 |                   |
|  26 t/s |  64 k |   4 | 41/41 | 15.7 | 14.6/0.0  | q8_0 (none) |    487 |  19s | none                                 -- |  1024/256 |                   |
|  25 t/s |  64 k |   4 | 41/41 | 15.7 | 14.6/0.0  | q8_0 (none) |    487 |  20s | none                                 -- |  1024/256 |                   |
|  25 t/s |  64 k |   4 | 38/41 | 15.3 | 14.5/0.0  | q4_0 (none) |    559 |  23s | none                                 -- |  1024/256 |                   |
|  22 t/s |  64 k |   5 | 38/41 | 14.9 | 14.1/0.0  | q4_0 (none) |    583 |  26s | none                                 -- |  1024/256 |                   |
|  21 t/s |  64 k |   3 | 37/41 | 15.2 | 14.5/0.0  | q4_0 (none) |    521 |  25s | none                                 -- |  1024/256 |                   |
|  21 t/s |  64 k |   0 | 37/41 | 15.2 | 14.5/0.0  | q4_0 (none) |    521 |  25s | none                                 -- |  1024/256 |                   |
|  21 t/s |  64 k |   2 | 37/41 | 15.2 | 14.5/0.0  | q4_0 (none) |    521 |  25s | none                                 -- |  1024/256 |                   |
|  20 t/s |  64 k |   5 | 37/41 | 14.9 | 14.1/0.0  | q4_0 (none) |    549 |  29s | none                                 -- |  1024/256 |                   |

|  28 t/s |  64 k |   3 | 41/41 | 15.7 | 15.0/0.0  | q4_0 (none) |    611 |  22s | N-gram            N=10 M=12 min=1 (40%) |  1024/256 |                   |
|  28 t/s |  64 k |   3 | 41/41 | 15.7 | 15.0/0.0  | q4_0 (none) |    618 |  22s | N-gram            N=10 M=10 min=1 (45%) |  1024/256 |                   |
|  26 t/s |  64 k |   3 | 41/41 | 15.7 | 15.0/0.0  | q4_0 (none) |    599 |  23s | N-gram              N=8 M=8 min=1 (56%) |  1024/256 |                   |
|  23 t/s |  64 k |   3 | 41/41 | 15.7 | 15.0/0.0  | q4_0 (none) |    665 |  28s | N-gram              N=6 M=8 min=1 (46%) |  1024/256 |                   |
|  18 t/s |  64 k |   3 | 41/41 | 15.7 | 15.0/0.0  | q4_0 (none) |    537 |  31s | N-gram              N=4 M=8 min=1 (37%) |  1024/256 |                   |
|  12 t/s |  64 k |   3 | 41/41 | 15.7 | 15.0/0.0  | q4_0 (none) |    551 |  45s | N-gram              N=2 M=4 min=1 (38%) |  1024/256 |                   |
|   9 t/s |  64 k |   3 | 41/41 | 15.7 | 15.0/0.0  | q4_0 (none) |    645 |  71s | N-gram              N=1 M=3 min=1 (24%) |  1024/256 |                   |

|  31 t/s |  32 k |   6 | 41/41 | 14.8 | 13.9/0.0  | q8_0 (none) |    490 |  15s | none                                 -- |   512/256 |                   |
|  29 t/s |  32 k |   7 | 41/41 | 14.4 | 13.6/0.0  | q8_0 (none) |    554 |  19s | none                                 -- |   512/256 |                   |


```