#if (NETFRAMEWORK)
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TextCopy
{
    public static partial class Clipboard
    {
        static Func<string, CancellationToken, Task> CreateAsyncSet()
        {
            return WindowsClipboard.SetTextAsync;
        }

        static Action<string> CreateSet()
        {
            return WindowsClipboard.SetText;
        }
    }
}
#endif