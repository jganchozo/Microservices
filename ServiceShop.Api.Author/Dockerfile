#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ServiceShop.Api.Author/ServiceShop.Api.Author.csproj", "ServiceShop.Api.Author/"]
RUN dotnet restore "ServiceShop.Api.Author/ServiceShop.Api.Author.csproj"
COPY . .
WORKDIR "/src/ServiceShop.Api.Author"
RUN dotnet build "ServiceShop.Api.Author.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServiceShop.Api.Author.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceShop.Api.Author.dll"]