namespace ToolPack.DomainUtils.Functional;

/// <summary>Represents the result of an operation, which is <see cref="Success"/> or <see cref="Error"/>.</summary>
public interface IResult
{
    /// <summary>Error, when the IResult is a failure.</summary>
    Error? Error { get; }
    /// <summary>The IResult is a failure.</summary>
    bool IsFailure { get; }
    /// <summary>The IResult is a success.</summary>
    bool IsSuccess { get; }
}
