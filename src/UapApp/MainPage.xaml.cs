using System.Diagnostics;
using System.Threading;

namespace UapApp
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            var thread1 = new Thread(OutputClipboardText);
            thread1.Start();
        }

        async void OutputClipboardText()
        {
            await TextCopy.Clipboard.SetText("AAA");
            var text = await TextCopy.Clipboard.GetText();
            Debug.WriteLine(text);
        }
    }
}