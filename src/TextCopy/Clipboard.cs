using System;
using System.Threading;
using System.Threading.Tasks;

namespace TextCopy
{
    /// <summary>
    /// Provides methods to place text on and retrieve text from the system Clipboard.
    /// </summary>
    public static partial class Clipboard
    {
        static Func<CancellationToken, Task<string?>> getAsyncFunc = CreateAsyncGet();
        static Func<string?> getFunc = CreateGet();

        /// <summary>
        /// Retrieves text data from the Clipboard.
        /// </summary>
        public static Task<string?> GetTextAsync(CancellationToken cancellation = default)
        {
            return getAsyncFunc(cancellation);
        }

        /// <summary>
        /// Retrieves text data from the Clipboard.
        /// </summary>
        public static string? GetText()
        {
            return getFunc();
        }

        static Func<string, CancellationToken, Task> setAsyncAction = CreateAsyncSet();
        static Action<string> setAction = CreateSet();

        /// <summary>
        /// Clears the Clipboard and then adds text data to it.
        /// </summary>
        public static Task SetTextAsync(string text, CancellationToken cancellation = default)
        {
            Guard.AgainstNull(text, nameof(text));
            return setAsyncAction(text, cancellation);
        }

        /// <summary>
        /// Clears the Clipboard and then adds text data to it.
        /// </summary>
        public static void SetText(string text)
        {
            Guard.AgainstNull(text, nameof(text));
            setAction(text);
        }
    }
}