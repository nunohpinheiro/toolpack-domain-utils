namespace UnitTests.Functional;

public class ResultPipeExtensionsTests
{
    [Test]
    public async Task PipeAsync_ResultInputIsSuccess_ReturnsNewSuccessResult()
        => (await Result<int>.Success(1)
        .PipeAsync(SumOthersAsStringAsync, 1, 1, 1))
        .Should()
        .Be(Result<string>.Success("4"));

    [Test]
    public async Task PipeAsync_ResultInputIsError_ReturnsError()
    {
        Error error = new("error-message", "error-title");
        (await Result<int>.Failure(error)
            .PipeAsync(SumOthersAsStringAsync, 1, 1, 1))
            .Should()
            .Be(Result<string>.Failure(error));
    }

    [Test]
    public void Pipe_ManyArguments_ResultInputIsSuccess_ReturnsNewSuccessResult()
        => Result<int>.Success(1)
        .Pipe(SumOthersAsString, 1, 1)
        .Should()
        .Be(Result<string>.Success("3"));

    [Test]
    public void Pipe_OneArgument_ResultInputIsSuccess_ReturnsNewSuccessResult()
        => Result<int>.Success(1)
        .Pipe(SumOneAsString)
        .Should()
        .Be(Result<string>.Success("2"));

    [Test]
    public void Pipe_ResultInputIsError_ReturnsError()
    {
        Error error = new("error-message", "error-title");
        Result<int>.Failure(error)
            .Pipe(SumOneAsString)
            .Should()
            .Be(Result<string>.Failure(error));
    }

    private static Result<string> SumOneAsString(int value)
        => $"{value + 1}";

    private static Result<string> SumOthersAsString(int value, int value2, int value3)
        => $"{value + value2 + value3}";

    private static async Task<Result<string>> SumOthersAsStringAsync(int value, int value2, int value3, int value4)
        => await Task.FromResult($"{value + value2 + value3 + value4}");
}
