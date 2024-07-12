using System.Collections;

namespace YaMonads.UnitTests.Helpers;

public class ObjectsProvider : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return [5];
        yield return [3.141045];
        yield return [Math.PI];
        yield return [0];
        yield return [0.0];
        yield return [4.88f];
        yield return [double.NaN];
        yield return [double.PositiveInfinity];
        yield return [float.PositiveInfinity];
        yield return [double.NegativeInfinity];
        yield return [new Exception()];
        yield return [new NullReferenceException()];
        yield return [new object()];
        yield return [true];
        yield return [false];
        yield return [int.MaxValue];
        yield return [int.MinValue];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}