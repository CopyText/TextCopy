#if (NETFRAMEWORK)
using System;
using System.Threading.Tasks;

namespace TextCopy
{
    public static partial class Clipboard
    {
        static Func<string,Task> CreateSet()
        {
            return WindowsClipboard.SetText;
        }
    }
}
#endif