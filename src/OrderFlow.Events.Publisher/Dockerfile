# Use official .NET SDK image for building the project
FROM public.ecr.aws/lambda/dotnet:8 AS base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /src
RUN pwd && ls

# Copy the csproj file and restore any dependencies (use separate layers for caching)
COPY src/OrderFlow.Events.Publisher/OrderFlow.Events.Publisher.csproj OrderFlow.Events.Publisher/
RUN dotnet restore OrderFlow.Events.Publisher/OrderFlow.Events.Publisher.csproj

# Copy the entire source code and build the project
COPY . .
RUN dotnet build OrderFlow.Events.Publisher/OrderFlow.Events.Publisher.csproj \
    --configuration Release \
    --output /app/build

FROM build AS publish
RUN pwd && ls
RUN dotnet publish OrderFlow.Events.Publisher/OrderFlow.Events.Publisher.csproj \
    -c Release \
    --runtime linux-x64 \
    --self-contained false \
    --output /app/publish \
    -p:PublishReadyToRun=true

FROM base AS final
# Copy the published output from the build stage to the runtime stage
COPY --from=publish /app/publish ${LAMBDA_TASK_ROOT}

# Command to run the Lambda function
CMD [ "OrderFlow.Events.Publisher::OrderFlow.Events.Publisher.Function::FunctionHandler" ]