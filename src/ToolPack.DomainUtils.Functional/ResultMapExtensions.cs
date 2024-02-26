namespace ToolPack.DomainUtils.Functional;

/// <summary>Extension methods to map <see cref="IResult"/> to other actions/functions.</summary>
public static class ResultMapExtensions
{
    /// <summary>Maps a success to a function, or default in case of error.</summary>
    public static T MapSuccess<T>(this IResult result, Func<T> successFunc)
        => result.IsSuccess ? successFunc() : default!;

    /// <summary>Maps a success to an async function, or default in case of error.</summary>
    public static async Task<T> MapSuccessAsync<T>(this IResult result, Func<Task<T>> successFunc)
        => result.IsSuccess ? await successFunc() : default!;

    /// <summary>Maps an error to a function, or default in case of success.</summary>
    public static T MapError<T>(this IResult result, Func<T> errorFunc)
        => result.IsFailure ? errorFunc() : default!;

    /// <summary>Maps an error to an async function, or default in case of success.</summary>
    public static async Task<T> MapErrorAsync<T>(this IResult result, Func<Task<T>> errorFunc)
        => result.IsFailure ? await errorFunc() : default!;
}
