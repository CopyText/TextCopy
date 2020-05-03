#if ANDROID
using System;
using System.Threading;
using System.Threading.Tasks;
using Android.Content;

namespace TextCopy
{
    public static partial class Clipboard
    {
        static Func<string, CancellationToken, Task> CreateAsyncSet()
        {
            return (text, cancellation) =>
            {
                var context = Android.App.Application.Context;
                if (context is null) return Task.FromException(new InvalidOperationException("Android context is null"));

                var clipboard = ClipboardManager.FromContext(context);
                clipboard.Text = text;

                return Task.CompletedTask;
            };
        }

        static Action<string> CreateSet()
        {
            var func = CreateAsyncSet();
            return text => { func(text, CancellationToken.None).GetAwaiter().GetResult(); };
        }
    }
}
#endif