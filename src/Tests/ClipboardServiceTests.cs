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