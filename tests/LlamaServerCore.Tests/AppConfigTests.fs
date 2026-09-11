module LlamaServerCore.Tests.AppConfigTests

open LlamaServerCore
open NUnit.Framework
open Swensen.Unquote

let tempFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"llama_cfg_{System.Guid.NewGuid():N}.json")

[<TestFixture>]
type AppConfigTests() =

    [<TearDown>]
    member _.cleanup() =
        if System.IO.File.Exists(tempFile) then
            System.IO.File.Delete(tempFile)

    [<Test>]
    member _.load_missing_file_returns_defaults() =
        let cfg = AppConfig.Load tempFile
        let gguf = @"L:\GGUF"
        let yaml = "scripts/models_config.yaml"
        test <@ cfg.GgufFolder = gguf @>
        test <@ cfg.Port = 8001 @>
        test <@ cfg.LlamaBinsFolder = "" @>
        test <@ cfg.LastModelId = "" @>
        test <@ cfg.ModelsConfigPath = yaml @>
    [<Test>]
    member _.save_and_load_round_trip_all_fields() =
        let bins = @"D:\Standalone Programs\llama-bXXXX-bin-win-cuda-12.4-x64"
        let gguf = @"M:\Models"
        let modelId = "qwen3-27b"
        let yaml = "scripts/models_config.yaml"
        let cfg = AppConfig(LlamaBinsFolder = bins, GgufFolder = gguf, Port = 9000, LastModelId = modelId)
        cfg.Save(tempFile)
        let loaded = AppConfig.Load(tempFile)
        test <@ loaded.LlamaBinsFolder = bins @>
        test <@ loaded.GgufFolder = gguf @>
        test <@ loaded.Port = 9000 @>
        test <@ loaded.LastModelId = modelId @>
        test <@ loaded.ModelsConfigPath = yaml @>
    [<Test>]
    member _.load_partial_json_fills_missing_fields_with_defaults() =
        System.IO.File.WriteAllText(tempFile, """{"Port": 9000}""")
        let cfg = AppConfig.Load(tempFile)
        let gguf = @"L:\GGUF"
        test <@ cfg.Port = 9000 @>
        test <@ cfg.GgufFolder = gguf @>
        test <@ cfg.LlamaBinsFolder = "" @>
    [<Test>]
    member _.load_corrupt_json_throws_descriptive_error() =
        System.IO.File.WriteAllText(tempFile, "{ this is not json ")
        let ex =
            try
                ignore (AppConfig.Load tempFile)
                null
            with e -> e
        test <@ ex <> null @>
        let ioex : System.Exception = ex
        test <@ ioex.Message.Contains(tempFile) @>
