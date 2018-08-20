using System;
using System.Diagnostics;
using System.Text;

static class BashRunner
{
    public static string Run(string commandLine)
    {
        var errorBuilder = new StringBuilder();
        var outputBuilder = new StringBuilder();
        var arguments = $"-c \"{commandLine}\"";
        using (var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "bash",
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = false,
            }
        })
        {
            process.ErrorDataReceived += (sender, args) => { errorBuilder.AppendLine(args.Data); };
            process.OutputDataReceived += (sender, args) => { outputBuilder.AppendLine(args.Data); };
            process.Start();
            process.WaitForExit();
            //if (process.ExitCode == 0)
            {
                return outputBuilder.ToString();
            }

//            var error = $@"Could not execute process. Command line: bash {arguments}.
//Output: {outputBuilder}
//Error: {errorBuilder}";
//            throw new Exception(error);
        }
    }
}