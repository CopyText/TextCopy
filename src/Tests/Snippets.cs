﻿using TextCopy;

// ReSharper disable UnusedVariable

class Snippets
{
    void SetText()
    {
        #region SetText

        ClipboardService.SetText("Text to place in clipboard");

        #endregion

        #region SetTextInstance

        Clipboard clipboard = new();
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
        ClipboardService.SetText("");
        #endregion
    }

    async Task ClearClipboardAsync()
    {
        #region ClearClipboardAsync
        await ClipboardService.SetTextAsync("");
        #endregion
    }
}
