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
            services.AddSingleton<IClipboard, Clipboard>(provider =>
            {
                var jsRuntimeType = Type.GetType("Microsoft.JSInterop.IJSRuntime",false);
                if (jsRuntimeType != null)
                {
                    var jsRuntime = provider.GetService(jsRuntimeType);
                    Console.WriteLine(jsRuntime == null);
                }

                return new Clipboard();
            });
        }
    }
}
#endif