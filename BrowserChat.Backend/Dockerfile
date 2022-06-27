#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BrowserChat.Backend/BrowserChat.Backend.csproj", "BrowserChat.Backend/"]
COPY ["BrowserChat.Value/BrowserChat.Value.csproj", "BrowserChat.Value/"]
COPY ["BrowserChat.Util/BrowserChat.Util.csproj", "BrowserChat.Util/"]
COPY ["BrowserChat.Entity/BrowserChat.Entity.csproj", "BrowserChat.Entity/"]
RUN dotnet restore "BrowserChat.Backend/BrowserChat.Backend.csproj"
COPY . .
WORKDIR "/src/BrowserChat.Backend"
RUN dotnet build "BrowserChat.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BrowserChat.Backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BrowserChat.Backend.dll"]