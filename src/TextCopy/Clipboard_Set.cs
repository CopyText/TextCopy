using System;

#if (NETSTANDARD)
using System.Runtime.InteropServices;
#endif

namespace TextCopy
{
    public static partial class Clipboard
    {
        static Action<string> setAction = CreateSet();

        /// <summary>
        /// Clears the Clipboard and then adds text data to it.
        /// </summary>
        public static void SetText(string text)
        {
            Guard.AgainstNull(text, nameof(text));
            setAction(text);
        }

#if (NETSTANDARD)
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

            return s => throw new NotSupportedException();
        }
#else
        static Action<string> CreateSet()
        {
            return WindowsClipboard.SetText;
        }
#endif
    }
}