# ---------- Build stage ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj first and restore (better layer caching).
COPY AuthService.csproj ./
RUN dotnet restore AuthService.csproj

# Copy the rest of the source and publish.
COPY . ./
RUN dotnet publish AuthService.csproj -c Release -o /app/publish /p:UseAppHost=false

# ---------- Runtime stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

# Render (and most PaaS) inject a PORT env var. Bind Kestrel to it.
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "AuthService.dll"]
