FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the csproj file and restore the project dependencies 
COPY game-vision-web-api.csproj ./
RUN dotnet restore "game-vision-web-api.csproj"

# Copy everything
COPY . ./
# Build and publish a release
RUN dotnet publish "game-vision-web-api.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "game-vision-web-api.dll"]