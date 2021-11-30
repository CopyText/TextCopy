using BlazorSample;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TextCopy;

#region BlazorStartup
var builder = WebAssemblyHostBuilder.CreateDefault();
var serviceCollection = builder.Services;
#region InjectClipboard
serviceCollection.InjectClipboard();
#endregion
builder.RootComponents.Add<App>("app");
#endregion
builder.RootComponents.Add<HeadOutlet>("head::after");
await builder.Build().RunAsync();