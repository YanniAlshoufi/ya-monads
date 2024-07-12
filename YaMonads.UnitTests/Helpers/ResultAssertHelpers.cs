using FluentAssertions;

namespace YaMonads.UnitTests.Helpers;

public static class ResultAssertHelpers
{
    public static void AssertResultValidWith<TOk, TErr>(Result<TOk, TErr> result, TOk expectedOkValue)
    {
        // Act
        Action act = () => result.UnwrapError();

        // Assert
        result.IsOk.Should().BeTrue();
        result.IsErr.Should().BeFalse();
        result.Unwrap().Should().Be(expectedOkValue);

        act.Should().Throw<AccessViolationException>()
            .WithMessage("The result is valid!");
    }

    public static void AssertResultInvalidWith<TOk, TErr>(Result<TOk, TErr> result, TErr expectedErrValue)
    {
        // Act
        var act = result.Unwrap;

        // Assert
        result.IsOk.Should().BeFalse();
        result.IsErr.Should().BeTrue();
        result.UnwrapError().Should().Be(expectedErrValue);
        act.Should().Throw<AccessViolationException>()
            .WithMessage($"Value is invalid! Error: {expectedErrValue}");
    }
}