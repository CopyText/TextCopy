$ErrorActionPreference = 'Stop'

function exec($command) {
    & $command
    if ($LastExitCode -ne 0) {
        throw "Failed with exit code $($LastExitCode): $command"
    }
}

if ($env:OS -eq 'Windows_NT') {
    # full MSBuild is required for the Xamarin targets and the legacy Android/iOS projects
    exec { msbuild.exe src/TextCopy.sln /t:restore /p:Configuration=Release -verbosity:quiet }
    exec { msbuild.exe src/TextCopy.sln /t:build /p:Configuration=Release -verbosity:quiet }
    exec { msbuild.exe src/TextCopy.sln /t:pack /p:Configuration=Release -verbosity:quiet }
    exec { dotnet test src --configuration Release --no-build --no-restore }
}
else {
    # the Xamarin targets and the legacy Android/iOS projects cannot build on the dotnet CLI,
    # so build only what the tests need and run the net6.0 tests against the OS clipboard
    exec { dotnet build src/Weavers --configuration Release }
    if ($IsLinux) {
        # xsel requires an X display
        exec { xvfb-run --auto-servernum dotnet test src/Tests --configuration Release --framework net6.0 }
    }
    else {
        exec { dotnet test src/Tests --configuration Release --framework net6.0 }
    }
}
