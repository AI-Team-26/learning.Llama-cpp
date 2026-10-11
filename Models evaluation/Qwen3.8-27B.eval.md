# Qwen3.8 27B

``https://github.com/AI-Team-26/learning.Llama-cpp/pull/<PR>``


Where not indicated, the default KV is Q8_Q4.  
- --cache-type-k/v       q8_0  
- --cache-type-k/v-draft q4_0  


PR  Model                                                  Temp   Tested code   Var name   gitignore pycache   TODO   SOLVED   Note
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
23  Swift-1.5-Qwen3.8-27B-IQ4_XS_(ukisai)                         no            yes        --                  yes    no
25  Qwen3.8_27B_UD-IQ4_XS_(Unsloth)_[40k]                         yes           yes        no                  yes    ✔️
45  Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_Q4_Q4_[64k]            0.7    yes           yes        no                  yes    no     
46  Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_Q8_Q8_[48k]            0.7    --            yes        no                  yes    ✔️      Nice rename of functions. Took LONG time.
47  Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[48k]_ngram            0.7    --            yes        no                  yes    no
48  Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[48k]_ngram            0.7    --            yes        no                  yes    no      
49  Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_Q8_Q8_[48k]_ngram      0.7    yes           yes        no                  yes    no       Aded ngram-simple

26  Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_(finex666)_[64k]          no            yes        --                  ❌     no
27  Qwen3.8-27B-abliterated-UD-IQ4_XS_(huihui)_[64k]              yes           yes        no                  yes    no
28  Swift-Qwen3.8-27B-IQ4_XS_(ahmeddelkilami01)_[64k]             no            no         --                  yes    no
29  Qwen3.8-27B-IQ4_XS_(byteshape)_[56k]                          Yes           0.5        No                  yes    no
30  Qwen3.8-27B-IQ4_XS_(byteshape)_[56k]                          yes           yes        Yes                 yes    ✔️      
31  North-Mini-Code-1.0-UD-IQ4_XS_(unsloth)_[64]                  no            no         --                  ❌     no      
32  Swift-Qwen3.8-27B-IQ4_XS_(ahmeddelkilami01)_[64k]             yes           yes        --                  yes    ✔️      tested with dry-run (no pycache?) FAST
33  Swift-Qwen3.8-27B-IQ4_XS_(ahmeddelkilami01)_[64k]             --            yes        --                  yes    ✔️
34  Qwen3.8-27B-IQ4_XS_(byteshape)_[64k]                          --            yes        --                  yes    no        
35  Qwen3.8-27B-IQ4_XS_(byteshape)_[64k]                          --            yes        yes                 yes    no   
39  Qwen3.8-27B-IQ4_XS_(byteshape)_[56k]                          --            no         no                  yes    no
40  Qwen3.8-27B-IQ4_XS_(byteshape)_[56k]                          --            yes        no                  yes    no       Note (no change) saying the code doesn't work
41  Qwen3.8-27B-IQ4_XS_(byteshape)_[56k]                   0.7    yes           yes        no                  yes    ✔️      
42  Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]     0.7    yes           yes        no                  yes    ✔️      


36  Swift-1.5-Qwen3.8-27B-MiniPlus-V3_isvalorum.gguf              --            no         --                  yes    no
37  Swift-1.5-Qwen3.8-27B-MiniPlus-V3_isvalorum.gguf              --            yes        yes                 yes    no
38  Swift-1.5-Qwen3.8-27B-MiniPlus-V3_isvalorum.gguf              --            no         --                  ❌     no
42  Swift-1.5-Qwen3.8-27B-MiniPlus-V3_isvalorum.gguf       0.7    no            yes        no                  ❌     no      temperature 0.7    ❌reused old PR
42  Swift-1.5-Qwen3.8-27B-MiniPlus-V3_isvalorum.gguf       0.7    no            yes        no                  ❌     no      temperature 0.7    ❌reused old PR
43  Swift-1.5-Qwen3.8-27B-MiniPlus-V3_isvalorum.gguf              no            yes        no                  ❌     no       
44  Swift-1.5-Qwen3.8-27B-MiniPlus-V3_isvalorum.gguf              no            yes        no                  yes    no       


Note.
Swift-1.5-Qwen3.8-27B-APEX-I-MiniPlus-V3_isvalorum.gguf renamed Swift-1.5-Qwen3.8-27B-MiniPlus-V3_isvalorum.gguf