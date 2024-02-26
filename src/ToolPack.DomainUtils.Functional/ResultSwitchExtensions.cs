namespace ToolPack.DomainUtils.Functional;

/// <summary>Extension methods to switch actions according to an input <see cref="IResult"/>.</summary>
public static class ResultSwitchExtensions
{
    /// <summary>Switches between two actions according to an input <see cref="IResult"/>.</summary>
    public static void Switch(this IResult result, Action successAct, Action errorAct)
    {
        if (result.IsFailure)
            errorAct();

        successAct();
    }

    /// <summary>Switches between two async tasks according to an input <see cref="IResult"/>.</summary>
    public static async Task SwitchAsync(this IResult result, Func<Task> successAct, Func<Task> errorAct)
    {
        if (result.IsFailure)
            await errorAct();

        await successAct();
    }
}
