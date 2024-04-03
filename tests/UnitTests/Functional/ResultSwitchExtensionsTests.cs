namespace UnitTests.Functional;

public class ResultSwitchExtensionsTests
{
    [Test]
    public void Switch_ResultInputIsSuccess_RunsSuccessAction()
    {
        ActionCalled actionCalled = new();

        Result<int>.Success(1)
            .Switch(
                () => actionCalled.SetSuccess(),
                () => actionCalled.SetError());

        actionCalled.SuccessCalled
            .Should().BeTrue();
    }

    [Test]
    public void Switch_ResultInputIsError_RunsErrorAction()
    {
        ActionCalled actionCalled = new();

        Result<int>.Failure(new Error("error"))
            .Switch(
                () => actionCalled.SetSuccess(),
                () => actionCalled.SetError());

        actionCalled.ErrorCalled
            .Should().BeTrue();
    }

    [Test]
    public async Task SwitchAsync_ResultInputIsSuccess_RunsSuccessAsyncAction()
    {
        ActionCalled actionCalled = new();

        await Result<int>.Success(1)
            .SwitchAsync(
                () => actionCalled.SetSuccessAsync(),
                () => actionCalled.SetErrorAsync());

        actionCalled.SuccessCalled
            .Should().BeTrue();
    }

    [Test]
    public async Task SwitchAsync_ResultInputIsError_RunsErrorAsyncAction()
    {
        ActionCalled actionCalled = new();

        await Result<int>.Failure(new Error("error"))
            .SwitchAsync(
                () => actionCalled.SetSuccessAsync(),
                () => actionCalled.SetErrorAsync());

        actionCalled.ErrorCalled
            .Should().BeTrue();
    }

    internal class ActionCalled
    {
        internal bool ErrorCalled { get; private set; }
        internal bool SuccessCalled { get; private set; }
        internal void SetError() => ErrorCalled = true;
        internal void SetSuccess() => SuccessCalled = true;
        internal Task SetErrorAsync()
        {
            ErrorCalled = true;
            return Task.CompletedTask;
        }
        internal Task SetSuccessAsync()
        {
            SuccessCalled = true;
            return Task.CompletedTask;
        }
    }
}
