FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["XoDotNet.Main/XoDotNet.Main.csproj", "XoDotNet.Main/"]
COPY ["XoDotNet.Mediator/XoDotNet.Mediator.csproj", "XoDotNet.Mediator/"]
COPY ["XoDotNet.Features/XoDotNet.Features.csproj", "XoDotNet.Features/"]
COPY ["XoDotNet.Infrastructure/XoDotNet.Infrastructure.csproj", "XoDotNet.Infrastructure/"]
COPY ["XoDotNet.Domain/XoDotNet.Domain.csproj", "XoDotNet.Domain/"]
COPY ["XoDotNet.GameEvents/XoDotNet.GameEvents.csproj", "XoDotNet.GameEvents/"]
COPY ["XoDotNet.DataAccess/XoDotNet.DataAccess.csproj", "XoDotNet.DataAccess/"]
RUN dotnet restore "XoDotNet.Main/XoDotNet.Main.csproj"
COPY . .
WORKDIR "/src/XoDotNet.Main"
RUN dotnet build "XoDotNet.Main.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "XoDotNet.Main.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XoDotNet.Main.dll"]
