#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiGateways/ECommerce.Aggregator/ECommerce.Aggregator.csproj", "ApiGateways/ECommerce.Aggregator/"]
RUN dotnet restore "ApiGateways/ECommerce.Aggregator/ECommerce.Aggregator.csproj"
COPY . .
WORKDIR "/src/ApiGateways/ECommerce.Aggregator"
RUN dotnet build "ECommerce.Aggregator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerce.Aggregator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Aggregator.dll"]