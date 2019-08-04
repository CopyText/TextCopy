using System;

namespace TextCopy
{
    public static class Clipboard
    {
        public static void SetText(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            WindowsClipboard.SetText(text);
        }

        public static string GetText()
        {
            return WindowsClipboard.GetText();
        }
    }
}