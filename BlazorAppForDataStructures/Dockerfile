# Use SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore BlazorAppForDataStructures/BlazorAppForDataStructures.csproj
RUN dotnet publish BlazorAppForDataStructures/BlazorAppForDataStructures.csproj -c Release -o /app/publish

# Use ASP.NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0

#  Install ICU manually
RUN apt-get update && apt-get install -y --no-install-recommends libicu67 && \
    apt-get clean && rm -rf /var/lib/apt/lists/*

#  Set invariant mode to false
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "BlazorAppForDataStructures.dll"]

