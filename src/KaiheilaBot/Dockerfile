FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/KaiheilaBot/KaiheilaBot.csproj", "KaiheilaBot/"]
RUN dotnet restore "src/KaiheilaBot/KaiheilaBot.csproj"
COPY . .
WORKDIR "/src/KaiheilaBot"
RUN dotnet build "KaiheilaBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KaiheilaBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KaiheilaBot.dll"]
