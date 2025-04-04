# ----------------------------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5199

ENV ASPNETCORE_HTTP_PORTS=5199

USER app
# ----------------------------------------------------------------------------------------------
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["cinemaratona.csproj", "./"]
RUN dotnet tool install --global dotnet-ef
COPY . .
WORKDIR "/src/."
RUN dotnet build "cinemaratona.csproj" -c $configuration -o /app/build

# ----------------------------------------------------------------------------------------------
FROM build AS publish
ARG configuration=Release
RUN dotnet publish "cinemaratona.csproj" -c $configuration -o /app/publish /p:UseAppHost=false


# ----------------------------------------------------------------------------------------------
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "cinemaratona.dll"]
