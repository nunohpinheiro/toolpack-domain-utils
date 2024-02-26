namespace ToolPack.DomainUtils.Functional;

using System.Collections.ObjectModel;

/// <summary>Represents an error that occurred.</summary>
public record Error
{
    /// <summary>Title of the <see cref="Error"/>.</summary>
    public readonly string Title;

    /// <summary>Message explaining the <see cref="Error"/>.</summary>
    public readonly string Message;

    /// <summary>Stack trace in which the <see cref="Error"/> occurred.</summary>
    public readonly string StackTrace = Environment.StackTrace;

    /// <summary>Inner errors related with <see cref="Error"/>.</summary>
    public readonly IReadOnlyDictionary<string, Error[]> InnerErrors = new Dictionary<string, Error[]>();

    /// <summary>Initializes a new instance of the <see cref="Error"/> class.</summary>
    /// <param name="message">The error message.</param>
    /// <param name="title">Title of the error; defaults to "Error".</param>
    /// <exception cref="ArgumentException">Input message must not be null, empty or white spaces. - message</exception>
    public Error(string message, string title = null!)
    {
        Title = string.IsNullOrWhiteSpace(title)
            ? "Error"
            : title;

        Message = string.IsNullOrWhiteSpace(message)
            ? throw new ArgumentException($"Input {nameof(message)} must not be null, empty or white spaces.", nameof(message))
            : message;
    }

    /// <summary>Initializes a new instance of the <see cref="Error"/> class.</summary>
    /// <param name="innerErrors">Inner errors related with the error.</param>
    /// <exception cref="ArgumentException">Inner errors must not be null or empty when creating a multiple error instance. - innerErrors</exception>
    public Error(IDictionary<string, Error[]> innerErrors)
    {
        Title = "Multiple errors";
        Message = "One or more errors occurred";

        InnerErrors = innerErrors?.Any() is true
            ? new ReadOnlyDictionary<string, Error[]>(innerErrors)
            : throw new ArgumentException("Inner errors must not be null or empty when creating a multiple error instance.", nameof(innerErrors));
    }
}
