#if (NETSTANDARD2_0 || NETFRAMEWORK)
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

        // XDG_SESSION_TYPE is a systemd(1) environment variable and is unlikely set in non-systemd environments.
        // Therefore we check the Wayland specific display environment variable first and fall back to x11.
        windowSystem = Environment.GetEnvironmentVariable("WAYLAND_DISPLAY") != null ? WindowSystem.Wayland : WindowSystem.X11;
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
            return File.ReadAllText(tempFileName);
        }
        finally
        {
            File.Delete(tempFileName);
        }
    }
}
#endif