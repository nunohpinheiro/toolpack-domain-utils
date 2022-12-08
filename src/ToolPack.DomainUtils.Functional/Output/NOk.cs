namespace ToolPack.DomainUtils.Functional.Output;

/// <summary>Represents the unsuccessful result of an operation, holding an output <see cref="Error"/>.</summary>
/// <typeparam name="TError">The type of the <see cref="Error"/>.</typeparam>
public record NOk<TError> : Result
{
    private TError? _value;

    /// <summary>Error that the unsuccessful (NOk) result holds.</summary>
    /// <exception cref="InvalidOperationException">Result is Ok. It does not have <see cref="Error"/>.</exception>
    public TError? Error
    {
        get
        {
            if (IsOk)
                throw new InvalidOperationException($"Result is Ok. It does not have {nameof(Error)}.");

            return _value;
        }
        private set => _value = value;
    }

    internal NOk(TError value) : base(false)
    {
        Error = value;
    }

    public static implicit operator NOk<TError>(TError error) => NOk<TError>(error);
}
