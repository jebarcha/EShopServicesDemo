#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EShopServices.Api.Gateway/EShopServices.Api.Gateway.csproj", "EShopServices.Api.Gateway/"]
RUN dotnet restore "EShopServices.Api.Gateway/EShopServices.Api.Gateway.csproj"
COPY . .
WORKDIR "/src/EShopServices.Api.Gateway"
RUN dotnet build "EShopServices.Api.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EShopServices.Api.Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShopServices.Api.Gateway.dll"]