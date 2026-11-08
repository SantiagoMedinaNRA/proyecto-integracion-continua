# Stage 1: Builder
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS builder

WORKDIR /src

# Copy project files from parent directory context
COPY Web/Web.Blazor.csproj ./Web/
COPY Shared/Shared.csproj ./Shared/

# Restore dependencies
RUN dotnet restore "Web/Web.Blazor.csproj"

# Copy entire source
COPY Web/ ./Web/
COPY Shared/ ./Shared/

# Build in Release mode
RUN dotnet build "Web/Web.Blazor.csproj" -c Release -o /app/build

# Publish
RUN dotnet publish "Web/Web.Blazor.csproj" -c Release -o /app/publish

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
ENTRYPOINT ["dotnet", "Web.Blazor.dll"]
