namespace UnitTests.Functional;

public class ResultMapExtensionsTests
{
    [Test]
    public void Map_InputFunction_ResultInputIsSuccess_ReturnsSuccessMap()
        => Result<int>.Success(1)
        .Map(
            () => "success",
            () => "error")
        .Should()
        .Be("success");

    [Test]
    public void Map_InputFunction_ResultInputIsError_ReturnsErrorMap()
        => Result<int>.Failure(new Error("error"))
        .Map(
            () => "success",
            () => "error")
        .Should()
        .Be("error");

    [Test]
    public void Map_InputObject_ResultInputIsSuccess_ReturnsSuccessMap()
        => Result<int>.Success(1)
        .Map(
            "success",
            "error")
        .Should()
        .Be("success");

    [Test]
    public void Map_InputObject_ResultInputIsError_ReturnsErrorMap()
        => Result<int>.Failure(new Error("error"))
        .Map(
            "success",
            "error")
        .Should()
        .Be("error");

    [Test]
    public async Task MapAsync_ResultInputIsSuccess_ReturnsSuccessMap()
        => (await Result<int>.Success(1)
        .MapAsync(
            () => Task.FromResult("success"),
            () => Task.FromResult("error")))
        .Should()
        .Be("success");

    [Test]
    public async Task MapAsync_ResultInputIsError_ReturnsErrorMap()
        => (await Result<int>.Failure(new Error("error"))
        .MapAsync(
            () => Task.FromResult("success"),
            () => Task.FromResult("error")))
        .Should()
        .Be("error");

    [Test]
    public void MapSuccess_ResultInputIsSuccess_ReturnsSuccessMap()
        => Result<int>.Success(1)
        .MapSuccess(
            () => "success")
        .Should()
        .Be("success");

    [Test]
    public void MapSuccess_ResultInputIsError_ReturnsDefault()
        => Result<int>.Failure(new Error("error"))
        .MapSuccess(
            () => "success")
        .Should()
        .Be(null);

    [Test]
    public async Task MapSuccessAsync_ResultInputIsSuccess_ReturnsSuccessMap()
        => (await Result<int>.Success(1)
        .MapSuccessAsync(
            () => Task.FromResult("success")))
        .Should()
        .Be("success");

    [Test]
    public async Task MapSuccessAsync_ResultInputIsError_ReturnsDefault()
        => (await Result<int>.Failure(new Error("error"))
        .MapSuccessAsync(
            () => Task.FromResult("error")))
        .Should()
        .Be(null);

    [Test]
    public void MapError_ResultInputIsSuccess_ReturnsDefault()
        => Result<int>.Success(1)
        .MapError(
            () => "error")
        .Should()
        .Be(null);

    [Test]
    public void MapError_ResultInputIsError_ReturnsErrorMap()
        => Result<int>.Failure(new Error("error"))
        .MapError(
            () => "error")
        .Should()
        .Be("error");

    [Test]
    public async Task MapErrorAsync_ResultInputIsSuccess_ReturnsDefault()
        => (await Result<int>.Success(1)
        .MapErrorAsync(
            () => Task.FromResult("error")))
        .Should()
        .Be(null);

    [Test]
    public async Task MapErrorAsync_ResultInputIsError_ReturnsErrorMap()
        => (await Result<int>.Failure(new Error("error"))
        .MapErrorAsync(
            () => Task.FromResult("error")))
        .Should()
        .Be("error");
}
