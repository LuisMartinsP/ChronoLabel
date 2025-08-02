@echo off
REM Vai para a pasta do seu projeto principal
cd /d "C:\Users\Luis Felipe\Documents\ChronoLabel\ChronoLabel"

echo Limpando cache do NuGet...
dotnet nuget locals all --clear

echo Limpando build antigo do projeto principal...
dotnet clean ChronoLabel.csproj

echo Limpando build antigo dos testes...
dotnet clean .\Tests\UnitTests\UnitTests.csproj

echo Restaurando pacotes do projeto principal...
dotnet restore ChronoLabel.csproj

echo Restaurando pacotes do projeto de testes...
dotnet restore .\Tests\UnitTests\UnitTests.csproj

echo Compilando projeto principal...
dotnet build ChronoLabel.csproj

echo Compilando projeto de testes...
dotnet build .\Tests\UnitTests\UnitTests.csproj

echo Executando testes...
dotnet test .\Tests\UnitTests\UnitTests.csproj --no-build --verbosity normal

echo Processo finalizado. Pressione qualquer tecla para sair.
pause >nul
