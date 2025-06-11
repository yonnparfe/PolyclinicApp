# syntax=docker/dockerfile:1

ARG BASE_IMAGE_TAG=4.8
FROM mcr.microsoft.com/dotnet/framework/sdk:${BASE_IMAGE_TAG} AS builder
SHELL ["cmd", "/S", "/C"]
WORKDIR /src

COPY . .

RUN nuget restore PolyclinicApp.csproj

RUN msbuild PolyclinicApp.csproj /p:Configuration=Release /p:OutputPath=C:\out

FROM mcr.microsoft.com/dotnet/framework/runtime:${BASE_IMAGE_TAG}
WORKDIR /app

RUN powershell -Command \
    Invoke-WebRequest "https://download.microsoft.com/download/8/6/8/868E5C4A-15A6-4E1F-B4A0-DF3D6B57B4A2/ENU/x64/sqlncli.msi" -OutFile sqlncli.msi ; \
    Start-Process -Wait -FilePath msiexec.exe -ArgumentList /i, sqlncli.msi, /quiet, /norestart ; \
    Remove-Item -Force sqlncli.msi

COPY --from=builder C:/out .
COPY --from=builder C:/src/DataBase ./DataBase
COPY --from=builder C:/src/app.config .

COPY --from=builder C:/src/init.ps1 .

ENV DISPLAY=host.docker.internal:0.0

ENTRYPOINT ["powershell", "-ExecutionPolicy", "Bypass", "-File", "C:\\app\\init.ps1"]
