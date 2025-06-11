 # syntax=docker/dockerfile:1.0.0

# Сборка
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS builder
WORKDIR /src
COPY . .
RUN nuget restore && msbuild PolyclinicApp.csproj /p:Configuration=Release /p:OutputPath=C:\out

# Запуск
FROM mcr.microsoft.com/dotnet/framework/runtime:4.8
WORKDIR /app

COPY --from=builder C:/out .
COPY --from=builder C:/src/DataBase ./DataBase
COPY --from=builder C:/src/*.config .
COPY --from=builder C:/src/init.ps1 .

ENV DISPLAY=host.docker.internal:0.0
ENTRYPOINT ["powershell", "-ExecutionPolicy", "Bypass", "-File", ".\\init.ps1"]
