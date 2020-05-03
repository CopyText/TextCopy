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
            var func = CreateAsyncGet();
            return () => func(CancellationToken.None).GetAwaiter().GetResult();
        }

        static string? GetTextAndroid()
        {
            var context = Android.App.Application.Context;
            if (context is null)
            {
                return null;
            }
            return ClipboardManager.FromContext(context).Text;
        }
    }
}
#endif