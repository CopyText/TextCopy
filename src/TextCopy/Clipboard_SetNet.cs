#if (NETFRAMEWORK)
using System;

namespace TextCopy
{
    public static partial class Clipboard
    {
        static Action<string> CreateSet()
        {
            return WindowsClipboard.SetText;
        }
    }
}
#endif