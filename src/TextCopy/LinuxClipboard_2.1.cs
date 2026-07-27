#if (NETSTANDARD2_1 || NET5_0_OR_GREATER)

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
        await File.WriteAllTextAsync(tempFileName, text, cancellation);
        await InnerSetTextAsync(tempFileName, cancellation);
    }

    public static void SetText(string text)
    {
        var tempFileName = Path.GetTempFileName();
        File.WriteAllText(tempFileName, text);
        InnerSetText(tempFileName);
    }

    static void InnerSetText(string tempFileName)
    {
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

    static async Task InnerSetTextAsync(string tempFileName, Cancellation cancellation)
    {
        try
        {
            if (cancellation.IsCancellationRequested)
            {
                return;
            }

            if (isWsl)
            {
                await BashRunner.RunAsync($"cat {tempFileName} | clip.exe ", cancellation);
            }
            else
            {
                await BashRunner.RunAsync($"cat {tempFileName} | xsel -i --clipboard ", cancellation);
            }
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
            InnerGetText(tempFileName);
            return File.ReadAllText(tempFileName);
        }
        finally
        {
            File.Delete(tempFileName);
        }
    }

    public static async Task<string?> GetTextAsync(Cancellation cancellation)
    {
        var tempFileName = Path.GetTempFileName();
        try
        {
            await InnerGetTextAsync(tempFileName, cancellation);
            return await File.ReadAllTextAsync(tempFileName, cancellation);
        }
        finally
        {
            File.Delete(tempFileName);
        }
    }

    static void InnerGetText(string tempFileName)
    {
        if (isWsl)
        {
            BashRunner.Run($"powershell.exe -NoProfile Get-Clipboard  > {tempFileName}");
        }
        else
        {
            BashRunner.Run($"xsel -o --clipboard  > {tempFileName}");
        }
    }

    static async Task InnerGetTextAsync(string tempFileName, Cancellation cancellation)
    {
        if (isWsl)
        {
            await BashRunner.RunAsync($"powershell.exe -NoProfile Get-Clipboard  > {tempFileName}", cancellation);
        }
        else
        {
            await BashRunner.RunAsync($"xsel -o --clipboard  > {tempFileName}", cancellation);
        }
    }
}
#endif
