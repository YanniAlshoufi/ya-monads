using System.Collections;
using System.Data;

namespace YaMonads.UnitTests.Helpers;

public class TestingOkResultsProvider : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return [Result<string, string>.Ok("Hello there! :)")];
        yield return [Result<double, string>.Ok(Math.PI)];
        yield return [Result<float, string>.Ok(4354.5593f)];
        yield return [Result<decimal, string>.Ok(9340.34m)];
        yield return [Result<char, string>.Ok('a')];
        yield return [Result<double, string>.Ok(double.NaN)];
        yield return [Result<Exception, string>.Ok(new NotImplementedException())];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}