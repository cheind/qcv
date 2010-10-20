::
:: QCV
:: Copyright (c) Christoph Heindl, 2010
::
:: This batch file builds the entire QCV solution for the .NET 3.5 target framework.
:: Requires Visual Studio 2010.
::

call "%ProgramFiles(x86)%\Microsoft Visual Studio 10.0\VC\vcvarsall.bat" x86
msbuild "..\..\qcv.sln" /p:TargetFrameworkVersion=v3.5 /p:Configuration=Release /p:Platform=x86 /t:rebuild /ToolsVersion:4.0
ren "..\..\bin\x86\qcv_installer_x86.msi" "qcv_installer_net35_x86.msi"
pause