namespace ToolPack.DomainUtils.Functional;

/// <summary>Represents a success.</summary>
public readonly struct Success
{
    /// <summary>Initializes a new instance of the <see cref="Success"/> struct.</summary>
    public Success() { }

    /// <summary>Performs an implicit conversion from <see cref="Success" /> to boolean.</summary>
    /// <returns>True.</returns>
    public static implicit operator bool(Success _) => true;
}
