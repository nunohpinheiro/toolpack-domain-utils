namespace ToolPack.DomainUtils.Functional;

/// <summary>Represents an error that occurred in an application.</summary>
public class Error
{
    /// <summary>Message explaining the <see cref="Error"/>.</summary>
    public readonly string Message;

    /// <summary>Stack trace in which the <see cref="Error"/> occurred.</summary>
    public readonly string StackTrace;

    /// <summary>Collection inner errors related with <see cref="Error"/>.</summary>
    public readonly IReadOnlyCollection<Error> InnerErrors = Enumerable.Empty<Error>().ToList().AsReadOnly();

    /// <summary>Initializes a new instance of the <see cref="Error"/> class.</summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerErrors">Inner errors related with the error.</param>
    /// <exception cref="ArgumentException">Input message must not be null, empty or white spaces. - message</exception>
    public Error(string message, IEnumerable<Error> innerErrors = null!)
    {
        Message = string.IsNullOrWhiteSpace(message)
            ? throw new ArgumentException($"Input {nameof(message)} must not be null, empty or white spaces.", nameof(message))
            : message;
        StackTrace = Environment.StackTrace;

        if (innerErrors?.Any() is true)
            InnerErrors = innerErrors.ToList().AsReadOnly();
    }
}
