#if (UAP)
using System;

namespace TextCopy
{
    public static partial class Clipboard
    {
        static Func<string?> CreateGet()
        {
            return WindowsClipboard.GetText;
        }
    }
}
#endif