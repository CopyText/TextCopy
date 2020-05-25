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
            await TextCopy.ClipboardService.SetTextAsync("AAA");
            var text = await TextCopy.ClipboardService.GetTextAsync();
            Debug.WriteLine(text);
        }
    }
}