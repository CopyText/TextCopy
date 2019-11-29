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
        static Func<CancellationToken,Task<string?>> getFunc = CreateGet();

        /// <summary>
        /// Retrieves text data from the Clipboard.
        /// </summary>
        public static Task<string?> GetText(CancellationToken cancellation = default)
        {
            return getFunc(cancellation);
        }

        static Func<string,CancellationToken,Task> setAction = CreateSet();

        /// <summary>
        /// Clears the Clipboard and then adds text data to it.
        /// </summary>
        public static Task SetText(string text, CancellationToken cancellation = default)
        {
            Guard.AgainstNull(text, nameof(text));
            return setAction(text,cancellation);
        }
    }
}