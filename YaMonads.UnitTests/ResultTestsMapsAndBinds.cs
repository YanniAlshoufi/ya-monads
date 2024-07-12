using System.Globalization;
using FluentAssertions;
using Xunit.Abstractions;
using YaMonads.UnitTests.Helpers;
using static YaMonads.UnitTests.Helpers.ResultAssertHelpers;

namespace YaMonads.UnitTests;

public class ResultTestsMapsAndBinds
{
    private readonly ITestOutputHelper _testOutputHelper;
    private static readonly Random Random = new();

    public ResultTestsMapsAndBinds(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Theory]
    [ClassData(typeof(TestingOkResultsProvider))]
    public void Map_ManyOkResultsIn_MapToSelfAlwaysGivesValidResultOut<TOk, TErr>(
        Result<TOk, TErr> okResult)
    {
        // Arrange

        // Act
        var actualRes = okResult.Map(val => val);
        var act = () => actualRes.Unwrap();

        // Assert
        actualRes.IsOk.Should().BeTrue();
        actualRes.IsErr.Should().BeFalse();
        act.Should().NotThrow();
        var res = act();
        res.Should().BeEquivalentTo(okResult.Unwrap());
    }

    [Theory]
    [ClassData(typeof(TestingOkResultsProvider))]
    public void Map_ManyOkResultsIn_MapToSomethingElseAlwaysGivesValidResultOut<TOk, TErr>(
        Result<TOk, TErr> okResult)
    {
        // Arrange

        // Act
        var actualRes = okResult.Map(val => val!.ToString());
        var act = () => actualRes.Unwrap();

        // Assert
        actualRes.IsOk.Should().BeTrue();
        actualRes.IsErr.Should().BeFalse();
        act.Should().NotThrow();
        var res = act();
        res.Should().BeEquivalentTo(okResult.Unwrap()!.ToString());
    }

    [Theory]
    [ClassData(typeof(TestingErrResultsProvider))]
    public void Map_ManyErrResultsIn_MapToSelfAlwaysGivesSameErrorResultOut<TOk, TErr>(
        Result<TOk, TErr> okResult)
    {
        // Arrange

        // Act
        var actualRes = okResult.Map(val => val);
        var act = () => actualRes.UnwrapError();

        // Assert
        actualRes.IsOk.Should().BeFalse();
        actualRes.IsErr.Should().BeTrue();
        act.Should().NotThrow();
        var res = act();
        res.Should().BeEquivalentTo(okResult.UnwrapError());
    }
    

    [Theory]
    [ClassData(typeof(TestingOkResultsProvider))]
    public async Task MapAsync_ManyOkResultsIn_MapToSelfAlwaysGivesValidResultOut<TOk, TErr>(
        Result<TOk, TErr> okResult)
    {
        // Arrange

        // Act
        var actualRes = await okResult.MapAsync(async val =>
        {
            await Task.Delay(Random.Next(1, 10));
            return val;
        });
        var act = () => actualRes.Unwrap();

        // Assert
        actualRes.IsOk.Should().BeTrue();
        actualRes.IsErr.Should().BeFalse();
        act.Should().NotThrow();
        var res = act();
        res.Should().BeEquivalentTo(okResult.Unwrap());
    }

    [Theory]
    [ClassData(typeof(TestingOkResultsProvider))]
    public async Task MapAsync_ManyOkResultsIn_MapToSomethingElseAlwaysGivesValidResultOut<TOk, TErr>(
        Result<TOk, TErr> okResult)
    {
        // Arrange

        // Act
        var actualRes = await okResult.MapAsync(async val =>
        {
            await Task.Delay(Random.Next(1, 10));
            return val!.ToString();
        });
        var act = () => actualRes.Unwrap();

        // Assert
        actualRes.IsOk.Should().BeTrue();
        actualRes.IsErr.Should().BeFalse();
        act.Should().NotThrow();
        var res = act();
        res.Should().BeEquivalentTo(okResult.Unwrap()!.ToString());
    }

    [Theory]
    [ClassData(typeof(TestingErrResultsProvider))]
    public async Task MapAsync_ManyErrResultsIn_MapToSelfAlwaysGivesSameErrorResultOut<TOk, TErr>(
        Result<TOk, TErr> okResult)
    {
        // Arrange

        // Act
        var actualRes = await okResult.MapAsync(async val =>
        {
            await Task.Delay(Random.Next(1, 10));
            return val;
        });
        var act = () => actualRes.UnwrapError();

        // Assert
        actualRes.IsOk.Should().BeFalse();
        actualRes.IsErr.Should().BeTrue();
        act.Should().NotThrow();
        var res = act();
        res.Should().BeEquivalentTo(okResult.UnwrapError());
    }
}