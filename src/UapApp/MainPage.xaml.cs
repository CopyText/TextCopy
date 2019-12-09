using System;
using System.Diagnostics;
using System.Threading;

namespace UapApp
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            var thread1 = new Thread(TestClipboard);
            thread1.Start();
        }

        void TestClipboard()
        {
            var random = new Random();
            while (true)
            {
                var thread = new Thread(OutputClipboardText);
                thread.Start();
                thread.Join();
                Thread.Sleep(random.Next(1, 100) * 10);
            }
        }

        async void OutputClipboardText()
        {
            await TextCopy.Clipboard.SetText("AAA");
            var text = await TextCopy.Clipboard.GetText();
            Debug.WriteLine("Clipboard");
            if (text == null)
            {
                Debug.WriteLine("<NULL>");
            }
            else
            {
                Debug.WriteLine(text);
            }
        }
    }
}
