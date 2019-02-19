# install script for custom visual studio 2017 start page
# copies startpagecontrol.dll to Visual Studio Private Assemblies folder
# copies the XAML file to the StartPages folder
# you must open Visual Studio/Tools/Options/Environment/Startup and set the Customize Start Page Dropdown to start-page

param ([string]$vsDir)

$versionYear = $vsDir.Split("\\")[3];

write-host 'VS Install Dir: ' $vsDir;
$file = (Get-Item -Path "..\" -Verbose).FullName + '\setup\StartPageControl.dll';

$assemblyDir = $vsDir + "\Common7\IDE\PrivateAssemblies";
Copy-Item -Path "..\setup\StartPageControl.dll" -Destination $assemblyDir -errorAction SilentlyContinue -errorVariable errors;
foreach($e in $errors)
{
    if ($null -ne $e.Exception)
    {
        write-host -foregroundColor Red "Exception: $($e.Exception)"
    }
    write-host -foregroundColor Red "Error: An error occured during copy operation."
}
write-host "File " $file " copied to " $assemblyDir;

$startPageFolder = [Environment]::GetFolderPath('MyDocuments');
$startPageFolder = $startPageFolder + '\Visual Studio '+ $versionYear + '\StartPages';
write-host $startPageFolder

Copy-Item -Path ".\start-page.xaml" -Destination $startPageFolder

write-host 'You must now open Visual Studio and goto Tools/Options/Environment/Startup and set the Customize Start Page Dropdown to start-page.xaml' -ForegroundColor Yellow