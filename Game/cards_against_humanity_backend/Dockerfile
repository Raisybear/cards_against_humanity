# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /Source
COPY . .
RUN dotnet restore "./cards_against_humanity_backend.csproj" --disable-parallel
RUN dotnet build "./cards_against_humanity_backend.csproj" -c Release -o /app --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "cards_against_humanity_backend.dll"]
