using FluentAssertions;

namespace YaMonads.UnitTests;

public class ResultTestsEmptyConstructor
{
    [Fact]
    public void EmptyConstructor_WhenCalled_ResultIsOk()
    {
        // Arrange

        // Act
        var act = () => new Result<int, string>();

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Please provide a value!");
    }
}