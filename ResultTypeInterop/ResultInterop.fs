namespace ResultTypeInterop

open System.Threading.Tasks
open CSharp_Result
open System.Runtime.CompilerServices

module ResultInterop =
    // Result
    [<Extension>]
    type ResultExtensions =
        
        [<Extension>]
        static member inline ToFSharpResult (result: Result<'T>) =
            result.Match(Ok, Error)
            
    let ToCSharpResult (result:Result<'T, exn>): Result<'T> =
        match result with
        |Ok success -> success :> Result<'T>
        |Error failure -> new Failure<'T>(failure) :> Result<'T>
        
    // Result Collection
    [<Extension>]
    type ResultListExtensions =
        [<Extension>]
        static member inline ToFSharpResultList (list: seq<Result<'T>>) =
            list
            |> Seq.map (fun x -> x.ToFSharpResult())
            |> Seq.toList
            
    let ToCSharpResultList (list:Result<'T, exn> list) =
        list
        |> List.map ToCSharpResult
        |> Seq.ofList
        
    // Async Result Type       
    [<Extension>]  
    type AsyncResultExtensions =
        [<Extension>]
        static member ToFSharpAsyncResult (task: Task<Result<'T>>) =
            AsyncInterop.asAsyncT (task.ContinueWith(fun (x:Task<Result<'T>>) -> x.Result.ToFSharpResult()), None);
            

    let ToCSharpAsyncResult (task: Async<Result<'T,exn>>) : Task<Result<'T>> =
        let mapped = async{
            let! result = task
            return ToCSharpResult result
        }
        mapped.AsTask()
        
    // Async Result Collection
    [<Extension>]  
    type AsyncResultListExtensions =
        [<Extension>]
        static member ToFSharpAsyncResultList (list: seq<Task<Result<'T>>>) =
            list
            |> Seq.map (fun x -> x.ToFSharpAsyncResult())
            |> Seq.toList

    let ToCSharpAsyncResultList (list: Async<Result<'T,exn>> list) =
        list
        |> List.map ToCSharpAsyncResult
        |> Seq.ofList