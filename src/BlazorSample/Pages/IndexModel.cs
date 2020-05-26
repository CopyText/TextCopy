using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PropertyChanged;
using TextCopy;

namespace BlazorSample
{
    [AddINotifyPropertyChangedInterface]
    public partial class IndexModel :
        ComponentBase
    {
        #region Inject
        [Inject]
        public IClipboard Clipboard { get; set; }
        #endregion

        public string Content { get; set; }

        public async Task CopyTextToClipboard()
        {
            await Clipboard.SetTextAsync(Content);
        }

        public async Task ReadTextFromClipboard()
        {
            Content = await Clipboard.GetTextAsync();
        }
    }
}