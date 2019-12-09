#if NETFRAMEWORK
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TextCopy
{
    public static partial class Clipboard
    {
        static Func<CancellationToken, Task<string?>> CreateGet()
        {
            return WindowsClipboard.GetText;
        }
    }
}
#endif