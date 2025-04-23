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

# Copy the rest of the code
COPY . .

# Build the project
WORKDIR "/src/BlazorAppForDataStructures"
RUN dotnet build "BlazorAppForDataStructures.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "BlazorAppForDataStructures.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# --------------------
# Runtime stage (no ICU required)
# --------------------
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final

# Set working directory and copy build output
WORKDIR /app
COPY --from=publish /app/publish .

# ? Disable globalization to avoid ICU dependency
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true

# Expose port
EXPOSE 80

# Start the app
ENTRYPOINT ["dotnet", "BlazorAppForDataStructures.dll"]

