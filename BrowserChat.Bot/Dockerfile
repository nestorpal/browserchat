#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BrowserChat.Bot/BrowserChat.Bot.csproj", "BrowserChat.Bot/"]
COPY ["BrowserChat.Value/BrowserChat.Value.csproj", "BrowserChat.Value/"]
COPY ["BrowserChat.Util/BrowserChat.Util.csproj", "BrowserChat.Util/"]
COPY ["BrowserChat.Entity/BrowserChat.Entity.csproj", "BrowserChat.Entity/"]
RUN dotnet restore "BrowserChat.Bot/BrowserChat.Bot.csproj"
COPY . .
WORKDIR "/src/BrowserChat.Bot"
RUN dotnet build "BrowserChat.Bot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BrowserChat.Bot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BrowserChat.Bot.dll"]