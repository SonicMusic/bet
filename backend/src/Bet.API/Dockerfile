FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Bet.API/Bet.API.csproj", "src/Bet.API/"]
COPY ["src/Bet.Application/Bet.Application.csproj", "src/Bet.Application/"]
COPY ["src/Bet.Domain/Bet.Domain.csproj", "src/Bet.Domain/"]
COPY ["src/Bet.Infrastructure/Bet.Infrastructure.csproj", "src/Bet.Infrastructure/"]
RUN dotnet restore "src/Bet.API/Bet.API.csproj"
COPY . .
WORKDIR "/src/src/Bet.API"
RUN dotnet build "Bet.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Bet.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bet.API.dll"]
