# Basis-Image für die Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

# Build-Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PharmacyManagement.Application.csproj", "."]
RUN dotnet restore "./PharmacyManagement.Application.csproj"
COPY . .
RUN dotnet build "./PharmacyManagement.Application.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish-Stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PharmacyManagement.Application.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Finales Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS="http://+:5000"
ENTRYPOINT ["dotnet", "PharmacyManagement.Application.dll"]
