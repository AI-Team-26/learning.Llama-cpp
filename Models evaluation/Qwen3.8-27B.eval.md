# Qwen3.8 27B

``https://github.com/AI-Team-26/learning.Llama-cpp/pull/<PR>``


Where not indicated, the default KV is Q8_Q4.  
- --cache-type-k/v       q8_0  
- --cache-type-k/v-draft q4_0  



PR  Model                                                    Temp  Ubatch  Test code   Var name   TODO   SOLVED   Note
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

69  Qwen3.8-27B_UD-Q3-K-XL_[Unsloth]_Q4_[80k]                0.3   512     --          yes        yes    ✔️       underscore prefix for funcion name

23  Swift-1.5-Qwen3.8-27B-IQ4_XS_[ukisai]                                  no          yes        yes    no

81  Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[52k]_ngram              0.5   512     no          yes        yes    no
80  Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[52k]_ngram              0.3   512     no          yes        yes    no

72  Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[52k]                    0.3   512     no          yes        yes    ✔️
72  Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[52k]                    0.3   512     no          yes        yes    ✔️
71  Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[52k]                    0.3   512     no          yes        yes    ✔️
70  Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[52k]                    0.3   512     no          yes        yes    no
62  Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_Q8_Q8_[52k]              0.7           --          yes        yes    no
61  Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[56k]                    0.7           --          yes        yes    no      
60  Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[56k]                    0.2           --          yes        yes    ✔️      Asked about ambiguity. Not the best solution.
59  Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[56k]                    0.2           no          yes        yes    no       Completely wrong solution!
51  Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[56k]                    0.5           yes         yes        yes    ✔️      Fast (MTP max 2)
50  Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_Q8_Q8_[48k]_ngram        0.7           --          yes        no     ✔️      convoluted code, slow
49  Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_Q8_Q8_[48k]_ngram        0.7           yes         yes        yes    no       Aded ngram-simple
48  Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[48k]_ngram              0.7           --          yes        yes    no      
47  Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[48k]_ngram              0.7           --          yes        yes    no
46  Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_Q8_Q8_[48k]              0.7           --          yes        yes    ✔️       Nice rename of functions. Took LONG time.
45  Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_Q4_Q4_[64k]              0.7           yes         yes        no                  yes    no     
25  Qwen3.8_27B_UD-IQ4_XS_[Unsloth]_[40k]                                  yes         yes        no                  yes    ✔️


74  Swift-Qwen3.8-27B-IQ4_XS_[ahmeddelkilami01]_[64k]        0.2    512    --          no         yes    no       FAST  wrong URL generated in the description
73  Swift-Qwen3.8-27B-IQ4_XS_[ahmeddelkilami01]_[64k]        0.2    512    --          no         yes    no       FAST
58  Swift-Qwen3.8-27B-IQ4_XS_[ahmeddelkilami01]_[64k]        0.2    256    --          no         yes    no       FAST
57  Swift-Qwen3.8-27B-IQ4_XS_[ahmeddelkilami01]_[64k]        0.2    256    --          no         yes    no       FAST
56  Swift-Qwen3.8-27B-IQ4_XS_[ahmeddelkilami01]_[64k]        0.7    256    --          no         yes    no       FAST
55  Swift-Qwen3.8-27B-IQ4_XS_[ahmeddelkilami01]_[64k]        0.5    256    --          no         yes    no       FAST
54  Swift-Qwen3.8-27B-IQ4_XS_[ahmeddelkilami01]_[64k]        0.5    256    --          yes        yes    no       FAST
53  Swift-Qwen3.8-27B-IQ4_XS_[ahmeddelkilami01]_[64k]        0.5    256    --          yes        yes    no       FAST
53  Swift-Qwen3.8-27B-IQ4_XS_[ahmeddelkilami01]_[64k]        0.5           --          yes        yes    no       FAST
52  Swift-Qwen3.8-27B-IQ4_XS_[ahmeddelkilami01]_[64k]        0.5           --          yes        yes    no       FAST
33  Swift-Qwen3.8-27B-IQ4_XS_[ahmeddelkilami01]_[64k]_ngram                --          yes        yes    ✔️
32  Swift-Qwen3.8-27B-IQ4_XS_[ahmeddelkilami01]_[64k]_ngram                yes         yes        yes    ✔️      tested with dry-run (no pycache?) FAST
28  Swift-Qwen3.8-27B-IQ4_XS_[ahmeddelkilami01]_[64k]                      no          no         yes    no

76  Swift-1.5-Qwen3.8-27b.i1-IQ4_XS_[mrradermacher]_Q4_[48k] 1.0    512    no          no         no     no       ❌ Rubbish  fast
76  Swift-1.5-Qwen3.8-27b.i1-IQ4_XS_[mrradermacher]_Q4_[48k] 1.0    512    no          no         yes     no       

42  Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_[finex666]_[64k]     0.7           yes         yes        yes    ✔️
26  Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_[finex666]_[64k]                   no          yes        ❌     no
      
27  Qwen3.8-27B-abliterated-UD-IQ4_XS_[huihui]_[64k]                       yes         yes        yes    no

41  Qwen3.8-27B-IQ4_XS_(byteshape)_[56k]                     0.7    256    yes         yes        yes    ✔️      
40  Qwen3.8-27B-IQ4_XS_(byteshape)_[56k]                            256    --          yes        yes    no       Note (no change) saying the code doesn't work
39  Qwen3.8-27B-IQ4_XS_(byteshape)_[56k]                            256    --          no         yes    no
35  Qwen3.8-27B-IQ4_XS_(byteshape)_[64k]                            256    --          yes        yes    no
34  Qwen3.8-27B-IQ4_XS_(byteshape)_[64k]                            256    --          yes        yes    no        
30  Qwen3.8-27B-IQ4_XS_(byteshape)_[56k]                                   yes         yes        yes    ✔️      
29  Qwen3.8-27B-IQ4_XS_(byteshape)_[56k]                                   yes         0.5        yes    no


31  North-Mini-Code-1.0-UD-IQ4_XS_[Unsloth]_Q8_Q8_[64k]                    no          no         ❌     no    

44  Swift-1.5-Qwen3.8-27B-MiniPlus-V3_isvalorum.gguf                256    no          yes        yes    no     
43  Swift-1.5-Qwen3.8-27B-MiniPlus-V3_isvalorum.gguf                256    no          yes        ❌     no         
42  Swift-1.5-Qwen3.8-27B-MiniPlus-V3_isvalorum.gguf         0.7    256    no          yes        ❌     no      temperature 0.7    ❌reused old PR
42  Swift-1.5-Qwen3.8-27B-MiniPlus-V3_isvalorum.gguf         0.7    256    no          yes        ❌     no      temperature 0.7    ❌reused old PR
38  Swift-1.5-Qwen3.8-27B-MiniPlus-V3_isvalorum.gguf                256    --          no         ❌     no
37  Swift-1.5-Qwen3.8-27B-MiniPlus-V3_isvalorum.gguf                256    --          yes        yes    no
36  Swift-1.5-Qwen3.8-27B-MiniPlus-V3_isvalorum.gguf                256                         --          no         yes    no


Note.
Swift-1.5-Qwen3.8-27B-APEX-I-MiniPlus-V3_isvalorum.gguf renamed Swift-1.5-Qwen3.8-27B-MiniPlus-V3_isvalorum.gguf