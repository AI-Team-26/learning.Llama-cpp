module LlamaServerCore.Tests.ModelsConfigLoaderTests

open System
open System.IO
open NUnit.Framework
open Swensen.Unquote
open LlamaServerCore

let sampleYaml = """
models:
  model-a:
    file: a.gguf
    ctx_k: 64
    gpu_layers: 99
    cpu_moe: 7
    batch : 512
    ubatch: 256
    quant: q8_0/q8_0
    spec_type: none
    note: ...
    pi_agent_model: >
      { "_test": "ok", "id": "model-a" }
"""

[<TestFixture>]
type ModelsConfigLoaderTests() =
    let tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"))

    let writeFile name content =
        Directory.CreateDirectory(tempDir) |> ignore
        let p = Path.Combine(tempDir, name)
        File.WriteAllText(p, (content : string))
        p

    [<Test>]
    member _.``Loads models map with required fields and ignores unknown keys``() =
        let path = writeFile "models.yaml" sampleYaml
        let models = ModelsConfigLoader.Load(path)
        test <@ models.Count = 1 @>
        let m = models.["model-a"]
        test <@ m.File = "a.gguf" @>
        test <@ m.CtxK = 64 @>
        test <@ m.GpuLayers = 99 @>
        test <@ m.CpuMoe = 7 @>
        test <@ m.Batch = 512 @>
        test <@ m.UBatch = 256 @>
        test <@ m.Quant = "q8_0/q8_0" @>
        test <@ m.SpecType = "none" @>

    [<Test>]
    member _.``Optional keys are null when absent``() =
        let yaml = """
models:
  bare:
    file: b.gguf
    ctx_k: 8
    gpu_layers: 30
    cpu_moe: 0
    batch: 128
    ubatch: 64
    quant: f16
    spec_type: none
"""
        let path = writeFile "bare.yaml" yaml
        let m = ModelsConfigLoader.Load(path).["bare"]
        Assert.That(m.Alias, Is.Null)
        Assert.That(m.DraftModel, Is.Null)
        Assert.That(m.Jinja, Is.Null)
        Assert.That(m.QwenReasoningEffortMedium, Is.Null)

    [<Test>]
    member _.``Missing file throws FileNotFoundException``() =
        Assert.Throws<FileNotFoundException>(System.Action (fun _ -> ModelsConfigLoader.Load(Path.Combine(tempDir, "nope.yaml")) |> ignore)) |> ignore

    [<Test>]
    member _.``Invalid yaml throws InvalidOperationException``() =
        let path = writeFile "bad.yaml" "models:\n  broken: [unclosed\n"
        Assert.Throws<InvalidOperationException>(System.Action (fun _ -> ModelsConfigLoader.Load(path) |> ignore)) |> ignore
