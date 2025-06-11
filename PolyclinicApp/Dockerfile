# syntax=docker/dockerfile:1.0

FROM mcr.microsoft.com/dotnet/framework/sdk:4.8-windowsservercore-ltsc2022 AS build

WORKDIR /src


COPY PolyclinicApp/PolyclinicApp.csproj PolyclinicApp/


RUN nuget restore "PolyclinicApp/PolyclinicApp.csproj"


COPY PolyclinicApp/. ./PolyclinicApp/


WORKDIR /src/PolyclinicApp


RUN msbuild PolyclinicApp.csproj /p:Configuration=Release /p:OutputPath=C:\output /p:ValidateMarkup=false /p:DeployOnBuild=true /p:PublishProfile=FolderProfile

FROM mcr.microsoft.com/dotnet/framework/runtime:4.8-windowsservercore-ltsc2022
WORKDIR /app


COPY --from=build C:/output/ .


ENTRYPOINT ["PolyclinicApp.exe"]
