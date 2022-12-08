namespace ToolPack.DomainUtils.Functional.Output;

/// <summary>Extension methods for the <see cref="Result"/> class.</summary>
public static class ResultExtensions
{
    /// <summary>Tries to get the error of a NOk <see cref="Result"/>.</summary>
    /// <typeparam name="TError">The type of the error.</typeparam>
    /// <param name="result">The input <see cref="Result"/>.</param>
    /// <param name="error">The output error, when it exists (the method will return true). Otherwise, it will be default.</param>
    /// <returns>True, if the input <see cref="Result"/> is NOk and has an error of type TError. Otherwise, false.</returns>
    public static bool TryGetNOkError<TError>(this Result result, out TError error)
    {
        if (result is NOk<TError> nok)
        {
            error = nok.Error!;
            return true;
        }

        error = default!;
        return false;
    }

    /// <summary>Tries to get the value of an Ok <see cref="Result"/>.</summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="result">The input <see cref="Result"/>.</param>
    /// <param name="value">The output value, when it exists (the method will return true). Otherwise, it will be default.</param>
    /// <returns>True, if the input <see cref="Result"/> is Ok and has a value of type TValue. Otherwise, false.</returns>
    public static bool TryGetOkValue<TValue>(this Result result, out TValue value)
    {
        if (result is Ok<TValue> ok)
        {
            value = ok.Value!;
            return true;
        }

        value = default!;
        return false;
    }
}
