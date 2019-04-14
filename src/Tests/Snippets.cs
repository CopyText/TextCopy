// ReSharper disable UnusedVariable
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
}