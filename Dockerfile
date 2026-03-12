FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["SmartTaskManager/SmartTaskManager.csproj", "SmartTaskManager/"]
COPY ["SmartTaskManagerCore/SmartTaskManager.Core.csproj", "SmartTaskManagerCore/"]
COPY ["SmartTaskManagerData/SmartTaskManager.Infrastructure.csproj", "SmartTaskManagerData/"]
RUN dotnet restore "SmartTaskManager/SmartTaskManager.csproj"
COPY . .
WORKDIR "/src/SmartTaskManager"
RUN dotnet build "SmartTaskManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SmartTaskManager.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartTaskManager.dll"]
