#if (NETSTANDARD || NETFRAMEWORK)
using System;
using System.Diagnostics;

static class BashRunner
{
    public static void Run(string commandLine)
    {
        var arguments = $"-c \"{commandLine}\"";
        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "bash",
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = false,
            }
        };
        process.Start();
        
        // xclip communicates clipboard contents by spawning a child process, which results in the stdout and stderr
        // file handles not being closed, and so process.WaitForExit() (which would wait for stdout) will never exit,
        // as a result it's problematic to try to print stdout from xclip.
        // Context as to why WaitForExit(x) won't reliably print stdout/stderr: https://github.com/dotnet/runtime/issues/27128
        if (!process.WaitForExit(500))
        {
            var timeoutError = $@"Process timed out. Command line: bash {arguments}";
            throw new Exception(timeoutError);
        }
        if (process.ExitCode == 0)
        {
            return;
        }

        var error = $@"Could not execute process. Command line: bash {arguments} Exit code: {process.ExitCode}";
        throw new Exception(error);
    }
}
#endif