@echo off
echo CHIUSURA DI PROCESSI BLOCCANTI...
taskkill /f /im dotnet.exe >nul 2>&1
taskkill /f /im adb.exe >nul 2>&1
taskkill /f /im VBCSCompiler.exe >nul 2>&1
taskkill /f /im msbuild.exe >nul 2>&1

echo ELIMINAZIONE CARTELLE BIN/OBJ...
rd /s /q bin
rd /s /q obj

echo OPERAZIONE COMPLETATA. Premi un tasto per riaprire Visual Studio...
pause
start "" "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\devenv.exe"