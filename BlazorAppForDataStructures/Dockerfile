# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["BlazorAppForDataStructures/BlazorAppForDataStructures.csproj", "BlazorAppForDataStructures/"]
COPY ["DataStructuresVisualizer/DataStructuresVisualizer.csproj", "DataStructuresVisualizer/"]
RUN dotnet restore "BlazorAppForDataStructures/BlazorAppForDataStructures.csproj"
COPY . .
WORKDIR "/src/BlazorAppForDataStructures"
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

#  Final runtime image: Use Buster-based image with ICU support
FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim

#  Explicitly ensure globalization is enabled (ICU already included)
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "BlazorAppForDataStructures.dll"]



