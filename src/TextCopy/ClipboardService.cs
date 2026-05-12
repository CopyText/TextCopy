#if NET6_0_OR_GREATER
using System.Runtime.InteropServices;
#endif

namespace TextCopy;

/// <summary>
/// Provides methods to place text on and retrieve text from the system Clipboard.
/// </summary>
public static partial class ClipboardService
{
    static Func<Cancellation, Task<string?>> getAsyncFunc;
    static Func<string?> getFunc;

    /// <summary>
    /// Retrieves text data from the Clipboard.
    /// </summary>
    public static Task<string?> GetTextAsync(Cancellation cancellation = default)
    {
        return getAsyncFunc(cancellation);
    }

    /// <summary>
    /// Retrieves text data from the Clipboard.
    /// </summary>
    public static string? GetText()
    {
        return getFunc();
    }

    /// <summary>
    /// Retrieves text data from the Clipboard and splits it into lines.
    /// </summary>
    public static async Task<string[]?> GetLinesAsync(Cancellation cancellation = default)
    {
        var text = await GetTextAsync(cancellation);
        if (text == null)
            return null;
        return text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
    }

    /// <summary>
    /// Retrieves text data from the Clipboard and splits it into lines.
    /// </summary>
    public static string[]? GetLines()
    {
        var text = GetText();
        if (text == null)
            return null;
        return text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
    }

    static Func<string, Cancellation, Task> setAsyncAction;
    static Action<string> setAction;

    static ClipboardService()
    {
#if NET6_0
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("Browser")))
        {
            throw new($"The static class ClipboardService is not supported on Blazor. Instead inject an {nameof(IClipboard)} using {nameof(ServiceExtensions)}{nameof(ServiceExtensions.InjectClipboard)}.");
        }
#endif
        getAsyncFunc = CreateAsyncGet();
        getFunc = CreateGet();
        setAsyncAction = CreateAsyncSet();
        setAction = CreateSet();
    }

    /// <summary>
    /// Clears the Clipboard and then adds text data to it.
    /// </summary>
    public static Task SetTextAsync(string text, Cancellation cancellation = default)
    {
        return setAsyncAction(text, cancellation);
    }

    /// <summary>
    /// Clears the Clipboard and then adds text data to it.
    /// </summary>
    public static void SetText(string text)
    {
        setAction(text);
    }

    /// <summary>
    /// Clears the Clipboard and then adds lines of text data to it.
    /// </summary>
    public static Task SetLinesAsync(string[] lines, Cancellation cancellation = default)
    {
        var text = string.Join(Environment.NewLine, lines);
        return SetTextAsync(text, cancellation);
    }

    /// <summary>
    /// Clears the Clipboard and then adds lines of text data to it.
    /// </summary>
    public static void SetLines(string[] lines)
    {
        var text = string.Join(Environment.NewLine, lines);
        SetText(text);
    }
}