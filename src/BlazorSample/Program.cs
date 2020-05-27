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
        #region BlazorStartup
        var builder = WebAssemblyHostBuilder.CreateDefault();
        var serviceCollection = builder.Services;
        #region InjectClipboard
        serviceCollection.InjectClipboard();
        #endregion
        builder.RootComponents.Add<App>("app");
        #endregion

        serviceCollection.AddTransient(
            provider => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

        return builder.Build().RunAsync();
    }
}