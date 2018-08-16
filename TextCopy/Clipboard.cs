using System;
using System.Runtime.InteropServices;

namespace TextCopy
{
    public static class Clipboard
    {
        static Action<string> action = CreateClipboard();

        public static void SetText(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            action(text);
        }

        static Action<string> CreateClipboard()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return WindowsClipboard.SetText;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return OsxClipboard.SetText;
            }

            return s => throw new NotSupportedException();
        }
    }
}