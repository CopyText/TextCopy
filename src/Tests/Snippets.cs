using System.Threading.Tasks;
// ReSharper disable UnusedVariable

class Snippets
{
    void SetText()
    {
        #region SetText

        TextCopy.ClipboardService.SetText("Text to place in clipboard");

        #endregion
    }

    void GetText()
    {
        #region GetText

        var text = TextCopy.ClipboardService.GetText();

        #endregion
    }

    async Task SetTextAsync()
    {
        #region SetTextAsync

        await TextCopy.ClipboardService.SetTextAsync("Text to place in clipboard");

        #endregion
    }

    async Task GetTextAsync()
    {
        #region GetTextAsync

        var text = await TextCopy.ClipboardService.GetTextAsync();

        #endregion
    }
}