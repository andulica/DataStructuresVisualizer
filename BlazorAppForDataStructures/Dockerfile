# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["BlazorAppForDataStructures/BlazorAppForDataStructures.csproj", "BlazorAppForDataStructures/"]
COPY ["DataStructuresVisualizer/DataStructuresVisualizer.csproj", "DataStructuresVisualizer/"]
RUN dotnet restore "BlazorAppForDataStructures/BlazorAppForDataStructures.csproj"

COPY . .
WORKDIR "/src/BlazorAppForDataStructures"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

#  FINAL RUNTIME IMAGE — use FULL Debian-based runtime that supports ICU!
FROM mcr.microsoft.com/dotnet/aspnet:6.0

#  Enable globalization support
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "BlazorAppForDataStructures.dll"]


