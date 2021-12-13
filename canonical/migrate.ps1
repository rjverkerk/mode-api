dotnet build .\mode-canonical-api.sln

cd .\mode-canonical-api\bin\Debug\net5.0;

dotnet-fm migrate -p postgres -c "Server=192.168.1.76;Port=5432;User Id=dbuser;Password=pass1234;Database=mode_canonical;" -a mode-canonical-api.data.dll;

cd ../../../../;
