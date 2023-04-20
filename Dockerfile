FROM mcr.microsoft.com/dotnet/aspnet:7.0-bullseye-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0-bullseye-slim AS build
WORKDIR /src
COPY ["alternatrr/alternatrr.csproj", "alternatrr/"]
RUN dotnet restore "alternatrr/alternatrr.csproj"
COPY . .
WORKDIR "/src/alternatrr"
RUN dotnet build "alternatrr.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "alternatrr.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "alternatrr.dll"]