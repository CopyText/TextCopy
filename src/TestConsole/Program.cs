using System;
using System.Threading.Tasks;
using TextCopy;

class Program
{
    static async Task<int> Main()
    {
        var text = "Hello World!";
        await ClipboardService.SetTextAsync(text);
        var result = await ClipboardService.GetTextAsync();
        Console.WriteLine(result);
        if (result == text)
        {
            return 0;
        }

        return 1;
    }
}