using System.Globalization;
using FluentAssertions;
using Xunit.Abstractions;
using YaMonads.UnitTests.Helpers;

namespace YaMonads.UnitTests;

public class ResultTestsSwitchesAndMatches
{
    private readonly ITestOutputHelper _testOutputHelper;
    private static readonly Random Random = new();

    public ResultTestsSwitchesAndMatches(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Theory]
    [ClassData(typeof(DecimalsProvider))]
    private void Switch_OfDecimalAndString_ValidResultGiven_CorrectActionExecuted(
        decimal okValue)
    {
        // Arrange
        var result = Result<decimal, string>.Ok(okValue);
        var wasCorrectActionExecuted = false;
        decimal? shouldValue = null;

        // Act
        result.Switch(
            whenOk: val =>
            {
                wasCorrectActionExecuted = true;
                shouldValue = val;
            },
            whenErr: err =>
            {
                _testOutputHelper.WriteLine(err);
                wasCorrectActionExecuted = false;
            });

        // Assert
        wasCorrectActionExecuted.Should().BeTrue();
        shouldValue.Should()
            .NotBeNull()
            .And
            .Be(okValue);
    }

    [Theory]
    [ClassData(typeof(StringsProvider))]
    private void Switch_OfDecimalAndString_InvalidResultGiven_CorrectActionExecuted(
        string errValue)
    {
        // Arrange
        var result = Result<decimal, string>.Err(errValue);
        var wasCorrectActionExecuted = false;
        string? shouldValue = null;

        // Act
        result.Switch(
            whenOk: actual =>
            {
                _testOutputHelper.WriteLine(actual.ToString(CultureInfo.InvariantCulture));
                wasCorrectActionExecuted = false;
            },
            whenErr:
            val =>
            {
                wasCorrectActionExecuted = true;
                shouldValue = val;
            });

        // Assert
        wasCorrectActionExecuted.Should().BeTrue();
        shouldValue.Should()
            .NotBeNull()
            .And
            .Be(errValue);
    }

    [Theory]
    [ClassData(typeof(DecimalsProvider))]
    private async Task SwitchAsync_OfDecimalAndString_ValidResultGiven_CorrectActionExecuted(
        decimal okValue)
    {
        // Arrange
        var result = Result<decimal, string>.Ok(okValue);
        var wasCorrectActionExecuted = false;
        decimal? shouldValue = null;

        // Act
        await result.SwitchAsync(
            whenOk: async val =>
            {
                await Task.Delay(Random.Next(100, 400));
                wasCorrectActionExecuted = true;
                shouldValue = val;
            },
            whenErr: async err =>
            {
                await Task.Delay(Random.Next(100, 400));
                _testOutputHelper.WriteLine(err);
                wasCorrectActionExecuted = false;
            });

        // Assert
        wasCorrectActionExecuted.Should().BeTrue();
        shouldValue.Should()
            .NotBeNull()
            .And
            .Be(okValue);
    }

    [Theory]
    [ClassData(typeof(StringsProvider))]
    private async Task SwitchAsync_OfDecimalAndString_InvalidResultGiven_CorrectActionExecuted(
        string errValue)
    {
        // Arrange
        var result = Result<decimal, string>.Err(errValue);
        var wasCorrectActionExecuted = false;
        string? shouldValue = null;

        // Act
        await result.SwitchAsync(
            whenOk: async actual =>
            {
                await Task.Delay(Random.Next(100, 400));
                _testOutputHelper.WriteLine(actual.ToString(CultureInfo.InvariantCulture));
                wasCorrectActionExecuted = false;
            },
            whenErr: async val =>
            {
                await Task.Delay(Random.Next(100, 400));
                wasCorrectActionExecuted = true;
                shouldValue = val;
            });

        // Assert
        wasCorrectActionExecuted.Should().BeTrue();
        shouldValue.Should()
            .NotBeNull()
            .And
            .Be(errValue);
    }

    [Theory]
    [ClassData(typeof(DecimalsProvider))]
    private void Match_OfDecimalAndString_ValidResultGiven_CorrectFuncExecuted(
        decimal okValue)
    {
        // Arrange
        var result = Result<decimal, string>.Ok(okValue);
        decimal? shouldValue = null;

        // Act
        var wasCorrectActionExecuted = result.Match(
            forOk: val =>
            {
                shouldValue = val;
                return true;
            },
            forErr: err =>
            {
                _testOutputHelper.WriteLine(err);
                return false;
            });

        // Assert
        wasCorrectActionExecuted.Should().BeTrue();
        shouldValue.Should()
            .NotBeNull()
            .And
            .Be(okValue);
    }

    [Theory]
    [ClassData(typeof(ExceptionsProvider))]
    private void Match_OfDecimalAndException_InvalidResultGiven_CorrectFuncExecuted(
        Exception errValue)
    {
        // Arrange
        var result = Result<decimal, Exception>.Err(errValue);
        Exception? shouldValue = null;

        // Act
        var wasCorrectActionExecuted = result.Match(
            forOk: actual =>
            {
                _testOutputHelper.WriteLine(actual.ToString(CultureInfo.InvariantCulture));
                return false;
            },
            forErr:
            val =>
            {
                shouldValue = val;
                return true;
            });

        // Assert
        wasCorrectActionExecuted.Should().BeTrue();
        shouldValue.Should()
            .NotBeNull()
            .And
            .Be(errValue);
    }

    [Theory]
    [ClassData(typeof(DecimalsProvider))]
    private async Task MatchAsync_OfDecimalAndString_ValidResultGiven_CorrectFuncExecuted(
        decimal okValue)
    {
        // Arrange
        var result = Result<decimal, string>.Ok(okValue);
        decimal? shouldValue = null;

        // Act
        var wasCorrectActionExecuted = await result.MatchAsync(
            forOk: async val =>
            {
                await Task.Delay(Random.Next(100, 400));
                shouldValue = val;
                return true;
            },
            forErr: async err =>
            {
                await Task.Delay(Random.Next(100, 400));
                _testOutputHelper.WriteLine(err);
                return false;
            });

        // Assert
        wasCorrectActionExecuted.Should().BeTrue();
        shouldValue.Should()
            .NotBeNull()
            .And
            .Be(okValue);
    }

    [Theory]
    [ClassData(typeof(ExceptionsProvider))]
    private async Task MatchAsync_OfDecimalAndException_InvalidResultGiven_CorrectFuncExecuted(
        Exception errValue)
    {
        // Arrange
        var result = Result<decimal, Exception>.Err(errValue);
        Exception? shouldValue = null;

        // Act
        var wasCorrectActionExecuted = await result.MatchAsync(
            forOk: async actual =>
            {
                await Task.Delay(Random.Next(100, 400));
                _testOutputHelper.WriteLine(actual.ToString(CultureInfo.InvariantCulture));
                return false;
            },
            forErr: async val =>
            {
                await Task.Delay(Random.Next(100, 400));
                shouldValue = val;
                return true;
            });

        // Assert
        wasCorrectActionExecuted.Should().BeTrue();
        shouldValue.Should()
            .NotBeNull()
            .And
            .Be(errValue);
    }
}