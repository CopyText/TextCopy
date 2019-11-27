using TextCopy;
using VerifyXunit;
using Xunit;
using Xunit.Abstractions;

public class ClipboardTests :
    VerifyBase
{
    [Fact]
    public void Simple()
    {
        VerifyInner("Foo");
        VerifyInner("🅢");
    }

    static void VerifyInner(string expected)
    {
        Clipboard.SetText(expected);

        var actual = Clipboard.GetText();
        Assert.Equal(expected, actual);
    }

    public ClipboardTests(ITestOutputHelper output) :
        base(output)
    {
    }
}