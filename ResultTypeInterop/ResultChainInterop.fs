namespace ResultTypeInterop

open System
open System.Runtime.CompilerServices
open System.Threading.Tasks
open CSharp_Result
open ResultTypeInterop

[<Extension>]
type ResultFSExtension =
    //THEN
    
    /// <summary>
    /// If holding a Success, Executes the function with the result as input.
    /// Returns the result of the computation.
    /// </summary>
    /// <param name="res">Input Result</param>
    /// <param name="function">The function to execute</param>
    /// <typeparam name="TSucc">Input type</typeparam>
    /// <typeparam name="TResult">The type of the result of the computation</typeparam>
    /// <returns>Either a Success from the computation, or a Failure</returns>
    [<Extension>]
    static member Then (res: Result<'a>, fn: 'a -> Result<'b>) =
        res.Then(Func<'a, Result<'b>>(fn))
    
    /// <summary>
    /// If holding a Success, Executes the function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function, otherwise returns the result of the computation.
    /// </summary>
    /// <param name="res">Input Result</param>
    /// <param name="function">The function to execute</param>
    /// <param name="mapException">The mapping function for the error</param>
    /// <typeparam name="TSucc">Input type</typeparam>
    /// <typeparam name="TResult">The type of the result of the computation</typeparam>
    /// <returns>Either a Success from the computation, or a Failure</returns>   
    [<Extension>]
    static member Then (res: Result<'a>, fn: 'a -> 'b, mapErrors: exn -> exn) =
        res.Then(fn, mapErrors)
        
    /// <summary>
    /// If holding a Success, Executes the function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function, otherwise returns the result of the computation.
    /// </summary>
    /// <param name="res">Input Result</param>
    /// <param name="function">The function to execute</param>
    /// <param name="mapException">The mapping function for the error</param>
    /// <typeparam name="TSucc">Input type</typeparam>
    /// <typeparam name="TResult">The type of the result of the computation</typeparam>
    /// <returns>Either a Success from the computation, or a Failure</returns>   
    [<Extension>]
    static member Then (res: Result<'a>, fn: 'a -> 'b, mapErrors: Func<exn,exn>) =
        res.Then(fn, mapErrors)
      
    /// <summary>
    /// If holding a Success, Executes the function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function, otherwise returns the result of the computation.
    /// </summary>
    /// <param name="res">Input Result</param>
    /// <param name="function">The function to execute</param>
    /// <param name="mapException">The mapping function for the error</param>
    /// <typeparam name="TSucc">Input type</typeparam>
    /// <returns>Either a Success from the computation, or a Failure</returns>   
    [<Extension>]
    static member Then (res: Result<'a>, fn: 'a -> unit, mapErrors: exn -> exn) =
        let voidfn = Func<'a, Unit>(fn >> Unit);
        res.Then(voidfn, mapErrors)

    /// <summary>
    /// If holding a Success, Executes the function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function, otherwise returns the result of the computation.
    /// </summary>
    /// <param name="res">Input Result</param>
    /// <param name="function">The function to execute</param>
    /// <param name="mapException">The mapping function for the error</param>
    /// <typeparam name="TSucc">Input type</typeparam>
    /// <returns>Either a Success from the computation, or a Failure</returns>   
    [<Extension>]
    static member Then (res: Result<'a>, fn: 'a -> unit, mapErrors: Func<exn,exn>) =
        let voidfn = Func<'a, Unit>(fn >> Unit);
        res.Then(voidfn, mapErrors)

    /// <summary>
    /// If holding a Success, Executes the function with the result as input.
    /// Returns the result of the computation.
    /// </summary>
    /// <param name="res">Input Async Result</param>
    /// <param name="function">The function to execute</param>
    /// <typeparam name="TSucc">Input type</typeparam>
    /// <typeparam name="TResult">The type of the result of the computation</typeparam>
    /// <returns>Async of either a Success from the computation, or a Failure</returns>   
    [<Extension>]
    static member Then (res: Task<Result<'a>>, fn: 'a -> Result<'b>) =
        AsyncResultExtensions.Then(res, Func<'a, Result<'b>>(fn))
    
    /// <summary>
    /// If holding a Success, Executes the function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function, otherwise returns the result of the computation.
    /// </summary>
    /// <param name="res">Input Async Result</param>
    /// <param name="function">The function to execute</param>
    /// <param name="mapException">The mapping function for the error</param>
    /// <typeparam name="TSucc">Input type</typeparam>
    /// <typeparam name="TResult">The type of the result of the computation</typeparam>
    /// <returns>Async of either a Success from the computation, or a Failure</returns>   
    [<Extension>]
    static member Then (res: Task<Result<'a>>, fn: 'a -> 'b, mapErrors: exn -> exn) =
        AsyncResultExtensions.Then(res, fn, mapErrors)
        
    /// <summary>
    /// If holding a Success, Executes the function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function, otherwise returns the result of the computation.
    /// </summary>
    /// <param name="res">Input Async Result</param>
    /// <param name="function">The function to execute</param>
    /// <param name="mapException">The mapping function for the error</param>
    /// <typeparam name="TSucc">Input type</typeparam>
    /// <typeparam name="TResult">The type of the result of the computation</typeparam>
    /// <returns>Async of either a Success from the computation, or a Failure</returns>   
    [<Extension>]
    static member Then (res: Task<Result<'a>>, fn: 'a -> 'b, mapErrors: Func<exn,exn>) =
        AsyncResultExtensions.Then(res, fn, mapErrors)
      
    /// <summary>
    /// If holding a Success, Executes the function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function, otherwise returns the result of the computation.
    /// </summary>
    /// <param name="res">Input Async Result</param>
    /// <param name="function">The function to execute</param>
    /// <param name="mapException">The mapping function for the error</param>
    /// <typeparam name="TSucc">Input type</typeparam>
    /// <returns>Async of either a Success from the computation, or a Failure</returns>   
    [<Extension>]
    static member Then (res: Task<Result<'a>>, fn: 'a -> unit, mapErrors: exn -> exn) =
        let voidfn = Func<'a, Unit>(fn >> Unit);
        AsyncResultExtensions.Then(res, voidfn, mapErrors)

    /// <summary>
    /// If holding a Success, Executes the function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function, otherwise returns the result of the computation.
    /// </summary>
    /// <param name="res">Input Async Result</param>
    /// <param name="function">The function to execute</param>
    /// <param name="mapException">The mapping function for the error</param>
    /// <typeparam name="TSucc">Input type</typeparam>
    /// <returns>Async of either a Success from the computation, or a Failure</returns>   
    [<Extension>]
    static member Then (res: Task<Result<'a>>, fn: 'a -> unit, mapErrors: Func<exn,exn>) =
        let voidfn = Func<'a, Unit>(fn >> Unit);
        AsyncResultExtensions.Then(res, voidfn, mapErrors)
        
    /// <summary>
    /// If holding a Success, Executes the .NET async function with the result as input.
    /// </summary>
    /// <param name="res">Input Async Result</param>
    /// <param name="function">The .NET async function to execute</param>
    /// <typeparam name="TSucc">Input type</typeparam>
    /// <typeparam name="TResult">The type of the result of the computation</typeparam>
    /// <returns>Async of either a Success from the computation, or a Failure</returns> 
    [<Extension>]
    static member ThenAwait (res: Task<Result<'a>>, fn: 'a -> Task<Result<'b>>) =
        AsyncResultExtensions.ThenAwait(res, Func<'a, Task<Result<'b>>>(fn))
    
    /// <summary>
    /// If holding a Success, Executes the .NET async function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function, otherwise returns the result of the computation.
    /// </summary>
    /// <param name="res">Input Async Result</param>
    /// <param name="function">The .NET async function to execute</param>
    /// <param name="mapException">The mapping function for the error</param>
    /// <typeparam name="TSucc">Input type</typeparam>
    /// <typeparam name="TResult">The type of the result of the computation</typeparam>
    /// <returns>Async of either a Success from the computation, or a Failure</returns> 
    [<Extension>]
    static member ThenAwait (res: Task<Result<'a>>, fn: 'a -> Task<'b>, mapErrors: exn -> exn) =
        AsyncResultExtensions.ThenAwait(res, fn, mapErrors)
        
    /// <summary>
    /// If holding a Success, Executes the .NET async function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function, otherwise returns the result of the computation.
    /// </summary>
    /// <param name="res">Input Async Result</param>
    /// <param name="function">The .NET async function to execute</param>
    /// <param name="mapException">The mapping function for the error</param>
    /// <typeparam name="TSucc">Input type</typeparam>
    /// <typeparam name="TResult">The type of the result of the computation</typeparam>
    /// <returns>Async of either a Success from the computation, or a Failure</returns> 
    [<Extension>]
    static member ThenAwait (res: Task<Result<'a>>, fn: 'a -> Task<'b>, mapErrors: Func<exn, exn>) =
        AsyncResultExtensions.ThenAwait(res, fn, mapErrors)
     
    /// <summary>
    /// If holding a Success, Executes the .NET async function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function, otherwise returns the result of the computation.
    /// </summary>
    /// <param name="res">Input Async Result</param>
    /// <param name="function">The .NET async function to execute</param>
    /// <param name="mapException">The mapping function for the error</param>
    /// <typeparam name="TSucc">Input type</typeparam>
    /// <returns>Async of either a Success from the computation, or a Failure</returns> 
    [<Extension>]
    static member ThenAwait (res: Task<Result<'a>>, fn: 'a -> Task, mapErrors: Func<exn,exn>) =
        AsyncResultExtensions.ThenAwait(res, fn, mapErrors)
        
    /// <summary>
    /// If holding a Success, Executes the .NET async function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function, otherwise returns the result of the computation.
    /// </summary>
    /// <param name="res">Input Async Result</param>
    /// <param name="function">The .NET async function to execute</param>
    /// <param name="mapException">The mapping function for the error</param>
    /// <typeparam name="TSucc">Input type</typeparam>
    /// <returns>Async of either a Success from the computation, or a Failure</returns> 
    [<Extension>]
    static member ThenAwait (res: Task<Result<'a>>, fn: 'a -> Task, mapErrors: exn -> exn) =
        AsyncResultExtensions.ThenAwait(res, fn, mapErrors)
        
    //DO
    
    /// <summary>
    /// If holding a Success, Executes the function with the result as input.
    /// </summary>
    /// <param name="res">The input result</param>
    /// <param name="fn">The function to execute</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <typeparam name="'b">The type of the result of the computation (unused)</typeparam>
    /// <returns>Either the Success, or a Failure</returns>
    [<Extension>]
    static member Do (res: Result<'a>, fn: 'a -> Result<'b>) =
        res.Do(Func<'a, Result<'b>>(fn))
    
    /// <summary>
    /// If holding a Success, Executes the function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function.
    /// </summary>
    /// <param name="res">The input result</param>
    /// <param name="fn">The function to execute</param>
    /// <param name="mapErrors">The mapping function for the error</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <typeparam name="'b">The type of the result of the computation (unused)</typeparam>
    /// <returns>Either the Success, or a Failure</returns>
    [<Extension>]
    static member Do (res: Result<'a>, fn: 'a -> 'b, mapErrors: exn -> exn) =
        res.Do(fn, mapErrors)
        
    /// <summary>
    /// If holding a Success, Executes the function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function.
    /// </summary>
    /// <param name="res">The input result</param>
    /// <param name="fn">The function to execute</param>
    /// <param name="mapErrors">The mapping function for the error</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <typeparam name="'b">The type of the result of the computation (unused)</typeparam>
    /// <returns>Either the Success, or a Failure</returns>
    [<Extension>]
    static member Do (res: Result<'a>, fn: 'a -> 'b, mapErrors: Func<exn,exn>) =
        res.Do(fn, mapErrors)
        
    /// <summary>
    /// If holding a Success, Executes the function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function.
    /// </summary>
    /// <param name="res">The input result</param>
    /// <param name="fn">The function to execute</param>
    /// <param name="mapErrors">The mapping function for the error</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <returns>Either the Success, or a Failure</returns>
    [<Extension>]
    static member Do (res: Result<'a>, fn: 'a -> unit, mapErrors: exn -> exn) =
        let voidfn = Func<'a, Unit>(fn >> Unit);
        res.Do(voidfn, mapErrors)
        
    /// <summary>
    /// If holding a Success, Executes the function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function.
    /// </summary>
    /// <param name="res">The input result</param>
    /// <param name="fn">The function to execute</param>
    /// <param name="mapErrors">The mapping function for the error</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <returns>Either the Success, or a Failure</returns>
    [<Extension>]
    static member Do (res: Result<'a>, fn: 'a -> unit, mapErrors: Func<exn,exn>) =
        let voidfn = Func<'a, Unit>(fn >> Unit);
        res.Do(voidfn, mapErrors)
        
    /// <summary>
    /// If holding a Success, Executes the function with the result as input.
    /// </summary>
    /// <param name="res">The input result</param>
    /// <param name="fn">The function to execute</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <typeparam name="'b">The type of the result of the computation (unused)</typeparam>
    /// <returns>Either the Success, or a Failure</returns>
    [<Extension>]
    static member Do (res: Task<Result<'a>>, fn: 'a -> Result<'b>) =
        AsyncResultExtensions.Do(res, Func<'a, Result<'b>>(fn))
    
    /// <summary>
    /// If holding a Success, Executes the function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function.
    /// </summary>
    /// <param name="res">The input result</param>
    /// <param name="fn">The function to execute</param>
    /// <param name="mapErrors">The mapping function for the error</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <typeparam name="'b">The type of the result of the computation (unused)</typeparam>
    /// <returns>Either the Success, or a Failure</returns>
    [<Extension>]
    static member Do (res: Task<Result<'a>>, fn: 'a -> 'b, mapErrors: exn -> exn) =
        AsyncResultExtensions.Do(res, Func<'a,'b>(fn), mapErrors)
        
    /// <summary>
    /// If holding a Success, Executes the function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function.
    /// </summary>
    /// <param name="res">The input result</param>
    /// <param name="fn">The function to execute</param>
    /// <param name="mapErrors">The mapping function for the error</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <typeparam name="'b">The type of the result of the computation (unused)</typeparam>
    /// <returns>Either the Success, or a Failure</returns>
    [<Extension>]
    static member Do (res: Task<Result<'a>>, fn: 'a -> 'b, mapErrors: Func<exn,exn>) =
        AsyncResultExtensions.Do(res, fn, mapErrors)
        
    /// <summary>
    /// If holding a Success, Executes the function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function.
    /// </summary>
    /// <param name="res">The input result</param>
    /// <param name="fn">The function to execute</param>
    /// <param name="mapErrors">The mapping function for the error</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <returns>Either the Success, or a Failure</returns>
    [<Extension>]
    static member Do (res: Task<Result<'a>>, fn: 'a -> unit, mapErrors: exn -> exn) =
        let voidfn = Func<'a, Unit>(fn >> Unit);
        AsyncResultExtensions.Do(res, voidfn, mapErrors)
        
    /// <summary>
    /// If holding a Success, Executes the function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function.
    /// </summary>
    /// <param name="res">The input result</param>
    /// <param name="fn">The function to execute</param>
    /// <param name="mapErrors">The mapping function for the error</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <returns>Either the Success, or a Failure</returns>
    [<Extension>]
    static member Do (res: Task<Result<'a>>, fn: 'a -> unit, mapErrors: Func<exn,exn>) =
        let voidfn = Func<'a, Unit>(fn >> Unit);
        AsyncResultExtensions.Do(res, voidfn, mapErrors)
        
    /// <summary>
    /// If holding a Success, Executes the .NET async function with the result as input.
    /// </summary>
    /// <param name="res">The input result</param>
    /// <param name="fn">The .NET async function to execute</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <typeparam name="'b">The type of the result of the computation (unused)</typeparam>
    /// <returns>Either the Success, or a Failure</returns>
    [<Extension>]
    static member DoAwait (res: Task<Result<'a>>, fn: 'a -> Task<Result<'b>>) =
        AsyncResultExtensions.DoAwait(res, Func<'a, Task<Result<'b>>>(fn))
    
    /// <summary>
    /// If holding a Success, Executes the .NET async function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function.
    /// </summary>
    /// <param name="res">The input async result</param>
    /// <param name="fn">The .NET async function to execute</param>
    /// <param name="mapErrors">The mapping function for the error</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <typeparam name="'b">The type of the result of the computation (unused)</typeparam>
    /// <returns>Either the Success, or a Failure</returns>
    [<Extension>]
    static member DoAwait (res: Task<Result<'a>>, fn: 'a -> Task<'b>, mapErrors: exn -> exn) =
        AsyncResultExtensions.DoAwait(res, fn, mapErrors)
        
    /// <summary>
    /// If holding a Success, Executes the .NET async function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function.
    /// </summary>
    /// <param name="res">The input async result</param>
    /// <param name="fn">The .NET async function to execute</param>
    /// <param name="mapErrors">The mapping function for the error</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <typeparam name="'b">The type of the result of the computation (unused)</typeparam>
    /// <returns>Either the Success, or a Failure</returns>
    [<Extension>]
    static member DoAwait (res: Task<Result<'a>>, fn: 'a -> Task<'b>, mapErrors: Func<exn, exn>) =
        AsyncResultExtensions.DoAwait(res, fn, mapErrors)
     
    /// <summary>
    /// If holding a Success, Executes the .NET async function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function.
    /// </summary>
    /// <param name="res">The input async result</param>
    /// <param name="fn">The .NET async function to execute</param>
    /// <param name="mapErrors">The mapping function for the error</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <returns>Either the Success, or a Failure</returns>
    [<Extension>]
    static member DoAwait (res: Task<Result<'a>>, fn: 'a -> Task, mapErrors: Func<exn,exn>) =
        AsyncResultExtensions.DoAwait(res, fn, mapErrors)
        
    /// <summary>
    /// If holding a Success, Executes the .NET async function with the result as input.
    /// If any exception is thrown, it is mapped by the mapper function.
    /// </summary>
    /// <param name="res">The input async result</param>
    /// <param name="fn">The .NET async function to execute</param>
    /// <param name="mapErrors">The mapping function for the error</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <returns>Either the Success, or a Failure</returns>
    [<Extension>]
    static member DoAwait (res: Task<Result<'a>>, fn: 'a -> Task, mapErrors: exn -> exn) =
        AsyncResultExtensions.DoAwait(res, fn, mapErrors)
        
    //IF
    
    /// <summary>
    /// If holding a Success, Executes the function with the result as input. If the function returns False, returns a failure.
    /// </summary>
    /// <param name="res">The input result</param>
    /// <param name="function">The function to execute</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <returns>Either the Success, or a Failure</returns>
    [<Extension>]
    static member If (res: Result<'a>, fn: 'a -> Result<bool>) =
        res.If(Func<'a, Result<bool>>(fn))
    
    /// <summary>
    /// If holding a Success, Executes the function with the result as input. If the function returns False, returns a failure.
    /// If any exception is thrown, it is mapped by the mapper function.
    /// </summary>
    /// <param name="res">The input result</param>
    /// <param name="function">The function to execute</param>
    /// <param name="mapException">The mapping function for the error</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <returns>Either the Success, or a Failure</returns>
    [<Extension>]
    static member If (res: Result<'a>, fn: 'a -> bool, mapErrors: exn -> exn) =
        res.If(fn, mapErrors)
        
    /// <summary>
    /// If holding a Success, Executes the function with the result as input. If the function returns False, returns a failure.
    /// If any exception is thrown, it is mapped by the mapper function.
    /// </summary>
    /// <param name="res">The input result</param>
    /// <param name="function">The function to execute</param>
    /// <param name="mapException">The mapping function for the error</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <returns>Either the Success, or a Failure</returns>
    [<Extension>]
    static member If (res: Result<'a>, fn: 'a -> bool, mapErrors: Func<exn,exn>) =
        res.If(fn, mapErrors)

    /// <summary>
    /// If holding a Success, Executes the function with the result as input. If the function returns False, returns a failure.
    /// </summary>
    /// <param name="res">The input async result</param>
    /// <param name="function">The function to execute</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <returns>Either the Success, or a Failure</returns>
    [<Extension>]
    static member If (res: Task<Result<'a>>, fn: 'a -> Result<bool>) =
        AsyncResultExtensions.If(res, Func<'a, Result<bool>>(fn))
    
    /// <summary>
    /// If holding a Success, Executes the function with the result as input. If the function returns False, returns a failure.
    /// If any exception is thrown, it is mapped by the mapper function.
    /// </summary>
    /// <param name="res">The input async result</param>
    /// <param name="function">The function to execute</param>
    /// <param name="mapException">The mapping function for the error</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <returns>Either the Success, or a Failure</returns>
    [<Extension>]
    static member If (res: Task<Result<'a>>, fn: 'a -> bool, mapErrors: exn -> exn) =
        AsyncResultExtensions.If(res, Func<'a, bool>(fn), Func<exn,exn>(mapErrors))
      
    /// <summary>
    /// If holding a Success, Executes the function with the result as input. If the function returns False, returns a failure.
    /// If any exception is thrown, it is mapped by the mapper function.
    /// </summary>
    /// <param name="res">The input async result</param>
    /// <param name="function">The function to execute</param>
    /// <param name="mapException">The mapping function for the error</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <returns>Either the Success, or a Failure</returns>  
    [<Extension>]
    static member If (res: Task<Result<'a>>, fn: 'a -> bool, mapErrors: Func<exn,exn>) =
        AsyncResultExtensions.If(res, Func<'a, bool>(fn), mapErrors)
        
    /// <summary>
    /// If holding a Success, Executes the .NET async function with the result as input. If the function returns False, returns a failure.
    /// </summary>
    /// <param name="res">The input async result</param>
    /// <param name="function">The .NET async function to execute</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <returns>Either the Success, or a Failure</returns>  
    [<Extension>]
    static member IfAwait (res: Task<Result<'a>>, fn: 'a -> Task<Result<bool>>) =
        AsyncResultExtensions.IfAwait(res, Func<'a, Task<Result<bool>>>(fn))
    
    /// <summary>
    /// If holding a Success, Executes the .NET async function with the result as input. If the function returns False, returns a failure.
    /// If any exception is thrown, it is mapped by the mapper function.
    /// </summary>
    /// <param name="res">The input async result</param>
    /// <param name="function">The .NET async function to execute</param>
    /// <param name="mapException">The mapping function for the error</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <returns>Either the Success, or a Failure</returns>  
    [<Extension>]
    static member IfAwait (res: Task<Result<'a>>, fn: 'a -> Task<bool>, mapErrors: exn -> exn) =
        AsyncResultExtensions.IfAwait(res, Func<'a, Task<bool>>(fn), Func<exn,exn>(mapErrors))
        
    /// <summary>
    /// If holding a Success, Executes the .NET async function with the result as input. If the function returns False, returns a failure.
    /// If any exception is thrown, it is mapped by the mapper function.
    /// </summary>
    /// <param name="res">The input async result</param>
    /// <param name="function">The .NET async function to execute</param>
    /// <param name="mapException">The mapping function for the error</param>
    /// <typeparam name="'a">The type of the result of the input</typeparam>
    /// <returns>Either the Success, or a Failure</returns>  
    [<Extension>]
    static member IfAwait (res: Task<Result<'a>>, fn: 'a -> Task<bool>, mapErrors: Func<exn, exn>) =
        AsyncResultExtensions.IfAwait(res, Func<'a, Task<bool>>(fn), mapErrors)