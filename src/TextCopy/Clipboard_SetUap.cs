#if (UAP)
using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using UapClipboard = Windows.ApplicationModel.DataTransfer.Clipboard;
using Windows.UI.Core;

namespace TextCopy
{
    public static partial class Clipboard
    {
        static Func<string, CancellationToken, Task> CreateAsyncSet()
        {
            return (text, cancellation) =>
            {
                var dataPackage = new DataPackage();
                dataPackage.SetText(text);
                var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
                return dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => UapClipboard.SetContent(dataPackage))
                    .AsTask(cancellation);
            };
        }

        static Action<string> CreateSet()
        {
            var func = CreateAsyncSet();
            return text => { func(text, CancellationToken.None).GetAwaiter().GetResult(); };
        }
    }
}
#endif