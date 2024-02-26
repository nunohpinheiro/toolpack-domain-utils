namespace ToolPack.DomainUtils.Functional;

/// <summary>Extension methods for the <see cref="Result"/> class.</summary>
public static class ResultExtensions
{
    public static IResult Execute(this IResult result, Func<IResult> resultFunc)
        => result.IsSuccess ? resultFunc() : result;

    public static void Execute(this IResult result, Action resultAct)
    {
        if (result.IsSuccess)
            resultAct();
    }

    public static async Task<IResult> ExecuteAsync(this IResult result, Func<Task<IResult>> resultFunc)
        => result.IsSuccess ? await resultFunc() : result;

    public static async Task ExecuteAsync(this IResult result, Func<Task> resultAct)
    {
        if (result.IsSuccess)
            await resultAct();
    }

    public static async Task<Result> ParallelExecuteAsync(this IResult result, params Func<Task<IResult>>[] resultFuncs)
    {
        if (result.IsFailure)
            return Result.Failure(result.Error!);

        var tasks = resultFuncs.Select(resultFunc => resultFunc());
        var results = await Task.WhenAll(tasks);
        return Result.Combine(results);
    }

    public static T MapSuccess<T>(this IResult result, Func<T> successFunc)
        => result.IsSuccess ? successFunc() : throw new InvalidOperationException("Result is not successful.");

    public static async Task<T> MapSuccessAsync<T>(this IResult result, Func<Task<T>> successFunc)
        => result.IsSuccess ? await successFunc() : throw new InvalidOperationException("Result is not successful.");

    public static T MapError<T>(this IResult result, Func<T> errorFunc)
        => result.IsFailure ? errorFunc() : throw new InvalidOperationException("Result is not an error.");

    public static async Task<T> MapErrorAsync<T>(this IResult result, Func<Task<T>> errorFunc)
        => result.IsFailure ? await errorFunc() : throw new InvalidOperationException("Result is not an error.");

    public static void Switch(this IResult result, Action successAct, Action errorAct)
    {
        if (result.IsFailure)
            errorAct();

        successAct();
    }

    public static async Task SwitchAsync(this IResult result, Func<Task> successAct, Func<Task> errorAct)
    {
        if (result.IsFailure)
            await errorAct();

        await successAct();
    }
}
