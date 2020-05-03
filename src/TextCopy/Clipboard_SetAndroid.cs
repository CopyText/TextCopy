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
                SetTextAndroid(text);
                return Task.CompletedTask;
            };
        }

        static Action<string> CreateSet()
        {
            return SetTextAndroid;
        }

        static void SetTextAndroid(string text)
        {
            var context = Android.App.Application.Context;
            if (context is null)
            {
                throw new InvalidOperationException("Android context is null");
            }

            var clipboard = ClipboardManager.FromContext(context);
            if (clipboard is null)
            {
                throw new InvalidOperationException("ClipboardManager.FromContext is null");
            }

            clipboard.Text = text;
        }
    }
}
#endif