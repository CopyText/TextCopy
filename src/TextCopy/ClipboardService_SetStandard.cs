#if (NETSTANDARD || NETFRAMEWORK || NET5_0_OR_GREATER)
using System.Runtime.InteropServices;

namespace TextCopy;

public static partial class ClipboardService
{
    static Func<string, CancellationToken, Task> CreateAsyncSet()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return WindowsClipboard.SetTextAsync;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return OsxClipboard.SetTextAsync;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return LinuxClipboard.SetTextAsync;
        }

        return (_, _) => throw new NotSupportedException();
    }

    static Action<string> CreateSet()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return WindowsClipboard.SetText;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return OsxClipboard.SetText;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return LinuxClipboard.SetText;
        }

        return _ => throw new NotSupportedException();
    }
}
#endif