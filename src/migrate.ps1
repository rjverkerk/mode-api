dotnet build .\mode-api.sln

cd .\mode-api\bin\Debug\net5.0;

dotnet-fm migrate -p postgres -c "Server=192.168.1.76;Port=5432;User Id=dbuser;Password=pass1234;Database=mode;" -a mode-api.data.dll;

cd ../../../../;
