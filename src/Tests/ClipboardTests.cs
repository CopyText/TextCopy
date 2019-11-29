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
        await VerifyInner("Foo");
        await VerifyInner("🅢");
    }

    static async Task VerifyInner(string expected)
    {
        await Clipboard.SetText(expected);

        var actual = await Clipboard.GetText();
        Assert.Equal(expected, actual);
    }

    public ClipboardTests(ITestOutputHelper output) :
        base(output)
    {
    }
}