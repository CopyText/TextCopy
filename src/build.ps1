dotnet msbuild src/TextCopy.slnx -t:restore -p:Configuration=Release -verbosity:quiet

dotnet msbuild src/TextCopy.slnx -t:build -p:Configuration=Release -verbosity:quiet

dotnet msbuild src/TextCopy.slnx -t:pack -p:Configuration=Release -verbosity:quiet

dotnet test src --configuration Release --no-build --no-restore --nologo