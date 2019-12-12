using System.Threading.Tasks;

class Snippets
{
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
