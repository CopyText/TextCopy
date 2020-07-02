using System.Threading.Tasks;
using TextCopy;

// ReSharper disable UnusedVariable

class Snippets
{
    void SetText()
    {
        #region SetText

        ClipboardService.SetText("Text to place in clipboard");

        #endregion

        #region SetTextInstance

        var clipboard = new Clipboard();
        clipboard.SetText("Text to place in clipboard");

        #endregion
    }

    void GetText()
    {
        #region GetText

        var text = ClipboardService.GetText();

        #endregion
    }

    async Task SetTextAsync()
    {
        #region SetTextAsync

        await ClipboardService.SetTextAsync("Text to place in clipboard");

        #endregion
    }

    async Task GetTextAsync()
    {
        #region GetTextAsync

        var text = await ClipboardService.GetTextAsync();

        #endregion
    }
    
    void ClearClipboard()
    {
        #region ClearClipboard

        // Get the text
        var text = ClipboardService.GetText();

        // Do something here with the text
        
        // Clear the Clipboard
        ClipboardService.SetText("");
        #endregion
    }
    
    async Task ClearClipboardAsync()
    {
        #region ClearClipboardAsync

        // Get the text
        var text = await ClipboardService.GetTextAsync();

        // Do something here with the text
        
        // Clear the Clipboard
        await ClipboardService.SetTextAsync("");
        #endregion
    }
}
