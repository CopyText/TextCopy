#if IOS
using System;
using System.Threading;
using System.Threading.Tasks;
using UIKit;

namespace TextCopy
{
    public static partial class Clipboard
    {
        static Func<string, CancellationToken, Task> CreateAsyncSet()
        {
            return (text, cancellation) =>
            {
                SetTextiOS(text);
                return Task.CompletedTask;
            };
        }

        static Action<string> CreateSet()
        {
            return SetTextiOS;
        }

        static void SetTextiOS(string text)
        {
            UIPasteboard.General.String = text;
        }
    }
}
#endif