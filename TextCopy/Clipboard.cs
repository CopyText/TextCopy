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
            IsGetSupported = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            IsSetSupported = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
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

            return s => throw new NotSupportedException();
        }

        static Func<string> CreateGet()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return WindowsClipboard.GetText;
            }


            return () => throw new NotSupportedException();
        }

        public static readonly bool IsSetSupported;

        public static readonly bool IsGetSupported;
    }
}