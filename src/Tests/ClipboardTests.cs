using System.Threading.Tasks;
using TextCopy;
using VerifyXunit;
using Xunit;
using Xunit.Abstractions;

public class ClipboardTests :
    VerifyBase
{
    [Fact]
    public async Task Simple()
    {
        VerifyInner("Foo");
        VerifyInner("🅢");
        await VerifyInnerAsync("Foo");
        await VerifyInnerAsync("🅢");
    }

    static void VerifyInner(string expected)
    {
        Clipboard.SetText(expected);

        var actual = Clipboard.GetText();
        Assert.Equal(expected, actual);
    }

    static async Task VerifyInnerAsync(string expected)
    {
        await Clipboard.SetTextAsync(expected);

        var actual = await Clipboard.GetTextAsync();
        Assert.Equal(expected, actual);
    }

    public ClipboardTests(ITestOutputHelper output) :
        base(output)
    {
    }
}