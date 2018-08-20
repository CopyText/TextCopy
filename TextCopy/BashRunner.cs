using System;
using System.Diagnostics;

static class BashRunner
{
    public static string Run(string commandLine)
    {
        using (var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"{commandLine}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = false,
            }
        })
        {
            process.Start();
            var result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                var error = $"Could not execute process. Command line: {commandLine}.{Environment.NewLine}Output:{result}";
                throw new Exception(error);
            }
            return result;
        }
    }
}