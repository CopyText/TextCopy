using System;

#if (NETSTANDARD)
using System.Runtime.InteropServices;
#endif

namespace TextCopy
{
    public static partial class Clipboard
    {
        static Func<string?> getFunc = CreateGet();

        /// <summary>
        /// Retrieves text data from the Clipboard.
        /// </summary>
        public static string? GetText()
        {
           return getFunc();
        }

#if (NETSTANDARD)
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

            return () => throw new NotSupportedException();
        }
#else
        static Func<string?> CreateGet()
        {
             return WindowsClipboard.GetText;
        }
#endif
    }
}