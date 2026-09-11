module LlamaServerCore.Tests.ArgBuilderTests

open NUnit.Framework
open Swensen.Unquote
open LlamaServerCore

let fixedArgs port =
    [ "--host"; "127.0.0.1"
      "--port"; string port
      "--parallel"; "1"
      "--prio"; "3"
      "--n-cpu-ffn"; "0"
      "--flash-attn"; "on"
      "--kv-unified"
      "--load-mode"; "mmap"
      "--fit"; "off"
      "--no-mmproj"
      "--agent"
      "--cache-reuse"; "64"
      "--ctx-checkpoints"; "4"
      "--checkpoint-min-step"; "16384"
      "--spec-draft-p-min"; "0.2"
      "--log-verbosity"; "4"
      "--samplers"; "penalties;dry;top_k;top_p;min_p;temperature"
      "--temperature"; "0.3"
      "--top-k"; "20"
      "--top-p"; "0.85"
      "--min-p"; "0.02"
      "--repeat-penalty"; "1.10"
      "--repeat-last-n"; "512"
      "--reasoning-preserve"
      "--reasoning"; "on"
      "--reasoning-budget"; "4096"
      "--reasoning-budget-message"; "... Considering the limited time by the user, I have to give the solution based on the thinking directly now." ]

let expectArgError (f: unit -> unit) =
    Assert.Throws<ArgBuildException>(System.Action f)

[<Test>]
let ``draft-mtp model produces exact expected args`` () =
    let m = ModelConfig()
    m.File <- "Qwen3.8-27B-abliterated-UD-IQ4_XS_huihui.gguf"
    m.CtxK <- 64
    m.GpuLayers <- 99
    m.CpuMoe <- 0
    m.Quant <- "q4_0"
    m.Batch <- 1024
    m.UBatch <- 512
    m.SpecType <- "draft-mtp"
    m.SpecDraftNMin <- 1
    m.SpecDraftNMax <- 4
    m.Jinja <- 0
    let expected =
        fixedArgs 8001
        @ [ "--cache-type-k"; "q4_0"
            "--cache-type-v"; "q4_0"
            "--cache-type-k-draft"; "q4_0"
            "--cache-type-v-draft"; "q4_0"
            "--model"; System.IO.Path.Combine(@"L:\GGUF", "Qwen3.8-27B-abliterated-UD-IQ4_XS_huihui.gguf")
            // no alias in yaml -> falls back to model id
            "--alias"; "Qwen3.8-27B-abliterated-UD-IQ4_XS_huihui_64k"
            "--ctx-size"; "65536"
            "--n-gpu-layers"; "99"
            "--n-cpu-moe"; "0"
            "--batch-size"; "1024"
            "--ubatch-size"; "512"
            "--spec-type"; "draft-mtp"
            // cpu_moe == 0 and gpu_layers == 99 -> cache ram disabled
            "--cache-ram"; "0"
            "--spec-draft-n-min"; "1"
            "--spec-draft-n-max"; "4" ]
    let actual = ServerArgs.Build("Qwen3.8-27B-abliterated-UD-IQ4_XS_huihui_64k", 8001, m, @"L:\GGUF") |> List.ofSeq
    test <@ actual = expected @>

[<Test>]
let ``ngram-simple model with all options produces exact expected args`` () =
    let m = ModelConfig()
    m.File <- "some-model.gguf"
    m.Alias <- "my-alias"
    m.CtxK <- 128
    m.GpuLayers <- 99
    m.CpuMoe <- 7
    m.Quant <- "q8_0/q4_0"
    m.Batch <- 1024
    m.UBatch <- 512
    m.SpecType <- "ngram-simple"
    m.SpecNgramSimpleSizeN <- 8
    m.SpecNgramSimpleSizeM <- 8
    m.SpecNgramSimpleMinHits <- 1
    m.DraftModel <- "mtp-draft.gguf"
    m.Jinja <- 1
    m.QwenReasoningEffortMedium <- 1
    let expected =
        fixedArgs 8001
        @ [ "--cache-type-k"; "q8_0"
            "--cache-type-v"; "q8_0"
            "--cache-type-k-draft"; "q4_0"
            "--cache-type-v-draft"; "q4_0"
            "--model"; System.IO.Path.Combine(@"L:\GGUF", "some-model.gguf")
            "--alias"; "my-alias"
            "--ctx-size"; "131072"
            "--n-gpu-layers"; "99"
            "--n-cpu-moe"; "7"
            "--batch-size"; "1024"
            "--ubatch-size"; "512"
            "--spec-type"; "ngram-simple"
            // cpu_moe != 0 -> cache ram enabled
            "--cache-ram"; "4096"
            "--spec-ngram-simple-size-n"; "8"
            "--spec-ngram-simple-size-m"; "8"
            "--spec-ngram-simple-min-hits"; "1"
            "--spec-draft-model"; System.IO.Path.Combine(@"L:\GGUF", "mtp-draft.gguf")
            "--jinja"
            "--chat-template-kwargs"; "{\"reasoning_effort\":\"medium\"}" ]
    let actual = ServerArgs.Build("model-id", 8001, m, @"L:\GGUF") |> List.ofSeq
    test <@ actual = expected @>

