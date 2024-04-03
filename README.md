# ToolPack Domain Utils

Useful opinionated domain components and/or helper functions for .NET solutions.

## ToolPack.DomainUtils.Functional

Classes and interfaces to help with functional programming in C#.

### `Result<T>`, `Result`, `Error` and `Success`

This is a simple implementation of the Result pattern in C#.

The package contains `Result<T>` and `Result` (_i.e._ `Result<Success>`) classes, used to represent the result of an operation that can succeed or fail:
* In case of success, the `Value` property (of type `T`) will contain the successful output of the operation,
* In case of failure, the `Error` property will contain the `Error` output of the operation.

> The `Error` class represents operation errors with all their details.

**Useful references:**
   * [The ServiceResult Pattern](https://codingbolt.net/2023/10/06/the-serviceresult-pattern/)
   * [Functional C#: Handling failures, input errors](https://enterprisecraftsmanship.com/posts/functional-c-handling-failures-input-errors/)
   * [Improving API error responses with the Result pattern](https://raygun.com/blog/api-error-reponses-results-pattern/)
   * [What�s the Result Type Everyone Is Using in .NET?](https://www.youtube.com/watch?v=YbuSuSpzee4)

### Result extension methods

The package contains a set of extension methods to help with the handling of `Result` instances:
* `Execute`/`ExecuteAsync` execute a `Result` function or action if the input result is successful, or return the initial `Error` in case of input failure;
* `Log` methods log the output of the input `Result` instance, depending on the input result being a success or error;
* `Map`/`MapAsync` methods apply functions of generic return type, depending on the input result being a success or error;
* `Pipe`/`PipeAsync` methods apply a `Result` function to the `Value` of an input `Result<T>` instance if it is successful, or return the initial `Error` in case of input failure;
* `Switch`/`SwitchAsync` methods run actions, depending on the input result being a success or error.

These methods are useful to chain operations and handle the output of each operation in a functional way.

They intend to help with the implementation of the Railway Oriented Programming (ROP) pattern.

**Useful references:**
   * [Railway Oriented Programming](https://fsharpforfunandprofit.com/rop/)
   * [Functional Programming With C# Using Railway-Oriented Programming Approach](https://www.youtube.com/watch?v=dDasAmowFts)
   * [Handling Side Effects In Functional Code With Railway-Oriented Programming](https://www.youtube.com/watch?v=vGkgsduwnc4)
