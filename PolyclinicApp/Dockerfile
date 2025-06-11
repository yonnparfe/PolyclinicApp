# syntax=docker/dockerfile:1.0

FROM mcr.microsoft.com/dotnet/framework/sdk:4.8-windowsservercore-ltsc2022 AS builder
WORKDIR /src
COPY . .

RUN (for /f "delims=" %f in ('dir /s /b *.csproj') do ( \
      echo Found project: %f && \
      nuget restore "%f" \
    )) && \
    (for /f "delims=" %f in ('dir /s /b *.sln') do ( \
      echo Found solution: %f && \
      nuget restore "%f" \
    ))
    
RUN nuget restore && \
    msbuild PolyclinicApp.csproj \
    /p:Configuration=Release \
    /p:OutputPath=C:\output \
    /p:ValidateMarkup=false \
    /p:DeployOnBuild=true \
    /p:PublishProfile=FolderProfile

FROM mcr.microsoft.com/dotnet/framework/runtime:4.8-windowsservercore-ltsc2022
WORKDIR /app
COPY --from=builder C:/output/. .
COPY --from=builder C:\src\DataBase .\DataBase

ENV DISPLAY=host.docker.internal:0.0

ENTRYPOINT ["C:\\app\\PolyclinicApp.exe"]
