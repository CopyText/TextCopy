#if (UAP)
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using UapClipboard = Windows.ApplicationModel.DataTransfer.Clipboard;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using System.Threading;

namespace TextCopy
{
    public static partial class Clipboard
    {
        static Func<CancellationToken, Task<string?>> CreateAsyncGet()
        {
            return async cancellation =>
            {
                using var resetEvent = new ManualResetEvent(false);
                var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
                string? value = null;
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                        async () =>
                        {
                            var dataPackageView = UapClipboard.GetContent();
                            if (dataPackageView.Contains(StandardDataFormats.Text))
                            {
                                value = await dataPackageView.GetTextAsync()
                                    .AsTask(cancellation);
                            }

                            resetEvent.Set();
                        })
                    .AsTask(cancellation);
                resetEvent.WaitOne();
                return value;
            };
        }

        static Func<string?> CreateGet()
        {
            var func = CreateAsyncGet();
            return () => func(CancellationToken.None).GetAwaiter().GetResult();
        }
    }
}
#endif