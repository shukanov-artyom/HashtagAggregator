@echo off

echo Started

SET currentPath=%~dp0
SET authPath=%~dp0\..\..\..\HashtagAggregator\WebAPI\HashtagAggregator.IdentityServer
SET ASPNETCORE_ENVIRONMENT=dev

cd %authPath%

echo %authPath%

dotnet run 

cd %currentPath%

echo Finished