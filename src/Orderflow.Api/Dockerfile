﻿# Use the official .NET 8 SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["src/Orderflow.Api/Orderflow.Api.csproj", "Orderflow.Api/"]
RUN dotnet restore "Orderflow.Api/Orderflow.Api.csproj"

# Copy the rest of the source code
COPY src/ .

# Build the application
WORKDIR "/src/Orderflow.Api"
RUN dotnet build "Orderflow.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Orderflow.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
EXPOSE 443

# Set the entry point for the application
ENTRYPOINT ["dotnet", "Orderflow.Api.dll"]