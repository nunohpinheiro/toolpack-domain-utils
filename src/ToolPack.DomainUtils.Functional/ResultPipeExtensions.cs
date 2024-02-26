namespace ToolPack.DomainUtils.Functional;

/// <summary>Extension methods to pipe a <see cref="Result"/> input to <see cref="Result"/> functions.</summary>
public static class ResultPipeExtensions
{
    public static IResult Pipe(this IResult result, Func<IResult, IResult> resultFunc)
        => result.IsSuccess
        ? resultFunc(result)
        : result;

    public static IResult Pipe<T>(this IResult result, Func<IResult, T, IResult> resultFunc, T input)
        => result.IsSuccess
        ? resultFunc(result, input)
        : result;

    public static IResult Pipe<T1, T2>(this IResult result, Func<IResult, T1, T2, IResult> resultFunc, T1 input1, T2 input2)
        => result.IsSuccess
        ? resultFunc(result, input1, input2)
        : result;

    public static IResult Pipe<T1, T2, T3>(this IResult result, Func<IResult, T1, T2, T3, IResult> resultFunc, T1 input1, T2 input2, T3 input3)
        => result.IsSuccess
        ? resultFunc(result, input1, input2, input3)
        : result;

    public static IResult Pipe<T1, T2, T3, T4>(this IResult result, Func<IResult, T1, T2, T3, T4, IResult> resultFunc, T1 input1, T2 input2, T3 input3, T4 input4)
        => result.IsSuccess
        ? resultFunc(result, input1, input2, input3, input4)
        : result;

    public static IResult Pipe<T1, T2, T3, T4, T5>(this IResult result, Func<IResult, T1, T2, T3, T4, T5, IResult> resultFunc, T1 input1, T2 input2, T3 input3, T4 input4, T5 input5)
        => result.IsSuccess
        ? resultFunc(result, input1, input2, input3, input4, input5)
        : result;

    public static async Task<IResult> PipeAsync(this IResult result, Func<IResult, Task<IResult>> resultFunc)
        => result.IsSuccess
        ? await resultFunc(result)
        : result;

    public static async Task<IResult> PipeAsync<T>(this IResult result, Func<IResult, T, Task<IResult>> resultFunc, T input)
        => result.IsSuccess
        ? await resultFunc(result, input)
        : result;

    public static async Task<IResult> PipeAsync<T1, T2>(this IResult result, Func<IResult, T1, T2, Task<IResult>> resultFunc, T1 input1, T2 input2)
        => result.IsSuccess
        ? await resultFunc(result, input1, input2)
        : result;

    public static async Task<IResult> PipeAsync<T1, T2, T3>(this IResult result, Func<IResult, T1, T2, T3, Task<IResult>> resultFunc, T1 input1, T2 input2, T3 input3)
        => result.IsSuccess
        ? await resultFunc(result, input1, input2, input3)
        : result;

    public static async Task<IResult> PipeAsync<T1, T2, T3, T4>(this IResult result, Func<IResult, T1, T2, T3, T4, Task<IResult>> resultFunc, T1 input1, T2 input2, T3 input3, T4 input4)
        => result.IsSuccess
        ? await resultFunc(result, input1, input2, input3, input4)
        : result;

    public static async Task<IResult> PipeAsync<T1, T2, T3, T4, T5>(this IResult result, Func<IResult, T1, T2, T3, T4, T5, Task<IResult>> resultFunc, T1 input1, T2 input2, T3 input3, T4 input4, T5 input5)
        => result.IsSuccess
        ? await resultFunc(result, input1, input2, input3, input4, input5)
        : result;
}
