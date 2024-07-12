using FluentAssertions;
using YaMonads.UnitTests.Helpers;

namespace YaMonads.UnitTests;

public class ResultTestsImplicitOperations
{

    [Theory]
    [ClassData(typeof(DecimalsProvider))]
    public void ImplicitOk_DecimalsIn_ValidResultsOut(decimal value)
    {
        // Arrange

        // Act
        Result<decimal, string> actualRes = value;
        var act = () => actualRes.Unwrap();

        // Assert
        actualRes.IsOk.Should().BeTrue();
        actualRes.IsErr.Should().BeFalse();
        act.Should().NotThrow();
        var res = act();
        res.Should().Be(value);
    }
    

    [Theory]
    [ClassData(typeof(ExceptionsProvider))]
    public void ImplicitOk_ExceptionsIn_ValidResultsOut<TE>(TE value) where TE : Exception
    {
        // Arrange

        // Act
        Result<TE, string> actualRes = value;
        var act = () => actualRes.Unwrap();

        // Assert
        actualRes.IsOk.Should().BeTrue();
        actualRes.IsErr.Should().BeFalse();
        act.Should().NotThrow();
        var res = act();
        res.Should().Be(value);
    }
    
    [Theory]
    [ClassData(typeof(DecimalsProvider))]
    public void ImplicitErr_DecimalsIn_ValidResultsOut(decimal value)
    {
        // Arrange

        // Act
        Result<string, decimal> actualRes = value;
        var normalUnwrap = () => actualRes.Unwrap();
        var errorUnwrap = () => actualRes.UnwrapError();

        // Assert
        actualRes.IsOk.Should().BeFalse();
        actualRes.IsErr.Should().BeTrue();
        normalUnwrap.Should().Throw<AccessViolationException>()
            .WithMessage($"Value is invalid! Error: {value}");
        errorUnwrap.Should().NotThrow();
        var err = errorUnwrap();
        err.Should().Be(value);
    }
    

    [Theory]
    [ClassData(typeof(ExceptionsProvider))]
    public void ImplicitErr_ExceptionsIn_ValidResultsOut<TE>(TE value) where TE : Exception
    {
        // Arrange

        // Act
        Result<string, TE> actualRes = value;
        var normalUnwrap = () => actualRes.Unwrap();
        var errorUnwrap = () => actualRes.UnwrapError();

        // Assert
        actualRes.IsOk.Should().BeFalse();
        actualRes.IsErr.Should().BeTrue();
        normalUnwrap.Should().Throw<AccessViolationException>()
            .WithMessage($"Value is invalid! Error: {value}");
        errorUnwrap.Should().NotThrow();
        var err = errorUnwrap();
        err.Should().Be(value);
    }
}