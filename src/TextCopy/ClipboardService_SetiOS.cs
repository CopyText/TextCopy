#if IOS
using UIKit;

namespace TextCopy;

public static partial class ClipboardService
{
    static Func<string, CancellationToken, Task> CreateAsyncSet()
    {
        return (text, _) =>
        {
            SetTextIos(text);
            return Task.CompletedTask;
        };
    }

    static Action<string> CreateSet()
    {
        return SetTextIos;
    }

    static void SetTextIos(string text)
    {
        UIPasteboard.General.String = text;
    }
}
#endif