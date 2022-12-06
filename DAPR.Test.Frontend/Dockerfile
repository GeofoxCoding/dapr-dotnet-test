#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY *.sln .
COPY ["./DAPR.Test.ApplicationLogic/DAPR.Test.ApplicationLogic.csproj", "DAPR.Test.ApplicationLogic/"]
COPY ["./DAPR.Test.Frontend/DAPR.Test.Frontend.csproj", "DAPR.Test.Frontend/"]
COPY ["./DAPR.Test.API/DAPR.Test.API.csproj", "DAPR.Test.API/"]
COPY ["./docker-compose.dcproj", "./"]
RUN dotnet restore "DAPR.Test.sln"
COPY ["./DAPR.Test.ApplicationLogic/.", "DAPR.Test.ApplicationLogic/"]
COPY ["./DAPR.Test.Frontend/.", "DAPR.Test.Frontend/"]
COPY ["./DAPR.Test.API/.", "DAPR.Test.API/"]
WORKDIR "/src/DAPR.Test.Frontend"
RUN dotnet build "DAPR.Test.Frontend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DAPR.Test.Frontend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DAPR.Test.Frontend.dll"]
