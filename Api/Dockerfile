# Stage 1: Builder
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS builder

WORKDIR /src

# Copy project files from parent directory context
COPY Api/Api.csproj ./Api/
COPY Shared/Shared.csproj ./Shared/

# Restore dependencies
RUN dotnet restore "Api/Api.csproj"

# Copy entire source
COPY Api/ ./Api/
COPY Shared/ ./Shared/

# Build in Release mode
RUN dotnet build "Api/Api.csproj" -c Release -o /app/build

# Publish
RUN dotnet publish "Api/Api.csproj" -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

# Install curl for health checks in one RUN command
RUN apt-get update \
    && apt-get install -y --no-install-recommends curl ca-certificates \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

# Copy only published artifacts from builder
COPY --from=builder /app/publish .

# Expose port 8000
EXPOSE 8000

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8000
ENV ASPNETCORE_ENVIRONMENT=Production

# Start application
ENTRYPOINT ["dotnet", "Api.dll"]
