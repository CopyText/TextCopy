using System.Threading.Tasks;
using TextCopy;

class Program
{
    static async Task<int> Main()
    {
        var text = "Hello World!";
        await Clipboard.SetTextAsync(text);
        var result = await Clipboard.GetTextAsync();
        if (result == text)
        {
            return 0;
        }

        return 1;
    }
}