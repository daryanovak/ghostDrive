@echo off

if "%1"=="" (
 echo No migration name
 goto End
)

echo Adding migration %1 ...
dotnet ef migrations add %1 --project ..\GhostDrive.Persistence --startup-project ..\GhostDrive.Web

:End