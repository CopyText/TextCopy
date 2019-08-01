using System;

namespace TextCopy
{
    public static class Clipboard
    {
        static Action<string> setAction = CreateSet();
        static Func<string> getFunc = CreateGet();

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
            return WindowsClipboard.SetText;
        }

        static Func<string> CreateGet()
        {
            return WindowsClipboard.GetText;
        }
    }
}