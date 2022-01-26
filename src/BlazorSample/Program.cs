using BlazorSample;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using TextCopy;

#region BlazorStartup
var builder = WebAssemblyHostBuilder.CreateDefault();
var serviceCollection = builder.Services;
#region InjectClipboardBlazor
serviceCollection.AddSingleton<IClipboard>(
    _ => new BlazorClipboard(_.GetRequiredService<IJSRuntime>()));
#endregion
builder.RootComponents.Add<App>("app");
#endregion
builder.RootComponents.Add<HeadOutlet>("head::after");
await builder.Build().RunAsync();