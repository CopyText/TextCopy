dotnet restore src/TextCopy.slnx --verbosity quiet

dotnet build src/TextCopy.slnx --configuration Release --no-restore --verbosity quiet

dotnet pack src/TextCopy.slnx --configuration Release --no-restore --verbosity quiet

dotnet test src --configuration Release --no-build --no-restore --nologo