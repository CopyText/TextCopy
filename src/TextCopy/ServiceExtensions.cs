#if !UAP
using Microsoft.Extensions.DependencyInjection;

namespace TextCopy
{
    /// <summary>
    /// Extensions to <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Retrieves text data from the Clipboard.
        /// </summary>
        public static void InjectClipboard(this IServiceCollection services)
        {
            services.AddSingleton<IClipboard>(provider =>
            {

#if NETSTANDARD2_1

                var jsRuntimeType = System.Type.GetType("Microsoft.JSInterop.IJSRuntime, Microsoft.JSInterop", false);
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