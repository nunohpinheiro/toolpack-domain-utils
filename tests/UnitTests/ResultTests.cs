namespace UnitTests;

using FluentAssertions;
using ToolPack.DomainUtils.Functional;
using ToolPack.DomainUtils.Functional.Output;

public class ResultTests
{
    [Test]
    public void NOk_CreatesNOk_IsNOk_HasError()
    {
        var error = new Error("error-message");
        var nok = Result.NOk(error);
        nok.Should().Match<NOk<Error>>(x
            => x.IsNOk && !x.IsOk && x.Error == error);
    }

    [Test]
    public void Ok_CreatesOk_IsOk()
        => Result.Ok()
        .Should().Match<Ok>(x => x.IsOk && !x.IsNOk);

    [Test]
    public void OkValue_CreatesOk_IsOk_HasValue()
    {
        var value = "just-a-value-string";
        var ok = Result.Ok(value);
        ok.Should().Match<Ok<string>>(x
            => x.IsOk && !x.IsNOk && x.Value == value);
    }

    [Test]
    public void TryGetNOkError_IsNOk_GetsError()
    {
        // Arrange
        var error = new Error("error-message");
        var nok = Result.NOk(error);

        // Act
        var hasValue = nok.TryGetNOkError(out Error errorOutput);

        // Assert
        hasValue.Should().BeTrue();
        errorOutput.Should().BeEquivalentTo(error);
    }

    [Test]
    public void TryGetNOkError_IsNOk_IsResult_GetsError()
    {
        // Arrange
        var error = new Error("error-message");
        var result = (Result)Result.NOk(error);

        // Act
        var hasValue = result.TryGetNOkError(out Error errorOutput);

        // Assert
        hasValue.Should().BeTrue();
        errorOutput.Should().BeEquivalentTo(error);
    }

    [Test]
    public void TryGetNOkError_IsNOk_WithDifferentErrorType_ReturnsFalse()
    {
        // Arrange
        var nok = Result.NOk(new Error("error-message"));

        // Act
        var hasValue = nok.TryGetNOkError(out string inexistentError);

        // Assert
        hasValue.Should().BeFalse();
        inexistentError.Should().BeNull();
    }

    [Test]
    public void TryGetNOkError_IsOk_ReturnsFalse()
    {
        // Arrange
        var ok = Result.Ok();

        // Act
        var hasValue = ok.TryGetNOkError(out Error errorOutput);

        // Assert
        hasValue.Should().BeFalse();
        errorOutput.Should().BeNull();
    }

    [Test]
    public void TryGetNOkError_IsOk_IsResult_ReturnsFalse()
    {
        // Arrange
        var result = (Result)Result.Ok();

        // Act
        var hasValue = result.TryGetNOkError(out Error errorOutput);

        // Assert
        hasValue.Should().BeFalse();
        errorOutput.Should().BeNull();
    }

    [Test]
    public void TryGetOkValue_IsOkValue_GetsValue()
    {
        // Arrange
        var value = "sample-value";
        var ok = Result.Ok(value);

        // Act
        var hasValue = ok.TryGetOkValue(out string valueOutput);

        // Assert
        hasValue.Should().BeTrue();
        valueOutput.Should().BeEquivalentTo(value);
    }

    [Test]
    public void TryGetOkValue_IsOkValue_IsResult_GetsValue()
    {
        // Arrange
        var value = "sample-value";
        var result = (Result)Result.Ok(value);

        // Act
        var hasValue = result.TryGetOkValue(out string valueOutput);

        // Assert
        hasValue.Should().BeTrue();
        valueOutput.Should().BeEquivalentTo(value);
    }

    [Test]
    public void TryGetOkValue_IsOkValue_WithDifferentErrorType_ReturnsFalse()
    {
        // Arrange
        var ok = Result.Ok("the result");

        // Act
        var hasValue = ok.TryGetOkValue(out object inexistentValue);

        // Assert
        hasValue.Should().BeFalse();
        inexistentValue.Should().BeNull();
    }

    [Test]
    public void TryGetOkValue_IsNOk_ReturnsFalse()
    {
        // Arrange
        var nok = Result.NOk("a-mistake");

        // Act
        var hasValue = nok.TryGetOkValue(out object outputValue);

        // Assert
        hasValue.Should().BeFalse();
        outputValue.Should().BeNull();
    }

    [Test]
    public void TryGetOkValue_IsNOk_IsResult_ReturnsFalse()
    {
        // Arrange
        var result = (Result)Result.NOk("a-mistake");

        // Act
        var hasValue = result.TryGetOkValue(out object outputValue);

        // Assert
        hasValue.Should().BeFalse();
        outputValue.Should().BeNull();
    }
}