[<Test>]
let ``missing quant throws ArgBuildException`` () =
    let m = ModelConfig()
    m.File <- "x.gguf"
    m.CtxK <- 64
    m.GpuLayers <- 99
    m.CpuMoe <- 0
    m.Batch <- 1024
    m.UBatch <- 512
    m.SpecType <- "none"
    let ex = expectArgError (fun () -> ServerArgs.Build("id", 8001, m, @"L:\GGUF") |> ignore)
    test <@ ex.Message.Contains "quant" @>

[<Test>]
let ``missing spec_type throws ArgBuildException`` () =
    let m = ModelConfig()
    m.File <- "x.gguf"
    m.CtxK <- 64
    m.GpuLayers <- 99
    m.CpuMoe <- 0
    m.Quant <- "q4_0"
    m.Batch <- 1024
    m.UBatch <- 512
    let ex = expectArgError (fun () -> ServerArgs.Build("id", 8001, m, @"L:\GGUF") |> ignore)
    test <@ ex.Message.Contains "spec_type" @>

[<Test>]
let ``dflash spec type is not supported`` () =
    let m = ModelConfig()
    m.File <- "x.gguf"
    m.CtxK <- 64
    m.GpuLayers <- 99
    m.CpuMoe <- 0
    m.Quant <- "q4_0"
    m.Batch <- 1024
    m.UBatch <- 512
    m.SpecType <- "dflash"
    let ex = expectArgError (fun () -> ServerArgs.Build("id", 8001, m, @"L:\GGUF") |> ignore)
    test <@ ex.Message.Contains "DFlash" @>

[<Test>]
let ``draft-mtp without draft n min/max throws`` () =
    let m = ModelConfig()
    m.File <- "x.gguf"
    m.CtxK <- 64
    m.GpuLayers <- 99
    m.CpuMoe <- 0
    m.Quant <- "q4_0"
    m.Batch <- 1024
    m.UBatch <- 512
    m.SpecType <- "draft-mtp"
    let ex = expectArgError (fun () -> ServerArgs.Build("id", 8001, m, @"L:\GGUF") |> ignore)
    test <@ ex.Message.Contains "spec_draft_n_min" @>

[<Test>]
let ``ngram-simple without min hits throws`` () =
    let m = ModelConfig()
    m.File <- "x.gguf"
    m.CtxK <- 64
    m.GpuLayers <- 99
    m.CpuMoe <- 0
    m.Quant <- "q4_0"
    m.Batch <- 1024
    m.UBatch <- 512
    m.SpecType <- "ngram-simple"
    m.SpecNgramSimpleSizeN <- 8
    m.SpecNgramSimpleSizeM <- 8
    let ex = expectArgError (fun () -> ServerArgs.Build("id", 8001, m, @"L:\GGUF") |> ignore)
    test <@ ex.Message.Contains "spec_ngram_simple_min_hits" @>

[<Test>]
let ``missing ctx_k throws ArgBuildException`` () =
    let m = ModelConfig()
    m.File <- "x.gguf"
    m.GpuLayers <- 99
    m.CpuMoe <- 0
    m.Quant <- "q4_0"
    m.Batch <- 1024
    m.UBatch <- 512
    m.SpecType <- "none"
    let ex = expectArgError (fun () -> ServerArgs.Build("id", 8001, m, @"L:\GGUF") |> ignore)
    test <@ ex.Message.Contains "ctx_k" @>
