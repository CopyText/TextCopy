namespace TextCopy;

/// <inheritdoc />
public class Clipboard :
    IClipboard
{
    /// <inheritdoc />
    public virtual Task<string?> GetTextAsync(CancellationToken cancellation = default)
    {
        return ClipboardService.GetTextAsync(cancellation);
    }

    /// <inheritdoc />
    public virtual string? GetText()
    {
        return ClipboardService.GetText();
    }

    /// <inheritdoc />
    public virtual Task SetTextAsync(string text, CancellationToken cancellation = default)
    {
        return ClipboardService.SetTextAsync(text, cancellation);
    }

    /// <inheritdoc />
    public virtual void SetText(string text)
    {
        ClipboardService.SetText(text);
    }
}