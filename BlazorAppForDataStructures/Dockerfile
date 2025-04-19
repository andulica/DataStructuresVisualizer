# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files
COPY ["BlazorAppForDataStructures/BlazorAppForDataStructures.csproj", "BlazorAppForDataStructures/"]
COPY ["DataStructuresVisualizer/DataStructuresVisualizer.csproj", "DataStructuresVisualizer/"]

# Restore dependencies
RUN dotnet restore "./BlazorAppForDataStructures/BlazorAppForDataStructures.csproj"

# Copy the rest of the source
COPY . .

# Build the app
WORKDIR "/src/BlazorAppForDataStructures"
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Install ICU for globalization support
RUN apt-get update && apt-get install -y --no-install-recommends libicu67 && \
    rm -rf /var/lib/apt/lists/*

# Disable invariant mode to enable culture-specific features like en-US
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Set working directory and copy app
WORKDIR /app
COPY --from=publish /app/publish .

# App entry point
ENTRYPOINT ["dotnet", "BlazorAppForDataStructures.dll"]



