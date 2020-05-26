using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorSample;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TextCopy;

public class Program
{
    public static Task Main()
    {
        var builder = WebAssemblyHostBuilder.CreateDefault();
        var serviceCollection = builder.Services;
        serviceCollection.AddSingleton<IClipboard, Clipboard>((IServiceProvider provider) =>
        {
            var jsRuntimeType = Type.GetType("Microsoft.JSInterop.IJSRuntime",false);
            if (jsRuntimeType != null)
            {
                var jsRuntime = provider.GetService(jsRuntimeType);
                Console.WriteLine(jsRuntime == null);
            }

            return new Clipboard();
        });
        builder.RootComponents.Add<App>("app");

        serviceCollection.AddTransient(
            provider => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

        return builder.Build().RunAsync();
    }
}