using FluentAssertions;
using YaMonads.UnitTests.Helpers;

namespace YaMonads.UnitTests;

public class ResultTestsMapsAndBinds
{
    private static readonly Random Random = new();

    [Theory]
    [ClassData(typeof(OkResultsProvider))]
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
    [ClassData(typeof(OkResultsProvider))]
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
    [ClassData(typeof(ErrResultsProvider))]
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
    [ClassData(typeof(OkResultsProvider))]
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
    [ClassData(typeof(OkResultsProvider))]
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
    [ClassData(typeof(ErrResultsProvider))]
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
    
    [Theory]
    [ClassData(typeof(OkResultsProvider))]
    public void Bind_ManyOkResultsIn_BindToNewWithSelfAlwaysGivesValidResultOut<TOk, TErr>(
        Result<TOk, TErr> okResult)
    {
        // Arrange

        // Act
        var actualRes = okResult.Bind(Result<TOk, TErr>.Ok);
        var act = () => actualRes.Unwrap();

        // Assert
        actualRes.IsOk.Should().BeTrue();
        actualRes.IsErr.Should().BeFalse();
        act.Should().NotThrow();
        var res = act();
        res.Should().BeEquivalentTo(okResult.Unwrap());
    }
    
    [Theory]
    [ClassData(typeof(OkResultsProvider))]
    public void Bind_ManyOkResultsIn_BindToDifferentResultAlwaysGivesValidResultOut<TOk, TErr>(
        Result<TOk, TErr> okResult)
    {
        // Arrange

        // Act
        var actualRes = okResult.Bind(val => Result<string?, TErr>.Ok(val!.ToString()));
        var act = () => actualRes.Unwrap();

        // Assert
        actualRes.IsOk.Should().BeTrue();
        actualRes.IsErr.Should().BeFalse();
        act.Should().NotThrow();
        var res = act();
        res.Should().BeEquivalentTo(okResult.Unwrap()!.ToString());
    }
    
    [Theory]
    [ClassData(typeof(ErrResultsProvider))]
    public void Bind_ManyErrResultsIn_BindToSelfAlwaysGivesSameErrorResultOut<TOk, TErr>(
        Result<TOk, TErr> okResult)
    {
        // Arrange

        // Act
        var actualRes = okResult.Bind(Result<TOk, TErr>.Ok);
        var act = () => actualRes.UnwrapError();

        // Assert
        actualRes.IsOk.Should().BeFalse();
        actualRes.IsErr.Should().BeTrue();
        act.Should().NotThrow();
        var res = act();
        res.Should().BeEquivalentTo(okResult.UnwrapError());
    }
    
    [Theory]
    [ClassData(typeof(OkResultsProvider))]
    public async Task BindAsync_ManyOkResultsIn_BindToNewWithSelfAlwaysGivesValidResultOut<TOk, TErr>(
        Result<TOk, TErr> okResult)
    {
        // Arrange

        // Act
        var actualRes = await okResult.BindAsync(async val =>
        {
            await Task.Delay(Random.Next(1, 10));
            return Result<TOk, TErr>.Ok(val);
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
    [ClassData(typeof(OkResultsProvider))]
    public async Task BindAsync_ManyOkResultsIn_BindToDifferentResultAlwaysGivesValidResultOut<TOk, TErr>(
        Result<TOk, TErr> okResult)
    {
        // Arrange

        // Act
        var actualRes = await okResult.BindAsync(async val =>
        {
            await Task.Delay(Random.Next(1, 10));
            return Result<string?, TErr>.Ok(val!.ToString());
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
    [ClassData(typeof(ErrResultsProvider))]
    public async Task BindAsync_ManyErrResultsIn_BindToSelfAlwaysGivesSameErrorResultOut<TOk, TErr>(
        Result<TOk, TErr> okResult)
    {
        // Arrange

        // Act
        var actualRes = await okResult.BindAsync(async val =>
        {
            await Task.Delay(Random.Next(1, 10));
            return Result<TOk, TErr>.Ok(val);
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