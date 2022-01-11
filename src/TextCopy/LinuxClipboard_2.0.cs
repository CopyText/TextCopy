#if (NETSTANDARD2_0 || NETFRAMEWORK)

static class LinuxClipboard
{
    static bool isWsl;

    static LinuxClipboard()
    {
        isWsl = Environment.GetEnvironmentVariable("WSL_DISTRO_NAME") != null;
    }

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
            if (isWsl)
            {
                BashRunner.Run($"cat {tempFileName} | clip.exe ");
            }
            else
            {
                BashRunner.Run($"cat {tempFileName} | xsel -i --clipboard ");
            }
        }
        finally
        {
            File.Delete(tempFileName);
        }
    }

    public static Task<string?> GetTextAsync(CancellationToken cancellation)
    {
        return Task.FromResult<string?>(GetText());
    }

    public static string GetText()
    {
        var tempFileName = Path.GetTempFileName();
        try
        {
            if (isWsl)
            {
                BashRunner.Run($"powershell.exe Get-Clipboard  > {tempFileName}");
            }
            else
            {
                BashRunner.Run($"xsel -o --clipboard  > {tempFileName}");
            }
            return File.ReadAllText(tempFileName);
        }
        finally
        {
            File.Delete(tempFileName);
        }
    }
}
#endif