using System;
using System.Runtime.InteropServices;

namespace TextCopy
{
    public static class Clipboard
    {
        static Action<string> setAction = CreateSet();
        static Func<string> getFunc = CreateGet();

        static Clipboard()
        {
            IsGetSupported = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ||
                             RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
            IsSetSupported = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ||
                             RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
                             RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }

        public static void SetText(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            setAction(text);
        }

        public static string GetText()
        {
           return getFunc();
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

            return s => throw new NotSupportedException();
        }

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

        public static readonly bool IsSetSupported;

        public static readonly bool IsGetSupported;
    }
}