@echo off

setlocal enabledelayedexpansion

set MSBUILDER="C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe"
set NUGET="C:\tools\nuget.exe"
set OUTPUTDIR="L:\Work\111 Online\nhs111-build-dir\nhs111-feedback-api"
REM C:\Windows\Microsoft.NET\Framework64\v4.0.30319

%NUGET% restore Feedback\NHS111.Feedback.Api.sln

%MSBUILDER% Feedback\NHS111.Feedback.Api.sln /t:Rebuild /p:Configuration=Release /p:VisualStudioVersion=12.0 /p:BuildingProject=true;OutDir=%OUTPUTDIR%