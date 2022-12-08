namespace ToolPack.DomainUtils.Functional.Output;

/// <summary>Represents the output of an operation, which can be <see cref="Ok"/> or <see cref="NOk"/> (with an associated <see cref="Error"/>).</summary>
public record Result
{
    /// <summary>Result is <see cref="Ok"/>. Represents a successful output.</summary>
    public readonly bool IsOk = true;

    /// <summary>Result is <see cref="NOk"/>. Represents an unsuccessful output.</summary>
    public bool IsNOk => !IsOk;

    /// <summary>Initializes a new instance of the <see cref="Result"/> class.</summary>
    /// <param name="isOk">Whether the <see cref="Result"/> is <see cref="Ok"/> or <see cref="NOk"/>.</param>
    protected Result(bool isOk = true)
    {
        IsOk = isOk;
    }

    /// <summary>Initializes a <see cref="NOk"/> (unsuccessful) result.</summary>
    /// <typeparam name="TError">The type of the error associated with the <see cref="NOk"/> result.</typeparam>
    /// <param name="error">The error associated with the <see cref="NOk"/> result.</param>
    /// <returns>A <see cref="NOk"/> result.</returns>
    public static NOk<TError> NOk<TError>(TError error) => new(error);

    /// <summary>Initializes an <see cref="Ok"/> (successful) result.</summary>
    /// <returns>An <see cref="Ok"/> result.</returns>
    public static Ok Ok() => new();

    /// <summary>Initializes an <see cref="Ok"/> (successful) result.</summary>
    /// <typeparam name="TValue">The type of the value associated with the <see cref="Ok"/> result.</typeparam>
    /// <param name="value">The value associated with the <see cref="Ok"/> result.</param>
    /// <returns>An <see cref="Ok"/> result.</returns>
    public static Ok<TValue> Ok<TValue>(TValue value) => new(value);
}
