#if (NETSTANDARD2_1)
using System.IO;
using System.Threading.Tasks;

static class LinuxClipboard
{
    public static async Task SetText(string text)
    {
        var tempFileName = Path.GetTempFileName();
        await File.WriteAllTextAsync(tempFileName, text);
        try
        {
            BashRunner.Run($"cat {tempFileName} | xclip -i -selection clipboard");
        }
        finally
        {
            File.Delete(tempFileName);
        }
    }

    public static async Task<string?> GetText()
    {
        var tempFileName = Path.GetTempFileName();
        try
        {
            BashRunner.Run($"xclip -o -selection clipboard > {tempFileName}");
            return await File.ReadAllTextAsync(tempFileName);
        }
        finally
        {
            File.Delete(tempFileName);
        }
    }
}
#endif