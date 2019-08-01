using System.IO;

static class LinuxClipboard
{
    public static void SetText(string text)
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
    }

    public static string GetText()
    {
        var tempFileName = Path.GetTempFileName();
        try
        {
            BashRunner.Run($"xclip -o -selection clipboard > {tempFileName}");
            return File.ReadAllText(tempFileName);
        }
        finally
        {
            File.Delete(tempFileName);
        }
    }
}