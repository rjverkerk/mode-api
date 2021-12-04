dotnet build .\mode-platonic-api.sln

cd .\mode-platonic-api\bin\Debug\net5.0;

dotnet-fm migrate -p postgres -c "Server=192.168.1.76;Port=5432;User Id=dbuser;Password=pass1234;Database=mode_platonic;" -a mode-platonic-api.data.dll;

cd ../../../../;