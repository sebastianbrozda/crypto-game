FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY UserBalanceService/UserBalanceService.csproj UserBalanceService/
RUN dotnet restore UserBalanceService/UserBalanceService.csproj
COPY . .
WORKDIR /src/UserBalanceService
RUN dotnet build UserBalanceService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish UserBalanceService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UserBalanceService.dll"]
