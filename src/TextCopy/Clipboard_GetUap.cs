#if (UAP)
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using UapClipboard = Windows.ApplicationModel.DataTransfer.Clipboard;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace TextCopy
{
    public static partial class Clipboard
    {
        static Func<Task<string?>> CreateGet()
        {
            return async () =>
            {
                var dataPackageView = UapClipboard.GetContent();

                var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
                string? value = null;
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    async () => {

                        if (dataPackageView.Contains(StandardDataFormats.Text))
                        {
                            value = await dataPackageView.GetTextAsync();
                            return;
                        }
                });
                return value;
            };
        }
    }
}
#endif