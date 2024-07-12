using FluentAssertions;
using YaMonads.UnitTests.Helpers;
using static YaMonads.UnitTests.Helpers.ResultAssertHelpers;

namespace YaMonads.UnitTests;

public class ResultTestsInitsAndUnwraps
{
    [Theory]
    [ClassData(typeof(DecimalsProvider))]
    private void Ok_OfDecimalAndString_DecimalsGiven_ResultsWithOkCreated(
        decimal okValue)
    {
        // Arrange
        var result = Result<decimal, string>.Ok(okValue);
        // Act
        // Assert
        AssertResultValidWith(result, okValue);
    }

    [Theory]
    [ClassData(typeof(ObjectsProvider))]
    public void Ok_OfObjectAndString_ObjectGiven_ResultsWithOkCreated(
        object okValue)
    {
        // Arrange
        var result = Result<object, string>.Ok(okValue);

        // Act
        // Assert
        AssertResultValidWith(result, okValue);
    }

    [Fact]
    private void Ok_OfObjectAndString_NullGiven_NullReferenceExceptionThrown()
    {
        // Arrange
        // Act
        Action act = () => Result<object, string>.Ok(null!);

        // Assert
        act.Should().Throw<NullReferenceException>()
            .WithMessage("`null` is not a valid value!");
    }

    [Theory]
    [ClassData(typeof(StringsProvider))]
    private void Err_OfDecimalAndString_StringErrorsGiven_ResultsWithSameStringCreated(
        string errValue)
    {
        // Arrange
        var result = Result<decimal, string>.Err(errValue);

        // Act
        // Assert
        AssertResultInvalidWith(result, errValue);
    }

    [Theory]
    [ClassData(typeof(ExceptionsProvider))]
    private void Err_OfDecimalAndException_ExceptionErrorsGiven_ResultsWithSameExceptionCreated(
        Exception errValue)
    {
        // Arrange
        var result = Result<decimal, Exception>.Err(errValue);

        // Act
        // Assert
        AssertResultInvalidWith(result, errValue);
    }

    [Fact]
    private void Err_OfObjectAndException_NullGiven_NullReferenceExceptionThrown()
    {
        // Arrange
        // Act
        Action act = () => Result<object, Exception>.Err(null!);

        // Assert
        act.Should().Throw<NullReferenceException>()
            .WithMessage("`null` is not a valid value!");
    }
}