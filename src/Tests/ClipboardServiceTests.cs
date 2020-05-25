using System.Threading.Tasks;
using TextCopy;
using VerifyXunit;
using Xunit;
using Xunit.Abstractions;

public class ClipboardServiceTests :
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
        ClipboardService.SetText(expected);

        var actual = ClipboardService.GetText();
        Assert.Equal(expected, actual);
    }

    static async Task VerifyInnerAsync(string expected)
    {
        await ClipboardService.SetTextAsync(expected);

        var actual = await ClipboardService.GetTextAsync();
        Assert.Equal(expected, actual);
    }

    public ClipboardServiceTests(ITestOutputHelper output) :
        base(output)
    {
    }
}