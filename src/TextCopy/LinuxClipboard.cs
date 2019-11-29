#if (NETSTANDARD)
using System.IO;
using System.Threading.Tasks;

static class LinuxClipboard
{
    public static Task SetText(string text)
    {
        var tempFileName = Path.GetTempFileName();
        File.WriteAllText(tempFileName, text);
        try
        {
            BashRunner.Run($"cat {tempFileName} | xclip -i -selection clipboard");
        }
        finally
        {
            File.Delete(tempFileName);
        }

        return Task.CompletedTask;
    }

    public static Task<string?> GetText()
    {
        var tempFileName = Path.GetTempFileName();
        try
        {
            BashRunner.Run($"xclip -o -selection clipboard > {tempFileName}");
            var readAllText = File.ReadAllText(tempFileName);
            // ReSharper disable once RedundantTypeArgumentsOfMethod
            return Task.FromResult<string?>(readAllText);
        }
        finally
        {
            File.Delete(tempFileName);
        }
    }
}
#endif