using System.Threading;
using System.Threading.Tasks;

namespace TextCopy
{
    /// <summary>
    /// Provides methods to place text on and retrieve text from the system Clipboard.
    /// </summary>
    public interface IClipboard
    {
        /// <summary>
        /// Retrieves text data from the Clipboard.
        /// </summary>
        public Task<string?> GetTextAsync(CancellationToken cancellation = default);

        /// <summary>
        /// Retrieves text data from the Clipboard.
        /// </summary>
        public string? GetText();

        /// <summary>
        /// Clears the Clipboard and then adds text data to it.
        /// </summary>
        public Task SetTextAsync(string text, CancellationToken cancellation = default);

        /// <summary>
        /// Clears the Clipboard and then adds text data to it.
        /// </summary>
        public void SetText(string text);
    }
}