#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Basket/BasketAPI/BasketAPI.csproj", "Services/Basket/BasketAPI/"]
COPY ["Services/Basket/Basket.Infastructure/Basket.Infastructure.csproj", "Services/Basket/Basket.Infastructure/"]
COPY ["Services/Basket/Basket.Domain/Basket.Domain.csproj", "Services/Basket/Basket.Domain/"]
COPY ["Services/Basket/Basket.Application/Basket.Application.csproj", "Services/Basket/Basket.Application/"]
COPY ["Kafka/Kafka/Kafka.csproj", "Kafka/Kafka/"]
RUN dotnet restore "Services/Basket/BasketAPI/BasketAPI.csproj"
COPY . .
WORKDIR "/src/Services/Basket/BasketAPI"
RUN dotnet build "BasketAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BasketAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BasketAPI.dll"]