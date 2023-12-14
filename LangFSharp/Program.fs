module Program
open basic

let programs = Map.ofList [
    "basic.add", fun args -> 
        match args with
        | [| arg1; arg2 |] -> basic.add (int arg1) (int arg2)
        | _ -> failwith "invalid number of arguments for basic.add"
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
