# Result Type Interop

A library for Interop between F# and C# Result, Async and List types.

Latest Version: 0.0.1

# Getting Started

Install via Nuget
```bash
$ dotnet add package ResultTypeInterop --version 0.0.1
```

Install via Package Manager
```bash
Install-Package ResultTypeInterop -Version 0.0.1
```

# Usage
This library can be included in a project by using the relevant assembly:<br>
C#
```c#
using ResultTypeInterop;
```
F#
```f#
open ResultTypeInterop
```

The library contains support for conversions between:
- .NET Tasks and F# Asyncs
- Result Types (C# Result Type from [here](https://github.com/archery2000/CSharpResult))
- Async Result Types (C# Task<Result<T>>)
- Result Lists
- Async Result Lists

C#
```c#
// .NET Task to F# and back
var task = new Task.FromResult(1);
var fsasync = task.AsAsync();
task = fsasync.AsTask();

// C# Result to F# and back
var res = 1.ToResult();
var fsres = res.ToFSharpResult();
res = FSharpConvert.ToCSharpResult(fsres);

// C# Async Result to F# and back
var res = Task.FromResult(1.ToResult());
var fsres = res.ToFSharpAsyncResult();
res = FSharpConvert.ToCSharpAsyncResult(fsres);

// C# Result List to F# and back
var resList = new List<Result<int>>();
var fsres = res.ToFSharpResultList();
resList = FSharpConvert.ToCSharpResultList(fsres);

// C# Async Result List to F# and back
var resList = new List<Task<Result<int>>>();
var fsres = res.ToFSharpAsyncResultList();
resList = FSharpConvert.ToCSharpAsyncResultList(fsres);
```

F#
```f#
// F# Async to .NET Task and back
let fsasync = async{
                return 1
            }
let task = fsasync.AsTask()
let fsasync = task.AsAsync()

// F# Result to C# and back
let res = Ok 1
let csres = FSharpConvert.ToCSharpResult(res);
let fsres = csres.ToFSharpResult();

// C# Async Result to F# and back
let res = async{
            return Ok 1
        }
let csres = FSharpConvert.ToCSharpAsyncResult(res);
let fsres = csres.ToFSharpAsyncResult();

// C# Result List to F# and back
let resList: Result<int, exn> list = [];
let csres = FSharpConvert.ToCSharpResultList(resList);
let fsList = csres.ToFSharpResultList();

// C# Async Result List to F# and back
let resList: Async<Result<int, exn>> list = [];
let csres = FSharpConvert.ToCSharpAsyncResultList(resList);
let fsList = csres.ToFSharpAsyncResultList();
```

## Contributing
Please read [CONTRIBUTING.md](CONTRIBUTING.MD) for details on our code of conduct, and the process for submitting pull requests to us.


## Versioning 
We use [SemVer](https://semver.org/) for versioning. For the versions available, see the tags on this repository.

## Changelog
| Version | Changes |
|---|---|
|0.0.1|Initial Release.

## Authors
* [archery2000](mailto:ong.yh@atomi.cloud) 

## License
This project is licensed under MIT - see the [LICENSE.md](LICENSE.MD) file for details