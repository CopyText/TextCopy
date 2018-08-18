using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

static class WindowsClipboard
{
    public static void SetText(string text)
    {
        if (!OpenClipboard(IntPtr.Zero))
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        EmptyClipboard();
        var hGlobal = IntPtr.Zero;
        try
        {
            var bytes = (text.Length + 1) * 2;
            hGlobal = Marshal.AllocHGlobal(bytes);

            if (hGlobal == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            var target = GlobalLock(hGlobal);

            if (target == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            try
            {
                Marshal.Copy(text.ToCharArray(), 0, target, text.Length);
            }
            finally
            {
                GlobalUnlock(target);
            }

            if (SetClipboardData(CF_UNICODETEXT, hGlobal) == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            hGlobal = IntPtr.Zero;
        }
        finally
        {
            if (hGlobal != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(hGlobal);
            }

            CloseClipboard();
        }
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr GlobalLock(IntPtr hMem);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool GlobalUnlock(IntPtr hMem);

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool OpenClipboard(IntPtr hWndNewOwner);

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool CloseClipboard();

    [DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr SetClipboardData(uint uFormat, IntPtr data);

    [DllImport("user32.dll")]
    static extern bool EmptyClipboard();

    const uint CF_UNICODETEXT = 13;
}