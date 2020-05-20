FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Yaroshinski.Bot/Yaroshinski.Bot.csproj", "Yaroshinski.Bot/"]
COPY ["Yaroshinski.Core/Yaroshinski.Core.csproj", "Yaroshinski.Core/"]
RUN dotnet restore "Yaroshinski.Bot/Yaroshinski.Bot.csproj"
COPY . .
WORKDIR "/src/Yaroshinski.Bot"
RUN dotnet build "Yaroshinski.Bot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Yaroshinski.Bot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Yaroshinski.Bot.dll