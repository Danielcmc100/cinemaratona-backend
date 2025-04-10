# ----------------------------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5199

ENV ASPNETCORE_HTTP_PORTS=5199

USER app
# ----------------------------------------------------------------------------------------------
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Cinemaratona/Cinemaratona.csproj", "./"]
RUN dotnet tool install --global dotnet-ef
COPY . .
WORKDIR "/src/Cinemaratona"
RUN dotnet build "Cinemaratona.csproj" -c $configuration -o /app/build

# ----------------------------------------------------------------------------------------------
FROM build AS publish
ARG configuration=Release
WORKDIR "/src/Cinemaratona"
RUN dotnet publish "Cinemaratona.csproj" -c $configuration -o /app/publish /p:UseAppHost=false


# ----------------------------------------------------------------------------------------------
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cinemaratona.dll"]
