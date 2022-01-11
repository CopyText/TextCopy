#if (NETSTANDARD || NETFRAMEWORK || NET5_0_OR_GREATER)
using System.Runtime.InteropServices;

namespace TextCopy;

public static partial class ClipboardService
{
    static Func<CancellationToken, Task<string?>> CreateAsyncGet()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return WindowsClipboard.GetTextAsync;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return OsxClipboard.GetTextAsync;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return LinuxClipboard.GetTextAsync;
        }

        throw new NotSupportedException();
    }

    static Func<string?> CreateGet()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return WindowsClipboard.GetText;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return OsxClipboard.GetText;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return LinuxClipboard.GetText;
        }

        throw new NotSupportedException();
    }
}
#endif