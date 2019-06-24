using TextCopy;

class Program
{
    static int Main()
    {
        var text = "Hello World!";
        Clipboard.SetText(text);
        if (Clipboard.GetText() == text)
        {
            return 0;
        }
        return 1;
    }
}