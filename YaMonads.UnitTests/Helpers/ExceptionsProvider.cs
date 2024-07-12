using System.Collections;

namespace YaMonads.UnitTests.Helpers;

public class ExceptionsProvider : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return [new Exception()];
        yield return [new NullReferenceException()];
        yield return [new ArgumentException()];
        yield return [new IOException()];
        yield return [new DllNotFoundException()];
        yield return [new AggregateException()];
        yield return [new ApplicationException()];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}