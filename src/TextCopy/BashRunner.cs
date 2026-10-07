#if (NETSTANDARD || NETFRAMEWORK || NET5_0_OR_GREATER)

static class BashRunner
{
    public static string Run(string commandLine)
    {
        var arguments = $"-c \"{commandLine}\"";
        using var process = StartBash(arguments, out var outputBuilder, out var errorBuilder);
        if (!process.DoubleWaitForExit())
        {
            var timeoutError = $@"Process timed out. Command line: bash {arguments}.
Output: {outputBuilder}
Error: {errorBuilder}";
            throw new(timeoutError);
        }

        return GetResult(process, arguments, outputBuilder, errorBuilder);
    }

    public static async Task<string> RunAsync(string commandLine, Cancellation cancellation)
    {
        var arguments = $"-c \"{commandLine}\"";
        using var process = StartBash(arguments, out var outputBuilder, out var errorBuilder);

        await process.WaitForExitAsync(cancellation);

        return GetResult(process, arguments, outputBuilder, errorBuilder);
    }

    static Process StartBash(string arguments, out StringBuilder outputBuilder, out StringBuilder errorBuilder)
    {
        var output = new StringBuilder();
        var error = new StringBuilder();
        var process = new Process
        {
            StartInfo = new()
            {
                FileName = "bash",
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = false,
            }
        };
        process.Start();
        process.OutputDataReceived += (_, args) => { output.AppendLine(args.Data); };
        process.BeginOutputReadLine();
        process.ErrorDataReceived += (_, args) => { error.AppendLine(args.Data); };
        process.BeginErrorReadLine();
        outputBuilder = output;
        errorBuilder = error;
        return process;
    }

    static string GetResult(Process process, string arguments, StringBuilder outputBuilder, StringBuilder errorBuilder)
    {
        if (process.ExitCode == 0)
        {
            return outputBuilder.ToString();
        }

        var error = $@"Could not execute process. Command line: bash {arguments}.
Output: {outputBuilder}
Error: {errorBuilder}";
        throw new(error);
    }

    //To work around https://github.com/dotnet/runtime/issues/27128
    static bool DoubleWaitForExit(this Process process)
    {
        var result = process.WaitForExit(500);
        if (result)
        {
            process.WaitForExit();
        }
        return result;
    }
}
#endif