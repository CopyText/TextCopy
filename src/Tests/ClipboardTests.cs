using TextCopy;

[TestFixture]
public class ClipboardTests
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

        var actual = new Clipboard().GetText();
        Assert.AreEqual(expected, actual);
    }

    static async Task VerifyInnerAsync(string expected)
    {
        await new Clipboard().SetTextAsync(expected);

        var actual = await ClipboardService.GetTextAsync();
        Assert.AreEqual(expected, actual);
    }
}