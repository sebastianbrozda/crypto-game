FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY CoinsService/CoinsService.csproj CoinsService/
COPY CoinsService.Api/CoinsService.Api.csproj CoinsService.Api/
RUN dotnet restore CoinsService/CoinsService.csproj
COPY . .
WORKDIR /src/CoinsService
RUN dotnet build CoinsService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish CoinsService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CoinsService.dll"]
