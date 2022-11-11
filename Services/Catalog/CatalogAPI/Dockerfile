#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Catalog/CatalogAPI/CatalogAPI.csproj", "Services/Catalog/CatalogAPI/"]
COPY ["Services/Catalog/Catalog.Infastructure/Catalog.Infastructure.csproj", "Services/Catalog/Catalog.Infastructure/"]
COPY ["Services/Catalog/Catalog.Domain/Catalog.Domain.csproj", "Services/Catalog/Catalog.Domain/"]
COPY ["Services/Catalog/Catalog.Application/Catalog.Application.csproj", "Services/Catalog/Catalog.Application/"]
COPY ["Kafka/Kafka/Kafka.csproj", "Kafka/Kafka/"]
RUN dotnet restore "Services/Catalog/CatalogAPI/CatalogAPI.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/CatalogAPI"
RUN dotnet build "CatalogAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CatalogAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CatalogAPI.dll"]