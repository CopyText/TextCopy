#if IOS
using System;
using System.Threading.Tasks;
using System.Threading;
using UIKit;

namespace TextCopy
{
    public static partial class Clipboard
    {
        static Func<CancellationToken, Task<string?>> CreateAsyncGet()
        {
            return token => Task.FromResult(GetTextiOS());
        }

        static Func<string?> CreateGet()
        {
            return GetTextiOS;
        }

        static string? GetTextiOS()
        {
            return UIPasteboard.General.String;
        }
    }
}
#endif