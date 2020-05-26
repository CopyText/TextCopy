#if !UAP
using System;
using Microsoft.Extensions.DependencyInjection;

namespace TextCopy
{
    /// <summary>
    /// Provides methods to place text on and retrieve text from the system Clipboard.
    /// </summary>
    public static partial class ClipboardService
    {
        /// <summary>
        /// Retrieves text data from the Clipboard.
        /// </summary>
        public static void InjectClipboard(this IServiceCollection services)
        {
            services.AddSingleton<IClipboard>(provider =>
            {
#if NETSTANDARD2_1
                var jsRuntimeType = Type.GetType("Microsoft.JSInterop.IJSRuntime",false);
                if (jsRuntimeType != null)
                {
                    var jsRuntime = provider.GetService(jsRuntimeType);
                    if (jsRuntime != null)
                    {
                        return new BlazorClipboard(jsRuntime);
                    }
                }
#endif

                return new Clipboard();
            });
        }
    }
}
#endif