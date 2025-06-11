# syntax=docker/dockerfile:1.4

# Этап сборки
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8-windowsservercore-ltsc2022 AS builder

WORKDIR /src
COPY . .

RUN nuget restore PolyclinicApp.csproj && \
    msbuild PolyclinicApp.csproj /p:Configuration=Release /p:OutputPath=C:\out

FROM mcr.microsoft.com/dotnet/framework/runtime:4.8-windowsservercore-ltsc2022

WORKDIR /app

COPY --from=builder C:\out .
COPY --from=builder C:\src\DataBase .\DataBase
COPY --from=builder C:\src\app.config .

ENV DISPLAY=host.docker.internal:0.0

ENTRYPOINT ["PolyclinicApp.exe"]
