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
 RUN dotnet publish "./BlazorAppFFROM base AS final
 WORKDIR /app
 COPY --from=publish /app/publish .orDataStructures.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
 ENTRYPOINT ["dotnet", "BlazorAppForDataStructures.dll"]