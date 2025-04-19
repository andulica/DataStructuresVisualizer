# Base runtime image with ASP.NET 6 (Debian-based, not Alpine)
# Use ASP.NET 6 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BlazorAppForDataStructures/BlazorAppForDataStructures.csproj", "BlazorAppForDataStructures/"]
COPY ["DataStructuresVisualizer/DataStructuresVisualizer.csproj", "DataStructuresVisualizer/"]
RUN dotnet restore "./BlazorAppForDataStructures/BlazorAppForDataStructures.csproj"
COPY . .
WORKDIR "/src/BlazorAppForDataStructures"
RUN dotnet build "./BlazorAppForDataStructures.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BlazorAppForDataStructures.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image: base + ICU installation + app copy + globalization enabled
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app

# Install ICU libraries for globalization
RUN apt-get update && apt-get install -y --no-install-recommends libicu67 && \
    rm -rf /var/lib/apt/lists/*

# Set the correct environment flag
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

#  Copy published output
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "BlazorAppForDataStructures.dll"]


