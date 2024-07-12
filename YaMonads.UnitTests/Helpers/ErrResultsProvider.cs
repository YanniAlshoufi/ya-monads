using System.Collections;
using System.Data;

namespace YaMonads.UnitTests.Helpers;

public class ErrResultsProvider : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return [Result<string, string>.Err("Hello there! :)")];
        yield return [Result<string, double>.Err(Math.PI)];
        yield return [Result<string, float>.Err(4354.5593f)];
        yield return [Result<string, decimal>.Err(9340.34m)];
        yield return [Result<string, char>.Err('a')];
        yield return [Result<string, double>.Err(double.NaN)];
        yield return [Result<string, Exception>.Err(new NotImplementedException())];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}