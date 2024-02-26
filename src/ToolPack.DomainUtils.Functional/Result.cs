namespace ToolPack.DomainUtils.Functional;

/// <summary>Represents the result of an operation, which is <see cref="Success"/> or <see cref="Error"/>.</summary>
public record Result : Result<Success>
{
    /// <summary>Initializes a new instance of a successful result.</summary>
    protected Result() : base(new Success()) { }

    /// <summary>Initializes a new instance of an unsuccessful result, with an error.</summary>
    /// <param name="error">The error of the unsuccessful result.</param>
    protected Result(Error error) : base(error) { }

    /// <summary>Creates an error result.</summary>
    /// <param name="error">The error of the unsuccessful result.</param>
    /// <returns>A result with an error.</returns>
    public static new Result Failure(Error error) => new(error);

    /// <summary>Creates a sucess result.</summary>
    /// <returns>A successful result.</returns>
    public static Result Success() => new();

    /// <summary>Performs an implicit conversion from a value to a success <see cref="Result{T}"/>.</summary>
    /// <returns>The matching success result.</returns>
    public static implicit operator Result(Success _) => Success();

    /// <summary>Performs an implicit conversion from an error value to an error <see cref="Result{T}"/>.</summary>
    /// <param name="error">The error value.</param>
    /// <returns>The matching error result.</returns>
    public static implicit operator Result(Error error) => Failure(error);

    /// <summary>Combines the specified results.</summary>
    /// <param name="results">The results.</param>
    /// <returns>A <see cref="Result"/> with either <see cref="Success"/> or an aggregated <see cref="Error"/>.</returns>
    public static Result Combine(params IResult[] results)
        => results switch
        {
            null or { Length: 0 }
                => throw new InvalidOperationException("There are no results to combine. The array should not be null or empty."),
            _ when Array.Exists(results, r => r.IsFailure)
                => Failure(new Error(
                    results
                    .Where(r => r?.IsFailure is true)
                    .Select(r => r.Error)
                    .GroupBy(e => e!.Title)
                    .ToDictionary(g => g.Key, g => g.ToArray())!)),
            _ => Success()
        };
}

/// <summary>Represents the <see cref="Result{T}"/> of an operation, which <see cref="IsSuccess"/> or <see cref="IsFailure"/>.
/// A success will have a defined <see cref="Value"/>.
/// A failure will have a defined <see cref="Error"/>.</summary>
public record Result<T> : IResult
{
    private readonly T? _value;
    private readonly Error? _error;
    private readonly bool _isSuccess = true;

    /// <summary>Value that the successful result holds.</summary>
    /// <exception cref="InvalidOperationException">Result is an error. It does not have value.</exception>
    public T? Value
    {
        get => IsFailure
            ? throw new InvalidOperationException("Result is an error. It does not have value.")
            : _value;
        private init => _value = value;
    }

    /// <summary>Error that the unsuccessful result holds.</summary>
    /// <exception cref="InvalidOperationException">Result is a success. It does not have an error.</exception>
    public Error? Error
    {
        get => IsSuccess
            ? throw new InvalidOperationException("Result is a success. It does not have an error.")
            : _error;
        private init => _error = value;
    }

    /// <summary>Result is successful. Represents a successful output.</summary>
    public bool IsSuccess
    {
        get => _isSuccess;
        private init => _isSuccess = value;
    }

    /// <summary>Result is an error. Represents an unsuccessful output.</summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>Initializes a new instance of a successful result, with a value.</summary>
    /// <param name="value">The value of the successful result.</param>
    protected Result(T value)
        => Value = value;

    /// <summary>Initializes a new instance of an unsuccessful result, with an error.</summary>
    /// <param name="error">The error of the unsuccessful result.</param>
    protected Result(Error error)
    {
        Error = error;
        IsSuccess = false;
    }

    /// <summary>Creates an error result.</summary>
    /// <param name="error">The error of the unsuccessful result.</param>
    /// <returns>A result with an error.</returns>
    public static Result<T> Failure(Error error) => new(error);

    /// <summary>Creates a sucess result.</summary>
    /// <param name="value">The value of the successful result.</param>
    /// <returns>A result with a success value.</returns>
    public static Result<T> Success(T value) => new(value);

    /// <summary>Performs an implicit conversion from a value to a success <see cref="Result{T}"/>.</summary>
    /// <param name="value">The success value.</param>
    /// <returns>The matching success result.</returns>
    public static implicit operator Result<T>(T value) => Result<T>.Success(value);

    /// <summary>Performs an implicit conversion from an error value to an error <see cref="Result{T}"/>.</summary>
    /// <param name="error">The error value.</param>
    /// <returns>The matching error result.</returns>
    public static implicit operator Result<T>(Error error) => Result<T>.Failure(error);
}
