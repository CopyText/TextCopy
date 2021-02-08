#if !UAP
using Microsoft.Extensions.DependencyInjection;
#if NET5_0
using System;
using System.Runtime.InteropServices;
#endif

namespace TextCopy
{
    /// <summary>
    /// Extensions to <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds a singleton instance of <see cref="MockClipboard"/> as an
        /// <see cref="IClipboard"/> to <paramref name="services"/>.
        /// </summary>
        public static void InjectMockClipboard(this IServiceCollection services)
        {
            Guard.AgainstNull(services, nameof(services));
            services.AddSingleton<IClipboard>(_ => new MockClipboard());
        }

        /// <summary>
        /// Adds a singleton instance of <see cref="Clipboard"/> as an
        /// <see cref="IClipboard"/> to <paramref name="services"/>.
        /// </summary>
        public static void InjectClipboard(this IServiceCollection services)
        {
            Guard.AgainstNull(services, nameof(services));
            services.AddSingleton<IClipboard>(provider =>
            {
#if NET5_0
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("Browser")))
                {
                    var jsRuntimeType = Type.GetType("Microsoft.JSInterop.IJSRuntime, Microsoft.JSInterop", false);
                    if (jsRuntimeType == null)
                    {
                        throw new("Running in Blazor but could not resolve JSInterop type.");
                    }
                    var jsRuntime = provider.GetService(jsRuntimeType);
                    if (jsRuntime == null)
                    {
                        throw new("Running in Blazor but could not get the JSInterop instance from the IServiceProvider.");
                    }
                    return new BlazorClipboard(jsRuntime);
                }
#endif

                return new Clipboard();
            });
        }
    }
}
#endif