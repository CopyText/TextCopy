using TextCopy;
using Xunit;

public class ClipboardTests
{
    [Fact]
    public void Simple()
    {
        Verify("Foo");
        Verify("🅢");
    }

    [Fact]
    public void Supported()
    {
        Assert.True(Clipboard.IsGetSupported);
        Assert.True(Clipboard.IsSetSupported);
    }

    static void Verify(string expected)
    {
        Clipboard.SetText(expected);

        var actual = Clipboard.GetText();
        Assert.Equal(expected, actual);
    }
}