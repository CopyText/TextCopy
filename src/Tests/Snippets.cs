using System.Threading.Tasks;

class Snippets
{
    void SetText()
    {
        #region SetText

        TextCopy.Clipboard.SetText("Text to place in clipboard");

        #endregion
    }

    void GetText()
    {
        #region GetText

        var text = TextCopy.Clipboard.GetText();

        #endregion
    }

    async Task SetTextAsync()
    {
        #region SetTextAsync

        await TextCopy.Clipboard.SetTextAsync("Text to place in clipboard");

        #endregion
    }

    async Task GetTextAsync()
    {
        #region GetTextAsync

        var text = await TextCopy.Clipboard.GetTextAsync();

        #endregion
    }
}