#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ServiceShop.Api.ShoppingCart/ServiceShop.Api.ShoppingCart.csproj", "ServiceShop.Api.ShoppingCart/"]
RUN dotnet restore "ServiceShop.Api.ShoppingCart/ServiceShop.Api.ShoppingCart.csproj"
COPY . .
WORKDIR "/src/ServiceShop.Api.ShoppingCart"
RUN dotnet build "ServiceShop.Api.ShoppingCart.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServiceShop.Api.ShoppingCart.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceShop.Api.ShoppingCart.dll"]