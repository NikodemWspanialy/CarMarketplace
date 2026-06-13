FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["CarMarketplace.API/CarMarketplace.API.csproj", "CarMarketplace.API/"]
COPY ["CarMarketplace.Application/CarMarketplace.Application.csproj", "CarMarketplace.Application/"]
COPY ["CarMarketplace.Domain/CarMarketplace.Domain.csproj", "CarMarketplace.Domain/"]
COPY ["CarMarketplace.Infrastructure/CarMarketplace.Infrastructure.csproj", "CarMarketplace.Infrastructure/"]
RUN dotnet restore "CarMarketplace.API/CarMarketplace.API.csproj"
COPY . .
WORKDIR "/src/CarMarketplace.API"
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CarMarketplace.API.dll"]
