# Use the official .NET 8 SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["src/OrderFlow.Api/OrderFlow.Api.csproj", "OrderFlow.Api/"]
RUN dotnet restore "OrderFlow.Api/OrderFlow.Api.csproj"

# Copy the rest of the source code
COPY src/ .

# Build the application
WORKDIR "/src/OrderFlow.Api"
RUN dotnet build "OrderFlow.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "OrderFlow.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
EXPOSE 443

# Set the entry point for the application
ENTRYPOINT ["dotnet", "OrderFlow.Api.dll"]