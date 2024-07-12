using System.Collections;

namespace YaMonads.UnitTests.Helpers;

public class TestingErrorStringsProvider : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return ["Nope!"];
        yield return ["Error!"];
        yield return ["Invalid!"];
        yield return ["Not valid!"];
        yield return ["Not good!"];
        yield return ["I am to be very expected! :)"];
        yield return [string.Empty];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}