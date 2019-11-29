#if (UAP)
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using UapClipboard = Windows.ApplicationModel.DataTransfer.Clipboard;
using Windows.UI.Core;

namespace TextCopy
{
    public static partial class Clipboard
    {
        static Func<string,Task> CreateSet()
        {
            return async s =>
            {
                var dataPackage = new DataPackage();
                dataPackage.SetText(s);
                var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => UapClipboard.SetContent(dataPackage));
            };
        }
    }
}
#endif