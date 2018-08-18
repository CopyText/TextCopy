using TextCopy;
using Xunit;
using Xunit.Abstractions;

public class ClipboardTests
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

    ITestOutputHelper output;

    public ClipboardTests(ITestOutputHelper output)
    {
        this.output = output;
    }
}