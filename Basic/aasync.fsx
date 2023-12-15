#load "aasync.fs"
open System
open basic.aasync

aasync.add 1 2 |> Async.StartImmediateAsTask |> printf "%A\n"
// System.Threading.Tasks.Task`1[System.Int32]

aasync.add 1 2 |> Async.StartChild |> printf "%A\n"
// Microsoft.FSharp.Control.FSharpAsync`1[Microsoft.FSharp.Control.FSharpAsync`1[System.Int32]]

let addprintfn x y =
    async {
        let! o = aasync.add x y
        printfn $"%d{o}"
        return o
    }

let multiaddprintf =
    [|1;2;3;4;5;6;7;8;9;0|]
    |> Seq.pairwise
    |> Seq.map (fun(x,y) -> addprintfn x y)
    |> Async.Parallel
    |> Async.RunSynchronously
    
multiaddprintf

let keepAdding x =
    async {
        while true do
            Random().Next(1,100)  |>
            aasync.add x |> Async.StartAsTask |> ignore
            printf $"{x}\n"
            do! Async.Sleep 1
        }

Seq.init 10000 keepAdding |> Async.Parallel |> Async.RunSynchronously |> ignore
