#if (UAP)
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using UapClipboard= Windows.ApplicationModel.DataTransfer.Clipboard;

namespace TextCopy
{
    public static partial class Clipboard
    {
        static Func<Task<string?>> CreateGet()
        {
            return async () =>
            {
                var dataPackageView = UapClipboard.GetContent();

                if (dataPackageView.Contains(StandardDataFormats.Text))
                {
                    return await dataPackageView.GetTextAsync();
                }

                return null;
            };
        }
    }
}
#endif