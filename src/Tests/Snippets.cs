using System.Threading.Tasks;

class Snippets
{
    async Task SetText()
    {
        #region SetText

        await TextCopy.Clipboard.SetText("Text to place in clipboard");

        #endregion
    }

    async Task GetText()
    {
        #region GetText

        var text = await TextCopy.Clipboard.GetText();

        #endregion
    }
}