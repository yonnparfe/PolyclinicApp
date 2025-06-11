# syntax=docker/dockerfile:1.0

FROM mcr.microsoft.com/dotnet/framework/sdk:4.8-windowsservercore-ltsc2022 AS builder


WORKDIR /src
COPY PolyclinicApp/. ./PolyclinicApp/


WORKDIR /src/PolyclinicApp


RUN nuget restore

RUN msbuild PolyclinicApp/PolyclinicApp.csproj \
    /p:Configuration=Release \
    /p:OutputPath=C:\publish \
    /p:DeployOnBuild=true


FROM mcr.microsoft.com/dotnet/framework/runtime:4.8-windowsservercore-ltsc2022
WORKDIR /app


COPY --from=builder C:/publish/ .
COPY --from=builder C:/src/PolyclinicApp/DataBase/ ./DataBase/


RUN powershell -Command \
    Invoke-WebRequest https://download.microsoft.com/download/8/6/8/868E5C4A-15A6-4E1F-B4A0-DF3D6B57B4A2/ENU/x64/sqlncli.msi -OutFile sqlncli.msi; \
    Start-Process -Wait msiexec.exe -ArgumentList '/i sqlncli.msi /quiet /norestart'; \
    Remove-Item -Force sqlncli.msi

ENTRYPOINT ["PolyclinicApp.exe"]
