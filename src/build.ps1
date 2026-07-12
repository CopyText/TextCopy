$ErrorActionPreference = 'Stop'

function exec($command) {
    & $command
    if ($LastExitCode -ne 0) {
        throw "Failed with exit code $($LastExitCode): $command"
    }
}

$repoRoot = Split-Path $PSScriptRoot
$src = $PSScriptRoot

# ensure the SDK pinned in global.json is available, installing a repo-local copy if not
$sdkAvailable = $false
try {
    dotnet --version *> $null
    $sdkAvailable = $LastExitCode -eq 0
}
catch {
}
if (!$sdkAvailable) {
    $installDir = Join-Path $repoRoot '.dotnet'
    $jsonFile = Join-Path $repoRoot 'global.json'
    if ($env:OS -eq 'Windows_NT') {
        $installScript = Join-Path ([IO.Path]::GetTempPath()) 'dotnet-install.ps1'
        Invoke-WebRequest 'https://dot.net/v1/dotnet-install.ps1' -OutFile $installScript
        & $installScript -JSonFile $jsonFile -InstallDir $installDir
        $env:Path = "$installDir;$env:Path"
    }
    else {
        $installScript = Join-Path ([IO.Path]::GetTempPath()) 'dotnet-install.sh'
        Invoke-WebRequest 'https://dot.net/v1/dotnet-install.sh' -OutFile $installScript
        exec { bash $installScript --jsonfile $jsonFile --install-dir $installDir }
        $env:PATH = "${installDir}:$env:PATH"
    }
    $env:DOTNET_ROOT = $installDir
}

exec { dotnet build $src --configuration Release }

$tests = Join-Path $src 'Tests'
if ($env:OS -eq 'Windows_NT') {
    # Windows runs tests for all target frameworks, including net472
    exec { dotnet test $tests --configuration Release --no-build --no-restore -- RunConfiguration.TreatNoTestsAsError=true }
}
elseif ($IsLinux) {
    # xsel requires an X display
    exec { xvfb-run --auto-servernum dotnet test $tests --configuration Release --framework net10.0 --no-build --no-restore -- RunConfiguration.TreatNoTestsAsError=true }
}
else {
    exec { dotnet test $tests --configuration Release --framework net10.0 --no-build --no-restore -- RunConfiguration.TreatNoTestsAsError=true }
}
