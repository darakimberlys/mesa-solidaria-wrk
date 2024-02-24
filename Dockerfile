FROM mcr.microsoft.com/dotnet/sdk:6.0 AS builder
WORKDIR /app

# caches restore result by copying csproj file separately

WORKDIR /src/MesaSolidariaWrk
COPY . .
RUN dotnet restore "MesaSolidariaWrk/MesaSolidariaWrk.csproj"
RUN dotnet build "MesaSolidariaWrk/MesaSolidariaWrk.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MesaSolidariaWrk/MesaSolidariaWrk.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MesaSolidariaWrk.dll"]

ENV PORT 5000
EXPOSE 5000