#if NETSTANDARD2_1
using System;
using System.Reflection;
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
        static Type[] types =
        {
            typeof(string),
            typeof(CancellationToken),
            typeof(object[])
        };
        object jsRuntime;
        MethodInfo invokeAsync;

        public BlazorClipboard(object jsRuntime)
        {
            this.jsRuntime = jsRuntime;
            invokeAsync = jsRuntime.GetType()
                .GetMethod(
                    "InvokeAsync",
                    types: types);
            invokeAsync = invokeAsync.MakeGenericMethod(typeof(string));
        }

        /// <summary>
        /// Retrieves text data from the Clipboard.
        /// </summary>
        public virtual async Task<string?> GetTextAsync(CancellationToken cancellation = default)
        {
            var parameters = new object[] {"navigator.clipboard.readText", cancellation, Array.Empty<object>()};
            return await (ValueTask<string>) invokeAsync.Invoke(jsRuntime, parameters);
        }

        /// <summary>
        /// Retrieves text data from the Clipboard.
        /// </summary>
        public virtual string? GetText()
        {
            return GetTextAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Clears the Clipboard and then adds text data to it.
        /// </summary>
        public virtual async Task SetTextAsync(string text, CancellationToken cancellation = default)
        {
            var parameters = new object[] {"navigator.clipboard.writeText", cancellation, new object[] {text}};
            await (ValueTask<string>) invokeAsync.Invoke(jsRuntime, parameters);
        }

        /// <summary>
        /// Clears the Clipboard and then adds text data to it.
        /// </summary>
        public virtual void SetText(string text)
        {
            SetTextAsync(text).GetAwaiter().GetResult();
        }
    }
}
#endif