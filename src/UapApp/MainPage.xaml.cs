using System.Diagnostics;

namespace UapApp
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            OutputClipboardText();
        }

        async void OutputClipboardText()
        {
            await TextCopy.Clipboard.SetText("AAA");
            var text = await TextCopy.Clipboard.GetText();
            Debug.WriteLine(text);
        }
    }
}