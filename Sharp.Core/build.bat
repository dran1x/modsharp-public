@echo off
git fetch --unshallow
git rev-list --count HEAD > gitcommit.txt
set /p GITCOMMIT=<gitcommit.txt
del /s /q "gitcommit.txt"
cls
title Building Sharp.Core +git-%GITCOMMIT%
dotnet publish Sharp.Core.csproj -f net9.0 -r win-x64 --no-self-contained -c Release --output "%cs2%/game/sharp/core" -p:VersionSuffix=%GITCOMMIT%
echo:
echo Build Completed...
echo:
echo Copying Assets...
xcopy "..\.asset\*" "%cs2%/game/sharp/" /E /I /Y
echo Copy Assets Completed...
echo:
pause
