using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PropertyChanged;
using TextCopy;

namespace BlazorSample;

[AddINotifyPropertyChangedInterface]
#region Inject
public partial class IndexModel :
    ComponentBase
{
    [Inject]
    public IClipboard Clipboard { get; set; }

    public string Content { get; set; }

    public Task CopyTextToClipboard() =>
        Clipboard.SetTextAsync(Content);

    public async Task ReadTextFromClipboard() =>
        Content = await Clipboard.GetTextAsync();
}
#endregion