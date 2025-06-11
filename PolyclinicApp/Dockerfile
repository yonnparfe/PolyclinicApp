# syntax=docker/dockerfile:1.0

FROM mcr.microsoft.com/dotnet/framework/sdk:4.8-windowsservercore-ltsc2022 AS builder

WORKDIR /build
COPY . .

RUN nuget restore PolyclinicApp.sln || nuget restore PolyclinicApp.csproj


RUN if exist PolyclinicApp.csproj ( \
    msbuild PolyclinicApp.csproj \
    /p:Configuration=Release \
    /p:OutputPath=C:\publish \
    /p:DeployOnBuild=true \
    /p:PublishProfile=FolderProfile \
    ) else ( \
    echo ERROR: PolyclinicApp.csproj not found && \
    dir /s *.csproj && \
    exit 1 \
    )


FROM mcr.microsoft.com/dotnet/framework/runtime:4.8-windowsservercore-ltsc2022
WORKDIR /app

COPY --from=builder C:/publish/ .
COPY --from=builder C:/build/DataBase/ ./DataBase/


ENTRYPOINT ["PolyclinicApp.exe"]
