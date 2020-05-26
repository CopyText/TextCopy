#if NETSTANDARD2_1
using System.Threading;
using System.Threading.Tasks;

namespace TextCopy
{
    /// <summary>
    /// Provides methods to place text on and retrieve text from the system Clipboard.
    /// </summary>
    public class BlazorClipboard :
        IClipboard
    {
        object jsRuntime;

        public BlazorClipboard(object jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Retrieves text data from the Clipboard.
        /// </summary>
        public virtual Task<string?> GetTextAsync(CancellationToken cancellation = default)
        {
            return ClipboardService.GetTextAsync(cancellation);
        }

        /// <summary>
        /// Retrieves text data from the Clipboard.
        /// </summary>
        public virtual string? GetText()
        {
            return ClipboardService.GetText();
        }

        /// <summary>
        /// Clears the Clipboard and then adds text data to it.
        /// </summary>
        public virtual Task SetTextAsync(string text, CancellationToken cancellation = default)
        {
            return ClipboardService.SetTextAsync(text, cancellation);
        }

        /// <summary>
        /// Clears the Clipboard and then adds text data to it.
        /// </summary>
        public virtual void SetText(string text)
        {
            ClipboardService.SetText(text);
        }
    }
}
#endif