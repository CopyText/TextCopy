#if (NETSTANDARD2_0 || NETFRAMEWORK)
using System.IO;
using System.Threading;
using System.Threading.Tasks;

static class LinuxClipboard
{
    public static Task SetTextAsync(string text, CancellationToken cancellation)
    {
        SetText(text);

        return Task.CompletedTask;
    }

    public static void SetText(string text)
    {
        var tempFileName = Path.GetTempFileName();
        File.WriteAllText(tempFileName, text);
        try
        {
            BashRunner.Run($"cat {tempFileName} | xsel -i --clipboard");
        }
        finally
        {
            File.Delete(tempFileName);
        }
    }

    public static Task<string?> GetTextAsync(CancellationToken cancellation)
    {
        return Task.FromResult(GetText());
    }

    public static string? GetText()
    {
        var tempFileName = Path.GetTempFileName();
        try
        {
            BashRunner.Run($"xsel -o --clipboard > {tempFileName}");
            var readAllText = File.ReadAllText(tempFileName);
            // ReSharper disable once RedundantTypeArgumentsOfMethod
            return readAllText;
        }
        finally
        {
            File.Delete(tempFileName);
        }
    }
}
#endif