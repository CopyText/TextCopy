namespace TextCopy;

/// <inheritdoc />
public class MockClipboard : IClipboard
{
    /// <inheritdoc />
    public virtual Task<string?> GetTextAsync(CancellationToken cancellation = default)
    {
        return Task.FromResult<string?>(null);
    }

    /// <inheritdoc />
    public virtual string? GetText()
    {
        return null;
    }

    /// <inheritdoc />
    public virtual Task SetTextAsync(string text, CancellationToken cancellation = default)
    {
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public void SetText(string text)
    {
    }
}