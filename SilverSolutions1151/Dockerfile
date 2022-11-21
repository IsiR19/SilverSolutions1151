#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SilverSolutions1151/SilverSolutions1151.csproj", "SilverSolutions1151/"]
RUN dotnet restore "SilverSolutions1151/SilverSolutions1151.csproj"
COPY . .
WORKDIR "/src/SilverSolutions1151"
RUN dotnet build "SilverSolutions1151.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SilverSolutions1151.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SilverSolutions1151.dll"]