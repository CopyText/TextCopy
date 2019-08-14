using System;

#if (NETSTANDARD)
using System.Runtime.InteropServices;
#endif

namespace TextCopy
{
    /// <summary>
    /// Provides methods to place text on and retrieve text from the system Clipboard.
    /// </summary>
    public static class Clipboard
    {
        static Action<string> setAction = CreateSet();
        static Func<string> getFunc = CreateGet();

        /// <summary>
        /// Clears the Clipboard and then adds text data to it.
        /// </summary>
        public static void SetText(string text)
        {
            Guard.AgainstNull(text, nameof(text));
            setAction(text);
        }

        /// <summary>
        /// Retrieves text data from the Clipboard.
        /// </summary>
        public static string GetText()
        {
           return getFunc();
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

#if (NETSTANDARD)
        static Func<string> CreateGet()
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
        static Func<string> CreateGet()
        {
             return WindowsClipboard.GetText;
        }
#endif
    }
}