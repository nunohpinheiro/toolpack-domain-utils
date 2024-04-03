namespace UnitTests.Functional;

public class ResultExecuteExtensionsTests
{
    [Test]
    public async Task ExecuteAsync_ResultInputIsSuccess_ReturnsNewSuccessResult()
        => (await Result<int>.Success(1)
        .ExecuteAsync(() => SumOthersAsStringAsync(2, 2, 2, 2)))
        .Should()
        .Be(Result<string>.Success("8"));

    [Test]
    public async Task ExecuteAsync_ResultInputIsError_ReturnsError()
    {
        Error error = new("error-message", "error-title");
        (await Result<int>.Failure(error)
            .ExecuteAsync(() => SumOthersAsStringAsync(2, 2, 2, 2)))
            .Should()
            .Be(Result<string>.Failure(error));
    }

    [Test]
    public void Execute_ResultInputIsSuccess_ReturnsNewSuccessResult()
        => Result<int>.Success(1)
        .Execute(() => SumOthersAsString(2, 2, 2))
        .Should()
        .Be(Result<string>.Success("6"));

    [Test]
    public void Execute_ResultInputIsError_ReturnsError()
    {
        Error error = new("error-message", "error-title");
        Result<int>.Failure(error)
            .Execute(() => SumOthersAsString(2, 2, 2))
            .Should()
            .Be(Result<string>.Failure(error));
    }

    [Test]
    public async Task ParallelExecuteAsync_ResultInputIsSuccess_SuccessiveResultsAreSuccess_ReturnsNewSuccessResult()
        => (await Result<int>.Success(1)
        .ParallelExecuteAsync(
            () => Task.FromResult((IResult)Result.Success()),
            () => Task.FromResult((IResult)Result<string>.Success("oi")),
            () => Task.FromResult((IResult)Result<bool>.Success(true))))
        .Should()
        .Be(Result.Success());

    [Test]
    public async Task ParallelExecuteAsync_ResultInputIsSuccess_SuccessiveResultsHaveErrors_ReturnsError()
        => (await Result<int>.Success(1)
        .ParallelExecuteAsync(
            () => Task.FromResult((IResult)Result.Success()),
            () => Task.FromResult((IResult)Result<string>.Failure(new Error("error"))),
            () => Task.FromResult((IResult)Result<bool>.Failure(new Error("error")))))
        .Should()
        .BeOfType<Result>()
        .Which.IsFailure
        .Should().BeTrue();

    [Test]
    public async Task ParallelExecuteAsync_ResultInputIsError_ReturnsError()
    {
        Error error = new("error-message", "error-title");
        (await Result<int>.Failure(error)
            .ParallelExecuteAsync(
                () => Task.FromResult((IResult)Result.Success())))
            .Should()
            .Be(Result.Failure(error));
    }

    private static Result<string> SumOthersAsString(int value, int value2, int value3)
        => $"{value + value2 + value3}";

    private static async Task<Result<string>> SumOthersAsStringAsync(int value, int value2, int value3, int value4)
        => await Task.FromResult($"{value + value2 + value3 + value4}");
}
