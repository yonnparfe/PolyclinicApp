FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src


COPY ["PolyclinicApp/PolyclinicApp.csproj", "PolyclinicApp/"]
COPY ["../OtherProject/OtherProject.csproj", "OtherProject/"]


RUN dotnet restore "PolyclinicApp/PolyclinicApp.csproj"


COPY . .


WORKDIR "/src/PolyclinicApp"
RUN dotnet publish "PolyclinicApp.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PolyclinicApp.dll"]
