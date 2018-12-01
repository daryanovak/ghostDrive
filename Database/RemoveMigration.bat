@echo off
echo Removing migration ...
dotnet ef migrations remove --project ..\GhostDrive.Persistence

