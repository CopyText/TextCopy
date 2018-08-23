using System;
using System.Runtime.InteropServices;

static class OsxClipboard
{
    static IntPtr nsString;
    static IntPtr generalPasteboard;
    static IntPtr utfDataType;
    static IntPtr dataType;

    static OsxClipboard()
    {
        nsString = objc_getClass("NSString");
        var nsPasteboard = objc_getClass("NSPasteboard");
        generalPasteboard = objc_msgSend(nsPasteboard, sel_registerName("generalPasteboard"));
        utfDataType = objc_msgSend(objc_msgSend(nsString, sel_registerName("alloc")), sel_registerName("initWithUTF8String:"), "public.utf8-plain-text");

        dataType = objc_msgSend(objc_msgSend(nsString, sel_registerName("alloc")), sel_registerName("initWithUTF8String:"), "NSStringPboardType");
    }

    public static string GetText()
    {
        IntPtr ptr = default;
        IntPtr charArray = default;
        try
        {
            ptr = objc_msgSend(generalPasteboard, sel_registerName("stringForType:"), dataType);
            charArray = objc_msgSend(ptr, sel_registerName("UTF8String"));

            return Marshal.PtrToStringAnsi(charArray);
        }
        finally
        {
            if (ptr != default)
            {
                objc_msgSend(ptr, sel_registerName("release"));
            }
            if (charArray != default)
            {
                objc_msgSend(charArray, sel_registerName("release"));
            }
        }
    }

    public static void SetText(string text)
    {
        IntPtr str = default;
        try
        {
            str = objc_msgSend(objc_msgSend(nsString, sel_registerName("alloc")), sel_registerName("initWithUTF8String:"), text);

            objc_msgSend(generalPasteboard, sel_registerName("clearContents"));
            objc_msgSend(generalPasteboard, sel_registerName("setString:forType:"), str, utfDataType);
        }
        finally
        {
            if (str != default)
            {
                objc_msgSend(str, sel_registerName("release"));
            }
        }
    }

    [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
    static extern IntPtr objc_getClass(string className);

    [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
    static extern IntPtr objc_msgSend(IntPtr receiver, IntPtr selector);

    [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
    static extern IntPtr objc_msgSend(IntPtr receiver, IntPtr selector, string arg1);

    [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
    static extern IntPtr objc_msgSend(IntPtr receiver, IntPtr selector, IntPtr arg1);

    [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
    static extern IntPtr objc_msgSend(IntPtr receiver, IntPtr selector, IntPtr arg1, IntPtr arg2);

    [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
    static extern IntPtr sel_registerName(string selectorName);
}