#if ANDROID
using Android.Content;

namespace TextCopy;

public static partial class ClipboardService
{
    static Func<Cancellation, Task<string?>> CreateAsyncGet()
    {
        return _ => Task.FromResult(GetTextAndroid());
    }

    static Func<string?> CreateGet()
    {
        return GetTextAndroid;
    }

    static string? GetTextAndroid()
    {
        var context = Android.App.Application.Context;
        if (context is null)
        {
            return null;
        }

        var clipboard = ClipboardManager.FromContext(context);

        return clipboard?.Text;
    }
}
#endif