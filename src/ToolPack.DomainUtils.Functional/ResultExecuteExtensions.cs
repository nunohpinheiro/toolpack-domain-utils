namespace ToolPack.DomainUtils.Functional;

/// <summary>Extension methods to execute <see cref="IResult"/> actions/functions according to an input <see cref="Result"/>.</summary>
public static class ResultExecuteExtensions
{
    /// <summary>Executes a <see cref="IResult"/> function according to an input <see cref="Result"/>.</summary>
    public static IResult Execute(this IResult result, Func<IResult> resultFunc)
        => result.IsSuccess ? resultFunc() : result;

    /// <summary>Executes an action according to an input <see cref="IResult"/>.</summary>
    public static void Execute(this IResult result, Action resultAct)
    {
        if (result.IsSuccess)
            resultAct();
    }

    /// <summary>Executes an async <see cref="IResult"/> function according to an input <see cref="IResult"/>.</summary>
    public static async Task<IResult> ExecuteAsync(this IResult result, Func<Task<IResult>> resultFunc)
        => result.IsSuccess ? await resultFunc() : result;

    /// <summary>Executes an async task according to an input <see cref="Result"/>.</summary>
    public static async Task ExecuteAsync(this IResult result, Func<Task> resultAct)
    {
        if (result.IsSuccess)
            await resultAct();
    }

    /// <summary>Executes parallel async <see cref="Result"/> functions according to an input <see cref="IResult"/>.</summary>
    public static async Task<Result> ParallelExecuteAsync(this IResult result, params Func<Task<IResult>>[] resultFuncs)
    {
        if (result.IsFailure)
            return Result.Failure(result.Error!);

        var tasks = resultFuncs.Select(resultFunc => resultFunc());
        var results = await Task.WhenAll(tasks);
        return Result.Combine(results);
    }
}
