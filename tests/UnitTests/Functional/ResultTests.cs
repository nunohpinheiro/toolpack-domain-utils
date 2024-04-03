namespace UnitTests.Functional;

public class ResultTests
{
    [Test]
    public void Result_Combine_FailedResults_ReturnsErrorWithInnerErrors()
    {
        Result<object> resultError1 = new Error("error-message", "one-title");
        Result<string> resultError2 = new Error("error-message", resultError1.Error!.Title);
        Result resultError3 = new Error("another-error-message", "three-title");

        var combinedResult = Result.Combine(resultError1, resultError2, resultError3);

        combinedResult.Should()
            .Match<Result>(x => x.IsFailure && !x.IsSuccess);

        combinedResult.Error.Should()
            .Match<Error>(x
                => x.InnerErrors.Count == 2
                && x.InnerErrors[resultError1.Error!.Title].Length == 2
                && x.InnerErrors[resultError3.Error!.Title].Length == 1
                && x.InnerErrors[resultError1.Error!.Title][0] == resultError1.Error
                && x.InnerErrors[resultError1.Error!.Title][1] == resultError2.Error
                && x.InnerErrors[resultError3.Error!.Title][0] == resultError3.Error);
    }

    [Test]
    public void Result_Combine_SuccessFailedResults_ReturnsErrorWithInnerErrors()
    {
        Result<object> resultError1 = new Error("error-message", "one-title");
        Result<string> resultError2 = new Error("error-message", resultError1.Error!.Title);
        Result resultSuccess = new Success();

        var combinedResult = Result.Combine(resultError1, resultError2, resultSuccess);

        combinedResult.Should()
            .Match<Result>(x => x.IsFailure && !x.IsSuccess);

        combinedResult.Error.Should()
            .Match<Error>(x
                => x.InnerErrors.Count == 1
                && x.InnerErrors[resultError1.Error!.Title].Length == 2
                && x.InnerErrors[resultError1.Error!.Title][0] == resultError1.Error
                && x.InnerErrors[resultError1.Error!.Title][1] == resultError2.Error);
    }

    [Test]
    public void Result_Combine_SuccessResults_ReturnsSuccess()
    {
        Result success1 = new Success();
        Result<int> success2 = 1234567890;

        var combinedResult = Result.Combine(success1, success2);

        combinedResult.Should()
            .Match<Result>(x => x.IsSuccess && !x.IsFailure && x.Value == new Success());
    }

    [Test]
    public void Result_CreateFailure_IsFailure_HasError_MatchesImplicit()
    {
        var error = new Error("error-message");
        var explicitFailure = Result.Failure(error);
        Result implicitFailure = error;

        explicitFailure.Should()
            .Be(implicitFailure)
            .And.Match<Result>(x => x.IsFailure && !x.IsSuccess && x.Error == error);
    }

    [Test]
    public void Result_CreateSuccess_IsSuccess_MatchesImplicit()
    {
        var explicitSuccess = Result.Success();
        Result implicitSuccess = new Success();

        explicitSuccess.Should()
            .Be(implicitSuccess)
            .And.Match<Result>(x => !x.IsFailure && x.IsSuccess && x.Value == new Success());
    }

    [Test]
    public void ResultT_CreateFailure_IsFailure_HasError_MatchesImplicit()
    {
        var error = new Error("error-message");
        var explicitFailure = Result<object>.Failure(error);
        Result<object> implicitFailure = error;

        explicitFailure.Should()
            .Be(implicitFailure)
            .And.Match<Result<object>>(x => x.IsFailure && !x.IsSuccess && x.Error == error);
    }

    [Test]
    public void ResultT_CreateSuccess_IsSuccess_HasValue_MatchesImplicit()
    {
        var successValue = "just-a-value-string";
        var explicitSuccess = Result<string>.Success(successValue);
        Result<string> implicitSuccess = successValue;

        explicitSuccess.Should()
            .Be(implicitSuccess)
            .And.Match<Result<string>>(x => !x.IsFailure && x.IsSuccess && x.Value == successValue);
    }
}
