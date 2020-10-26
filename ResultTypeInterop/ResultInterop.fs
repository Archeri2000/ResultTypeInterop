namespace ResultTypeInterop

open System.Threading.Tasks
open CSharp_Result
open System.Runtime.CompilerServices

module ResultInterop =
    // Result
    [<Extension>]
    type ResultExtensions =
        /// <summary>
        /// Converts a C# Result to an F# Result
        /// </summary>
        /// <param name="result">C# result to convert</param>
        /// <typeparam name="'T">Success type of result</typeparam>
        /// <returns>F# Result</returns>
        [<Extension>]
        static member inline ToFSharpResult (result: Result<'T>) =
            result.Match(Ok, Error)
            
    /// <summary>
    /// Converts a F# Result to a C# Result
    /// </summary>
    /// <param name="result">F# result to convert</param>
    /// <typeparam name="'T">Success type of result</typeparam>
    /// <returns>C# Result</returns>
    let ToCSharpResult (result:Result<'T, exn>): Result<'T> =
        match result with
        |Ok success -> success :> Result<'T>
        |Error failure -> new Failure<'T>(failure) :> Result<'T>
        
    // Result Collection
    [<Extension>]
    type ResultListExtensions =
        /// <summary>
        /// Converts a C# Result IEnumerable to a F# Result List
        /// </summary>
        /// <param name="list">C# Result IEnumerable to convert</param>
        /// <typeparam name="'T">Success type of result</typeparam>
        /// <returns>F# result list</returns>
        [<Extension>]
        static member inline ToFSharpResultList (list: seq<Result<'T>>) =
            list
            |> Seq.map (fun x -> x.ToFSharpResult())
            |> Seq.toList
            
    /// <summary>
    /// Converts a F# Result List to a C# Result IEnumerable
    /// </summary>
    /// <param name="list">F# result list to convert</param>
    /// <typeparam name="'T">Success type of result</typeparam>
    /// <returns>C# Result IEnumerable</returns>
    let ToCSharpResultList (list:Result<'T, exn> list) =
        list
        |> List.map ToCSharpResult
        |> Seq.ofList
        
    // Async Result Type       
    [<Extension>]  
    type AsyncResultExtensions =
        /// <summary>
        /// Converts a C# Result Task (Async Result) to an Async F# Result
        /// </summary>
        /// <param name="task">C# Result Task (Async Result) to convert</param>
        /// <typeparam name="'T">Success type of result</typeparam>
        /// <returns>Async F# result</returns>
        [<Extension>]
        static member ToFSharpAsyncResult (task: Task<Result<'T>>) =
            AsyncInterop.asAsyncT (task.ContinueWith(fun (x:Task<Result<'T>>) -> x.Result.ToFSharpResult()), None);
            
    /// <summary>
    /// Converts an Async F# Result to a C# Result Task (Async Result)
    /// </summary>
    /// <param name="task">Async F# result to convert</param>
    /// <typeparam name="'T">Success type of result</typeparam>
    /// <returns>C# Result Task (Async Result)</returns>
    let ToCSharpAsyncResult (task: Async<Result<'T,exn>>) : Task<Result<'T>> =
        let mapped = async{
            let! result = task
            return ToCSharpResult result
        }
        mapped.AsTask()
        
    // Async Result Collection
    [<Extension>]  
    type AsyncResultListExtensions =
        /// <summary>
        /// Converts a C# Result Task (Async Result) IEnumerable to an Async F# Result List
        /// </summary>
        /// <param name="list">C# Result Task (Async Result) IEnumerable to convert</param>
        /// <typeparam name="'T">Success type of result</typeparam>
        /// <returns>Async F# result list</returns>
        [<Extension>]
        static member ToFSharpAsyncResultList (list: seq<Task<Result<'T>>>) =
            list
            |> Seq.map (fun x -> x.ToFSharpAsyncResult())
            |> Seq.toList

    /// <summary>
    /// Converts an Async F# Result List to a C# Result Task (Async Result) IEnumerable
    /// </summary>
    /// <param name="list">Async F# result list to convert</param>
    /// <typeparam name="'T">Success type of result</typeparam>
    /// <returns>C# Result Task (Async Result) IEnumerable</returns>
    let ToCSharpAsyncResultList (list: Async<Result<'T,exn>> list) =
        list
        |> List.map ToCSharpAsyncResult
        |> Seq.ofList