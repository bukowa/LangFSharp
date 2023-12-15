module Program
open basic
open basic.aasync

let programs = Map.ofList [
    "basic.add", fun args -> 
        match args with
        | [| arg1; arg2 |] -> basic.add (int arg1) (int arg2)
        | _ -> failwith "invalid number of arguments for basic.add"
    "aasync.add", fun args ->
        match args with
        | [| arg1; arg2 |] -> aasync.add (int arg1) (int arg2) |> Async.RunSynchronously
        | _ -> failwith "invalid number of arguments for aasync.add"
    "aasync.compute", fun args ->
        match args with
        | [| arg1; arg2 |] -> aasync.compute (int arg1) (int arg2) |> Async.RunSynchronously
        | _ -> failwith "invalid number of arguments for aasync.compute"
]

let exec program args =
    match Map.tryFind program programs with
    | Some f -> f args
    | None -> failwithf $"{program} not found" 

[<EntryPoint>]
let main argv =
    if argv.Length < 1 then
        printfn "usage: <module.func> [args]"
        1
    else
        exec argv.[0] argv.[1..]
        |> printfn "%A"
        0
