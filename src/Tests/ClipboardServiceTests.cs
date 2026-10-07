using System.Runtime.InteropServices;
using TextCopy;

public class ClipboardServiceTests
{
    [Test]
    public async Task Simple()
    {
        VerifyInner("Foo");
        VerifyInner("🅢");
        await VerifyInnerAsync("Foo");
        await VerifyInnerAsync("🅢");
    }

    [Test]
    public async Task PassesCancellationToken()
    {
        // A token that is never cancelled must not interfere with normal operation.
        using var source = new CancellationTokenSource();
        var token = source.Token;

        await ClipboardService.SetTextAsync("Foo", token);

        var actual = await ClipboardService.GetTextAsync(token);
        Assert.AreEqual("Foo", actual);
    }

    static void VerifyInner(string expected)
    {
        ClipboardService.SetText(expected);

        var actual = ClipboardService.GetText();
        Assert.AreEqual(expected, actual);
    }

    static async Task VerifyInnerAsync(string expected)
    {
        await ClipboardService.SetTextAsync(expected);

        var actual = await ClipboardService.GetTextAsync();
        Assert.AreEqual(expected, actual);
    }
}