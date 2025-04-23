# --------------------
# Build stage
# --------------------
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files
COPY ["BlazorAppForDataStructures/BlazorAppForDataStructures.csproj", "BlazorAppForDataStructures/"]
COPY ["DataStructuresVisualizer/DataStructuresVisualizer.csproj", "DataStructuresVisualizer/"]

# Restore dependencies
RUN dotnet restore "./BlazorAppForDataStructures/BlazorAppForDataStructures.csproj"

# Copy full solution
COPY . .

# Build project
WORKDIR "/src/BlazorAppForDataStructures"
RUN dotnet build "BlazorAppForDataStructures.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish project
FROM build AS publish
RUN dotnet publish "BlazorAppForDataStructures.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# --------------------
# Runtime stage with ICU
# --------------------
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final

# Install ICU
RUN apt-get update && apt-get install -y libicu-dev

# Set working directory and copy build output
WORKDIR /app
COPY --from=publish /app/publish .

# Expose port
EXPOSE 80

# Set env (optional, but you can override in docker run too)
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Start the app
ENTRYPOINT ["dotnet", "BlazorAppForDataStructures.dll"]
