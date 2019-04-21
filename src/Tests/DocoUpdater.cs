using MarkdownSnippets;
using Xunit;

public class DocoUpdater
{
    [Fact]
    public void Run()
    {
        DirectoryMarkdownProcessor.RunForFilePath();
    }
}