using System;
using System.Threading.Tasks;

namespace TextCopy
{
    /// <summary>
    /// Provides methods to place text on and retrieve text from the system Clipboard.
    /// </summary>
    public static partial class Clipboard
    {
        static Func<Task<string?>> getFunc = CreateGet();

        /// <summary>
        /// Retrieves text data from the Clipboard.
        /// </summary>
        public static Task<string?> GetText()
        {
            return getFunc();
        }

        static Func<string,Task> setAction = CreateSet();

        /// <summary>
        /// Clears the Clipboard and then adds text data to it.
        /// </summary>
        public static Task SetText(string text)
        {
            Guard.AgainstNull(text, nameof(text));
            return setAction(text);
        }
    }
}