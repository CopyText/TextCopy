#if (NETSTANDARD2_1 || NET5_0)
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

static class LinuxClipboard
{
    enum WindowSystem
    {
        X11,
        Wayland
    }

    static bool isWsl;
    static WindowSystem windowSystem;

    static LinuxClipboard()
    {
        isWsl = Environment.GetEnvironmentVariable("WSL_DISTRO_NAME") != null;

        windowSystem = Environment.GetEnvironmentVariable("XDG_SESSION_TYPE") switch
        {
            "x11" => WindowSystem.X11,
            "wayland" => WindowSystem.Wayland,
            _ => throw new NotSupportedException()
        };
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
                switch (windowSystem)
                {
                    case WindowSystem.X11:
                        BashRunner.Run($"cat {tempFileName} | xsel -i --clipboard ");
                        break;
                    case WindowSystem.Wayland:
                        // wl-copy keeps stderr open. Since there is no straightforward way for us to wait for errors, just ignore them.
                        BashRunner.Run($"cat {tempFileName} | wl-copy -n 2>/dev/null ");
                        break;
                }
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
            BashRunner.Run($"powershell.exe Get-Clipboard > {tempFileName}");
        }
        else
        {
            switch (windowSystem)
            {
                case WindowSystem.X11:
                    BashRunner.Run($"xsel -o --clipboard > {tempFileName}");
                    break;
                case WindowSystem.Wayland:
                    BashRunner.Run($"wl-paste -n > {tempFileName}");
                    break;
            }
        }
    }
}
#endif