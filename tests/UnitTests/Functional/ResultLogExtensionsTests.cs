namespace UnitTests.Functional;

using Microsoft.Extensions.Logging;
using System.Reactive.Disposables;

public class ResultLogExtensionsTests
{
    [Test]
    public void Log_ResultInputIsSuccess_LoggerIsCalled_ReturnsResult()
    {
        LoggerFixtureMock logger = new();
        Result<int>.Success(1)
            .Log(logger)
            .Should()
            .Be(Result<int>.Success(1));
        logger.IsLogCalled
            .Should().BeTrue();
    }

    [Test]
    public void Log_ResultInputIsError_LoggerIsCalled_ReturnsResult()
    {
        LoggerFixtureMock logger = new();
        Error error = new("error-message", "error-title");
        Result<int>.Failure(error)
            .Log(logger)
            .Should()
            .Be(Result<int>.Failure(error));
        logger.IsLogCalled
            .Should().BeTrue();
    }

    [Test]
    public void LogIfError_ResultInputIsSuccess_LoggerIsNotCalled_ReturnsResult()
    {
        LoggerFixtureMock logger = new();
        Result<int>.Success(1)
            .LogIfError(logger)
            .Should()
            .Be(Result<int>.Success(1));
        logger.IsLogCalled
            .Should().BeFalse();
    }

    [Test]
    public void LogIfError_ResultInputIsError_LoggerIsCalled_ReturnsResult()
    {
        LoggerFixtureMock logger = new();
        Error error = new("error-message", "error-title");
        Result<int>.Failure(error)
            .LogIfError(logger)
            .Should()
            .Be(Result<int>.Failure(error));
        logger.IsLogCalled
            .Should().BeTrue();
    }

    [Test]
    public void LogIfSuccess_ResultInputIsSuccess_LoggerIsCalled_ReturnsResult()
    {
        LoggerFixtureMock logger = new();
        Result<int>.Success(1)
            .LogIfSuccess(logger)
            .Should()
            .Be(Result<int>.Success(1));
        logger.IsLogCalled
            .Should().BeTrue();
    }

    [Test]
    public void LogIfSuccess_ResultInputIsError_LoggerIsNotCalled_ReturnsResult()
    {
        LoggerFixtureMock logger = new();
        Error error = new("error-message", "error-title");
        Result<int>.Failure(error)
            .LogIfSuccess(logger)
            .Should()
            .Be(Result<int>.Failure(error));
        logger.IsLogCalled
            .Should().BeFalse();
    }

    internal class LoggerFixtureMock : ILogger
    {
        internal bool IsLogCalled { get; private set; }

        public LoggerFixtureMock()
            => IsLogCalled = false;

        public void Log<TState>(
            LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
            => IsLogCalled = true;

        public bool IsEnabled(LogLevel logLevel) => true;

        public IDisposable? BeginScope<TState>(TState state)
            where TState : notnull
            => new CancellationDisposable();
    }
}
