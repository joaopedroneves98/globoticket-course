FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GloboTicket.TicketManagement.Api/GloboTicket.TicketManagement.Api.csproj", "GloboTicket.TicketManagement.Api/"]
RUN dotnet restore "GloboTicket.TicketManagement.Api/GloboTicket.TicketManagement.Api.csproj"
COPY . .
WORKDIR "/src/GloboTicket.TicketManagement.Api"
RUN dotnet build "GloboTicket.TicketManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GloboTicket.TicketManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GloboTicket.TicketManagement.Api.dll"]
