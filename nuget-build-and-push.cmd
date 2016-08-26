::REM -UpdateNuGetExecutable not required since it's updated by VS.NET mechanisms
PowerShell -NoProfile -ExecutionPolicy Bypass -Command "& 'CompuMaster.Imaging\_CreateNewNuGetPackage\DoNotModify\New-NuGetPackage.ps1' -ProjectFilePath '.\CompuMaster.Imaging\CompuMaster.Imaging.VS2015.vbproj' -verbose -NoPrompt -PushPackageToNuGetGallery"
pause