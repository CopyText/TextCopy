#if IOS
using UIKit;

namespace TextCopy;

public static partial class ClipboardService
{
    static Func<CancellationToken, Task<string?>> CreateAsyncGet()
    {
        return _ => Task.FromResult(GetTextIos());
    }

    static Func<string?> CreateGet()
    {
        return GetTextIos;
    }

    static string? GetTextIos()
    {
        return UIPasteboard.General.String;
    }
}
#endif