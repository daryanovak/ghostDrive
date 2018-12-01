@echo off
echo Updating database ...
dotnet ef database update --project ..\GhostDrive.Persistence

