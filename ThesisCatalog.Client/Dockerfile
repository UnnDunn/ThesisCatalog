FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ThesisCatalog.Client/ThesisCatalog.Client.csproj", "ThesisCatalog.Client/"]
RUN dotnet restore "ThesisCatalog.Client/ThesisCatalog.Client.csproj"
COPY . .
WORKDIR "/src/ThesisCatalog.Client"
RUN dotnet build "ThesisCatalog.Client.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ThesisCatalog.Client.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Create a non-privileged user that the app will run under.
# See https://docs.docker.com/develop/develop-images/dockerfile_best-practices/#user
ARG UID=10001
RUN adduser \
    --disabled-password \
    --gecos "" \
    --shell "/sbin/nologin" \
    --uid "${UID}" \
    appuser
USER appuser

ENTRYPOINT ["dotnet", "ThesisCatalog.Client.dll"]
