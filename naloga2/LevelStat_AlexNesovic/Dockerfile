FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# kopiramo samo .csproj in obnovimo odvisnosti
COPY LevelStat_AlexNesovic.csproj ./
RUN dotnet restore

# kopiramo vse ostale datoteke
COPY . ./
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "LevelStat_AlexNesovic.dll"]

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
