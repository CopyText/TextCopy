using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PropertyChanged;
using TextCopy;

namespace BlazorSample
{
    [AddINotifyPropertyChangedInterface]
    public partial class IndexModel :
        ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public IClipboard Clipboard { get; set; }

        public string Content { get; set; }

        public async Task CopyTextToClipboard()
        {
            var runtimeType = JSRuntime.GetType();
            var methodInfo = runtimeType.GetMethod("InvokeAsync", new[] {typeof(string), typeof(CancellationToken), typeof(object[])});
            var makeGenericMethod = methodInfo.MakeGenericMethod(typeof(string));
            Console.WriteLine("makeGenericMethod " + makeGenericMethod != null);
            var parameters = new object[] {"navigator.clipboard.writeText", CancellationToken.None, new object[] {Content}};
            await (ValueTask<string>) makeGenericMethod.Invoke(JSRuntime, parameters);
        }

        public async Task ReadTextFromClipboard()
        {
            //Content = await JSRuntime.InvokeAsync<string>("navigator.clipboard.readText", null);


            var runtimeType = JSRuntime.GetType();
            var methodInfo = runtimeType.GetMethod("InvokeAsync", new[] {typeof(string), typeof(CancellationToken), typeof(object[])});
            var makeGenericMethod = methodInfo.MakeGenericMethod(typeof(string));
            Console.WriteLine("makeGenericMethod " + makeGenericMethod != null);
            var parameters = new object[] {"navigator.clipboard.readText", CancellationToken.None, new object[] {null}};
            Content =   await (ValueTask<string>) makeGenericMethod.Invoke(JSRuntime, parameters);
        }
    }
}