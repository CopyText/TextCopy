dotnet restore src/TextCopy/TextCopy.csproj --verbosity quiet

dotnet build src/TextCopy/TextCopy.csproj --configuration Release --no-restore --verbosity quiet

dotnet pack src/TextCopy/TextCopy.csproj --configuration Release --no-restore --verbosity quiet

dotnet restore src/Tests/Tests.csproj --verbosity quiet

dotnet build src/Tests/Tests.csproj --configuration Release --no-restore --verbosity quiet

dotnet test src/Tests/Tests.csproj --configuration Release --no-build --no-restore --nologo