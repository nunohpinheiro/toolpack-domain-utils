namespace ToolPack.DomainUtils.Functional;

/// <summary>Represents the result of an operation, which is <see cref="Success"/> or <see cref="Error"/>.</summary>
public interface IResult
{
    Error? Error { get; }
    bool IsFailure { get; }
    public bool IsSuccess { get; }
}
