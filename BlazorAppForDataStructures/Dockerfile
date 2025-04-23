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
# Runtime stage
# --------------------
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final

#  Install correct ICU version
RUN apt-get update && apt-get install -y libicu70

WORKDIR /app
COPY --from=publish /app/publish .

#  Ensure ICU is required
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

EXPOSE 80
ENTRYPOINT ["dotnet", "BlazorAppForDataStructures.dll"]

