#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EShopServices.Api.Cart/EShopServices.Api.Cart.csproj", "EShopServices.Api.Cart/"]
RUN dotnet restore "EShopServices.Api.Cart/EShopServices.Api.Cart.csproj"
COPY . .
WORKDIR "/src/EShopServices.Api.Cart"
RUN dotnet build "EShopServices.Api.Cart.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EShopServices.Api.Cart.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShopServices.Api.Cart.dll"]