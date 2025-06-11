FROM mcr.microsoft.com/dotnet/framework/sdk:4.8-windowsservercore-ltsc2022 AS builder

WORKDIR /build
COPY . .

RUN nuget restore PolyclinicApp/PolyclinicApp.sln || nuget restore PolyclinicApp/PolyclinicApp.csproj

RUN if exist PolyclinicApp/PolyclinicApp.csproj ( \
    msbuild PolyclinicApp/PolyclinicApp.csproj \
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
COPY --from=builder C:/build/PolyclinicApp/DataBase/ ./DataBase/

ENTRYPOINT ["PolyclinicApp.exe"]
