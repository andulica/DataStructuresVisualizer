# -------- BUILD STAGE --------
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["BlazorAppForDataStructures/BlazorAppForDataStructures.csproj", "BlazorAppForDataStructures/"]
COPY ["DataStructuresVisualizer/DataStructuresVisualizer.csproj", "DataStructuresVisualizer/"]

RUN dotnet restore "BlazorAppForDataStructures/BlazorAppForDataStructures.csproj"
COPY . .

WORKDIR "/src/BlazorAppForDataStructures"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# -------- FINAL RUNTIME STAGE --------
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Install ICU manually
RUN apt-get update && apt-get install -y libicu67 && rm -rf /var/lib/apt/lists/*

# Enable globalization support
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "BlazorAppForDataStructures.dll"]



