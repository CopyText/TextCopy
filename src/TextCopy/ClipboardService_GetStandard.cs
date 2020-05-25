#if (NETSTANDARD || NETFRAMEWORK)
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace TextCopy
{
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
}
#endif