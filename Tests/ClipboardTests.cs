using System;
using System.Threading;
using System.Windows;
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
        TextCopy.Clipboard.SetText(expected);

        Exception caught = null;
        var t = new Thread(() =>
        {
            try
            {
                var actual = Clipboard.GetText();
                Assert.Equal(expected, actual);
            }
            catch (Exception e)
            {
                caught = e;
            }
        });

        t.SetApartmentState(ApartmentState.STA);
        t.Start();
        t.Join();

        if (caught != null)
        {
            throw caught;
        }
    }

    ITestOutputHelper output;

    public ClipboardTests(ITestOutputHelper output)
    {
        this.output = output;
    }
}