using System.Collections;

namespace YaMonads.UnitTests.Helpers;

public class DecimalsProvider : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return [5];
        yield return [3.141045];
        yield return [Math.PI];
        yield return [0];
        yield return [0.0];
        yield return [int.MaxValue];
        yield return [int.MinValue];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}