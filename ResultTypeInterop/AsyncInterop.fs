namespace ResultTypeInterop

open System
open System.Runtime.CompilerServices
open System.Threading
open System.Threading.Tasks

module private AsyncInterop =
    /// <summary>
    /// Converts a F# Async into a .NET Task
    /// </summary>
    /// <param name="async">Async function to convert</param>
    /// <typeparam name="'T">Return type of function</typeparam>
    /// <returns>.NET Task that holds the function</returns>
    let asTask(async: Async<'T>, token: CancellationToken option) =
        let tcs = TaskCompletionSource<'T>()
        let token = defaultArg token (CancellationToken());
        Async.StartWithContinuations(async,
                tcs.SetResult, tcs.SetException,
                tcs.SetException, token)
        tcs.Task
 
    /// <summary>
    /// Converts a .NET Task to an F# Async
    /// </summary>
    /// <param name="task">.NET task to convert</param>
    /// <param name="token">Cancellation Token for function</param>
    /// <returns>F# Async function</returns>
    let asAsync(task: Task, token: CancellationToken option) =
       Async.FromContinuations( 
                fun (completed, caught, canceled) ->
       let token = defaultArg token (CancellationToken())
       task.ContinueWith(Action<Task>(fun _ ->
                      if task.IsFaulted then caught(task.Exception)
                      else if task.IsCanceled then 
                         canceled(OperationCanceledException(token)|>raise)
                      else completed()), token)
                        |> ignore)

    /// <summary>
    /// Converts a .NET Task to an F# Async
    /// </summary>
    /// <param name="task">.NET task to convert</param>
    /// <param name="token">Cancellation Token for function</param>
    /// <typeparam name="'T">Return type of function</typeparam>
    /// <returns>F# Async function</returns>
    let asAsyncT(task: Task<'T>, token: CancellationToken option) =
        Async.FromContinuations( 
                 fun (completed, caught, canceled) ->
        let token = defaultArg token (CancellationToken()) 
        task.ContinueWith(Action<Task<'T>>(fun _ -> 
                        if task.IsFaulted then caught(task.Exception)
                        else if task.IsCanceled then  
                            canceled(OperationCanceledException(token) |> raise)
                        else completed(task.Result)), token) 
                        |> ignore)

[<Extension>]  
type AsyncInteropExtensions =
    /// <summary>
    /// Converts a .NET Task to an F# Async
    /// </summary>
    /// <param name="task">.NET task to convert</param>
    /// <typeparam name="'T">Return type of function</typeparam>
    /// <returns>F# Async function</returns>
    [<Extension>]
    static member AsAsync (task: Task<'T>) = AsyncInterop.asAsyncT (task, None) 

    /// <summary>
    /// Converts a .NET Task to an F# Async
    /// </summary>
    /// <param name="task">.NET task to convert</param>
    /// <param name="token">Cancellation Token for function</param>
    /// <typeparam name="'T">Return type of function</typeparam>
    /// <returns>F# Async function</returns>
    [<Extension>]
    static member AsAsync (task: Task<'T>, token: CancellationToken) =
        AsyncInterop.asAsyncT (task, Some token) 

    /// <summary>
    /// Converts a F# Async into a .NET Task
    /// </summary>
    /// <param name="async">Async function to convert</param>
    /// <typeparam name="'T">Return type of function</typeparam>
    /// <returns>.NET Task that holds the function</returns>
    [<Extension>]
    static member AsTask (async: Async<'T>) = AsyncInterop.asTask (async, None)

    /// <summary>
    /// Converts a F# Async into a .NET Task
    /// </summary>
    /// <param name="async">Async function to convert</param>
    /// <param name="token">Cancellation Token for function</param>
    /// <typeparam name="'T">Return type of function</typeparam>
    /// <returns>.NET Task that holds the function</returns>
    [<Extension>]
    static member AsTask (async: Async<'T>, token: CancellationToken) =
        AsyncInterop.asTask (async, Some token) 
