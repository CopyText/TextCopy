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
        Verify("Foo");
        Verify("🅢");
    }

    static void Verify(string expected)
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