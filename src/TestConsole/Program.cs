using System.Threading.Tasks;
using TextCopy;

class Program
{
    static async Task<int> Main()
    {
        var text = "Hello World!";
        await Clipboard.SetText(text);
        var result = await Clipboard.GetText();
        if (result == text)
        {
            return 0;
        }

        return 1;
    }
}