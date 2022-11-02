namespace ToolPack.DomainUtils.Functional;

public record Result
{
    // TODO: Add tests for this
    // TODO: Worth to have extensions, for example, checking if Result T value is null (if class), default (if struct), string white/empty, or has elements in array (ienumerable)?

    public readonly Exception Exception = null!;
    public readonly bool IsOk = true;
    public bool IsNOk => !IsOk;

    protected internal Result() { }

    protected Result(Exception exception)
    {
        ArgumentNullException.ThrowIfNull(exception);

        Exception = exception;
        IsOk = false;
    }

    public static Result Combine(params Result[] results)
    {
        var exceptions = results?
            .Where(r => r.IsNOk)
            .Select(r => r.Exception)
            .ToList();

        if (exceptions?.Any() is true)
            return NOk(new AggregateException(exceptions));

        return Ok();
    }

    public static Result Combine(IEnumerable<Result> results) => Combine(results.ToArray());

    public static Result NOk<TException>(TException exception)
        where TException : Exception
        => new(exception);

    public static Result Ok() => new();
}

public record Result<T> : Result
{
    private T _value;

    public T Value
    {
        get
        {
            if (IsNOk)
                throw new InvalidOperationException($"Result is {nameof(NOk)}. It does not have {nameof(Value)}.");

            return _value;
        }
        private set => _value = value;
    }

    private Result(T value) : base()
    {
        Value = value;
    }

    public static Result<T> Ok(T value) => new(value);
}
