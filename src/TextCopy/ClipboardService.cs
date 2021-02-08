using System;
using System.Threading;
using System.Threading.Tasks;
#if NET5_0
using System.Runtime.InteropServices;
#endif

namespace TextCopy
{
    /// <summary>
    /// Provides methods to place text on and retrieve text from the system Clipboard.
    /// </summary>
    public static partial class ClipboardService
    {
        static Func<CancellationToken, Task<string?>> getAsyncFunc;
        static Func<string?> getFunc;

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

        static Func<string, CancellationToken, Task> setAsyncAction;
        static Action<string> setAction;

        static ClipboardService()
        {
#if NET5_0
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("Browser")))
            {
                throw new($"The static class ClipboardService is not supported on Blazor. Instead inject an {nameof(IClipboard)} using {nameof(ServiceExtensions)}{nameof(ServiceExtensions.InjectClipboard)}.");
            }
#endif
            getAsyncFunc = CreateAsyncGet();
            getFunc = CreateGet();
            setAsyncAction = CreateAsyncSet();
            setAction = CreateSet();
        }

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