# syntax=docker/dockerfile:1.0

FROM mcr.microsoft.com/dotnet/framework/sdk:4.8-windowsservercore-ltsc2022 AS builder
WORKDIR /src
COPY . .
RUN mkdir "Документация\Диаграммы" && \
    if not exist "Документация\Диаграммы\диаграмма_классов.png" type nul > "Документация\Диаграммы\диаграмма_классов.png" && \
    if not exist "Документация\Диаграммы\диаграмма_последовательности.png" type nul > "Документация\Диаграммы\диаграмма_последовательности.png" && \
    if not exist "Документация\Диаграммы\fileName.bmp" type nul > "Документация\Диаграммы\fileName.bmp" && \
    if not exist "Документация\Диаграммы\execute.bmp" type nul > "Документация\Диаграммы\execute.bmp" && \
    if not exist "Документация\Диаграммы\диаграмма_пакетов.png" type nul > "Документация\Диаграммы\диаграмма_пакетов.png" && \
    if not exist "Документация\Диаграммы\usecase2.png" type nul > "Документация\Диаграммы\usecase2.png"
RUN nuget restore && msbuild /p:Configuration=Release /p:OutputPath=C:\out /p:ValidateMarkup=false

FROM mcr.microsoft.com/dotnet/framework/runtime:4.8-windowsservercore-ltsc2022
WORKDIR /app
COPY --from=builder C:\out .
COPY --from=builder C:\src\DataBase .\DataBase

ENV DISPLAY=host.docker.internal:0.0

ENTRYPOINT ["PolyclinicApp.exe"]
