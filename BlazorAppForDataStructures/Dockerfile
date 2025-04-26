# --------------------
# Build stage
# --------------------
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["BlazorAppForDataStructures/BlazorAppForDataStructures.csproj", "BlazorAppForDataStructures/"]
COPY ["DataStructuresVisualizer/DataStructuresVisualizer.csproj", "DataStructuresVisualizer/"]
RUN dotnet restore "./BlazorAppForDataStructures/BlazorAppForDataStructures.csproj"

COPY . .
WORKDIR "/src/BlazorAppForDataStructures"
RUN dotnet build "BlazorAppForDataStructures.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorAppForDataStructures.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# --------------------
# Runtime stage (Full Debian Bullseye)
# --------------------
FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye

# Install ICU runtime
RUN apt-get update && \
    apt-get install -y --no-install-recommends libicu70 && \
    rm -rf /var/lib/apt/lists/*

# Enable full globalization mode
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "BlazorAppForDataStructures.dll"]
