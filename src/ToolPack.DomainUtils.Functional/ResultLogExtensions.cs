namespace ToolPack.DomainUtils.Functional;

using Microsoft.Extensions.Logging;

/// <summary>Extension methods to log an input <see cref="Result"/>.</summary>
public static class ResultLogExtensions
{
    /// <summary>Logs the <see cref="Result"/> as success or error.</summary>
    public static Result<T> Log<T>(this Result<T> result, ILogger logger, LogLevel successLevel = LogLevel.Debug, LogLevel errorLevel = LogLevel.Error)
    {
        if (result.IsFailure)
            logger.Log(errorLevel, "An error result occurred: {@Error}", result.Error!.ToString());
        else
            logger.Log(successLevel, "A success result occurred: {@Success}", result.Value);
        return result;
    }

    /// <summary>Logs the <see cref="IResult"/> in case it is an error.</summary>
    public static IResult LogIfError(this IResult result, ILogger logger, LogLevel errorLevel = LogLevel.Error)
    {
        if (result.IsFailure)
            logger.Log(errorLevel, "An error result occurred: {@Error}", result.Error!.ToString());
        return result;
    }

    /// <summary>Logs the <see cref="Result"/> in case it is a success.</summary>
    public static Result<T> LogIfSuccess<T>(this Result<T> result, ILogger logger, LogLevel successLevel = LogLevel.Debug)
    {
        if (result.IsSuccess)
            logger.Log(successLevel, "A success result occurred: {@Success}", result.Value);
        return result;
    }
}
