using System;

namespace TextCopy
{
    /// <summary>
    /// Provides methods to place text on and retrieve text from the system Clipboard.
    /// </summary>
    public static class Clipboard
    {
        /// <summary>
        /// Clears the Clipboard and then adds text data to it.
        /// </summary>
        public static void SetText(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            WindowsClipboard.SetText(text);
        }

        /// <summary>
        /// Retrieves text data from the Clipboard.
        /// </summary>
        public static string GetText()
        {
            return WindowsClipboard.GetText();
        }
    }
}