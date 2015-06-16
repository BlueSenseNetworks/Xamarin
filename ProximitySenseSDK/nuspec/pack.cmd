@echo off
del *.nupkg
nuget pack ProximitySenseSDK.nuspec -NoDefaultExcludes
pause