#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BrowserChat.Client/BrowserChat.Client.csproj", "BrowserChat.Client/"]
COPY ["BrowserChat.Entity/BrowserChat.Entity.csproj", "BrowserChat.Entity/"]
COPY ["BrowserChat.Value/BrowserChat.Value.csproj", "BrowserChat.Value/"]
RUN dotnet restore "BrowserChat.Client/BrowserChat.Client.csproj"
COPY . .
WORKDIR "/src/BrowserChat.Client"
RUN dotnet build "BrowserChat.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BrowserChat.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BrowserChat.Client.dll"]