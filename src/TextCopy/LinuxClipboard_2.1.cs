#if (NETSTANDARD2_1)
using System.IO;
using System.Threading;
using System.Threading.Tasks;

static class LinuxClipboard
{
    public static async Task SetTextAsync(string text, CancellationToken cancellation)
    {
        var tempFileName = Path.GetTempFileName();
        await File.WriteAllTextAsync(tempFileName, text, cancellation);
        try
        {
            if (cancellation.IsCancellationRequested)
            {
                return;
            }

            BashRunner.Run($"cat {tempFileName} | xsel -i --clipboard ");
        }
        finally
        {
            File.Delete(tempFileName);
        }
    }

    public static void SetText(string text)
    {
        var tempFileName = Path.GetTempFileName();
        File.WriteAllText(tempFileName, text);
        try
        {
            BashRunner.Run($"cat {tempFileName} | xsel -i --clipboard ");
        }
        finally
        {
            File.Delete(tempFileName);
        }
    }

    public static string? GetText()
    {
        var tempFileName = Path.GetTempFileName();
        try
        {
            BashRunner.Run($"xsel -o --clipboard > {tempFileName}");
            return File.ReadAllText(tempFileName);
        }
        finally
        {
            File.Delete(tempFileName);
        }
    }

    public static async Task<string?> GetTextAsync(CancellationToken cancellation)
    {
        var tempFileName = Path.GetTempFileName();
        try
        {
            BashRunner.Run($"xsel -o --clipboard  > {tempFileName}");
            return await File.ReadAllTextAsync(tempFileName, cancellation);
        }
        finally
        {
            File.Delete(tempFileName);
        }
    }
}
#endif