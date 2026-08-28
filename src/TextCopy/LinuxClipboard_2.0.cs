#if (NETSTANDARD2_0 || NETFRAMEWORK)

static class LinuxClipboard
{
    static bool isWsl;

    static LinuxClipboard()
    {
        isWsl = Environment.GetEnvironmentVariable("WSL_DISTRO_NAME") != null;
    }

    public static async Task SetTextAsync(string text, Cancellation cancellation)
    {
        var tempFileName = Path.GetTempFileName();
        File.WriteAllText(tempFileName, text);
        try
        {
            await BashRunner.RunAsync(SetCommand(tempFileName), cancellation);
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
            BashRunner.Run(SetCommand(tempFileName));
        }
        finally
        {
            File.Delete(tempFileName);
        }
    }

    static string SetCommand(string tempFileName) =>
        isWsl
            ? $"cat {tempFileName} | clip.exe "
            : $"cat {tempFileName} | xsel -i --clipboard ";

    public static async Task<string?> GetTextAsync(Cancellation cancellation)
    {
        var tempFileName = Path.GetTempFileName();
        try
        {
            await BashRunner.RunAsync(GetCommand(tempFileName), cancellation);
            return File.ReadAllText(tempFileName);
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
            BashRunner.Run(GetCommand(tempFileName));
            return File.ReadAllText(tempFileName);
        }
        finally
        {
            File.Delete(tempFileName);
        }
    }

    static string GetCommand(string tempFileName) =>
        isWsl
            ? $"powershell.exe -NoProfile Get-Clipboard  > {tempFileName}"
            : $"xsel -o --clipboard  > {tempFileName}";
}
#endif
