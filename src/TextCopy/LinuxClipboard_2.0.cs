#if (NETSTANDARD2_0 || NETFRAMEWORK)
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
                BashRunner.Run($"cat {tempFileName} | {GetXsel()} -i --clipboard ");
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
                BashRunner.Run($"{GetXsel()} -o --clipboard  > {tempFileName}");
            }
            return File.ReadAllText(tempFileName);
        }
        finally
        {
            File.Delete(tempFileName);
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