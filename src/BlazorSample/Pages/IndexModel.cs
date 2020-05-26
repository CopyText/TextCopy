using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PropertyChanged;

namespace BlazorSample
{
    [AddINotifyPropertyChangedInterface]
    public partial class IndexModel :
        ComponentBase
    {
        [Inject] public IJSRuntime JSRuntime { get; set; }

        public string Content { get; set; }

        public async Task CopyTextToClipboard()
        {
            await JSRuntime.InvokeVoidAsync("navigator.clipboard.writeText", Content);
        }

        public async Task ReadTextFromClipboard()
        {
            Content = await JSRuntime.InvokeAsync<string>("navigator.clipboard.readText");
        }
    }
}