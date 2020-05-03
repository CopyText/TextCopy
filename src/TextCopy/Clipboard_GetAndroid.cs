#if ANDROID
using System;
using System.Threading.Tasks;
using System.Threading;
using Android.Content;

namespace TextCopy
{
    public static partial class Clipboard
    {
        static Func<CancellationToken, Task<string?>> CreateAsyncGet()
        {
            return token => Task.FromResult(GetTextAndroid());
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
}
#endif