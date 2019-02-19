REM:============================================================
REM:  main install for custom Visual Studio start page
REM:  run from elevated command prompt
REM:============================================================
for /f "usebackq tokens=*" %%i in (`vswhere -latest -products * -requires Microsoft.Component.MSBuild -property installationPath`) do (
  set InstallDir=%%i
)

echo  %InstallDir%

PowerShell -NoProfile -ExecutionPolicy Bypass -File install-start-page.ps1 "%InstallDir%"
