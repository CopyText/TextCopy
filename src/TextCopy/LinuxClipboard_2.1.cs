#if (NETSTANDARD2_1 || NET5_0)
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

static class LinuxClipboard
{
    /// <summary>
    /// Determine whether we should use a local path for xsel binary or not.
    /// When start up, we will check if xsel exists in the 
    /// <see cref="AppContext.BaseDirectory"/> or not.
    /// If it exists, we will use a local xsel for it, otherwise this property
    /// will be <c>false</c>.
    /// You can also set it manually, but if it's <c>false</c>, xsel binary
    /// should exists in the user's path.
    /// </summary>
    /// <value>
    /// <c>true</c> if we have to use local path, otherwise <c>false</c>.
    /// </value>
    public static bool UseLocal { get; set; }
    static bool isWsl;

    static LinuxClipboard()
    {
        isWsl = Environment.GetEnvironmentVariable("WSL_DISTRO_NAME") != null;
        UseLocal = File.Exists(AppContext.BaseDirectory + "xsel");
    }

    public static async Task SetTextAsync(string text, CancellationToken cancellation)
    {
        var tempFileName = Path.GetTempFileName();
        await File.WriteAllTextAsync(tempFileName, text, cancellation);

        if (cancellation.IsCancellationRequested)
        {
            return;
        }

        InnerSetText(tempFileName);
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
                BashRunner.Run($"cat {tempFileName} | {GetXsel()} -i --clipboard ");
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

    public static async Task<string?> GetTextAsync(CancellationToken cancellation)
    {
        var tempFileName = Path.GetTempFileName();
        try
        {
            InnerGetText(tempFileName);
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
            BashRunner.Run($"powershell.exe Get-Clipboard  > {tempFileName}");
        }
        else
        {
            BashRunner.Run($"{GetXsel()} -o --clipboard  > {tempFileName}");
        }
    }
    private static string GetXsel()
    {
        if (UseLocal)
        {
            return "./" + "xsel";
        }
        return "xsel";
    }
}
#endif