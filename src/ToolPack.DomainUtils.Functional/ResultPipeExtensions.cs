namespace ToolPack.DomainUtils.Functional;

/// <summary>Extension methods to pipe a <see cref="Result"/> input to <see cref="Result"/> functions.</summary>
public static class ResultPipeExtensions
{
    public static Result<Tout> Pipe<T1, Tout>(
        this Result<T1> resultInput, Func<T1, Result<Tout>> resultFunc)
        => resultInput.IsSuccess
        ? resultFunc(resultInput.Value!)
        : resultInput.Error!;

    public static Result<Tout> Pipe<T1, T2, Tout>(
        this Result<T1> resultInput, Func<T1, T2, Result<Tout>> resultFunc, T2 input2)
        => resultInput.IsSuccess
        ? resultFunc(resultInput.Value!, input2)
        : resultInput.Error!;

    public static Result<Tout> Pipe<T1, T2, T3, Tout>(
        this Result<T1> resultInput, Func<T1, T2, T3, Result<Tout>> resultFunc, T2 input2, T3 input3)
        => resultInput.IsSuccess
        ? resultFunc(resultInput.Value!, input2, input3)
        : resultInput.Error!;

    public static Result<Tout> Pipe<T1, T2, T3, T4, Tout>(
        this Result<T1> resultInput, Func<T1, T2, T3, T4, Result<Tout>> resultFunc, T2 input2, T3 input3, T4 input4)
        => resultInput.IsSuccess
        ? resultFunc(resultInput.Value!, input2, input3, input4)
        : resultInput.Error!;

    public static async Task<Result<Tout>> PipeAsync<T1, Tout>(
        this Result<T1> resultInput, Func<T1, Task<Result<Tout>>> resultFunc)
        => resultInput.IsSuccess
        ? await resultFunc(resultInput.Value!)
        : resultInput.Error!;

    public static async Task<Result<Tout>> PipeAsync<T1, T2, Tout>(
        this Result<T1> resultInput, Func<T1, T2, Task<Result<Tout>>> resultFunc, T2 input2)
        => resultInput.IsSuccess
        ? await resultFunc(resultInput.Value!, input2)
        : resultInput.Error!;

    public static async Task<Result<Tout>> PipeAsync<T1, T2, T3, Tout>(
        this Result<T1> resultInput, Func<T1, T2, T3, Task<Result<Tout>>> resultFunc, T2 input2, T3 input3)
        => resultInput.IsSuccess
        ? await resultFunc(resultInput.Value!, input2, input3)
        : resultInput.Error!;

    public static async Task<Result<Tout>> PipeAsync<T1, T2, T3, T4, Tout>(
        this Result<T1> resultInput, Func<T1, T2, T3, T4, Task<Result<Tout>>> resultFunc, T2 input2, T3 input3, T4 input4)
        => resultInput.IsSuccess
        ? await resultFunc(resultInput.Value!, input2, input3, input4)
        : resultInput.Error!;
}
