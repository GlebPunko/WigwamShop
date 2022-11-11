#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/IdentityServer/IdentityServerAPI/IdentityServerAPI.csproj", "Services/IdentityServer/IdentityServerAPI/"]
COPY ["Services/IdentityServer/IdentityServer.Application/IdentityServer.Application.csproj", "Services/IdentityServer/IdentityServer.Application/"]
COPY ["Services/IdentityServer/IdentityServer.Domain/IdentityServer.Domain.csproj", "Services/IdentityServer/IdentityServer.Domain/"]
COPY ["Services/IdentityServer/IdentityServer.Infastructure/IdentityServer.Infastructure.csproj", "Services/IdentityServer/IdentityServer.Infastructure/"]
RUN dotnet restore "Services/IdentityServer/IdentityServerAPI/IdentityServerAPI.csproj"
COPY . .
WORKDIR "/src/Services/IdentityServer/IdentityServerAPI"
RUN dotnet build "IdentityServerAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityServerAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityServerAPI.dll"]