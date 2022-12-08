namespace ToolPack.DomainUtils.Functional.Output;

/// <summary>Represents the successful result of an operation.</summary>
public record Ok : Result
{
    internal Ok() : base() { }
}

/// <summary>Represents the successful result of an operation, holding an output <see cref="Value"/>.</summary>
/// <typeparam name="TValue">The type of the <see cref="Value"/>.</typeparam>
public record Ok<TValue> : Ok
{
    private TValue? _value;

    /// <summary>Value that the successful (<see cref="Ok"/>) result holds.</summary>
    /// <exception cref="InvalidOperationException">Result is NOk. It does not have <see cref="Value"/>.</exception>
    public TValue? Value
    {
        get
        {
            if (IsNOk)
                throw new InvalidOperationException($"Result is {nameof(NOk)}. It does not have {nameof(Value)}.");

            return _value;
        }
        private set => _value = value;
    }

    internal Ok(TValue value) : base()
    {
        Value = value;
    }

    public static implicit operator Ok<TValue>(TValue value) => Ok<TValue>(value);
}
