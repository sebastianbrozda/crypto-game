FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY UserOrdersService/UserOrdersService.csproj UserOrdersService/
COPY UserOrdersService.Api/UserOrdersService.Api.csproj UserOrdersService.Api/
RUN dotnet restore UserOrdersService/UserOrdersService.csproj
COPY . .
WORKDIR /src/UserOrdersService
RUN dotnet build UserOrdersService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish UserOrdersService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UserOrdersService.dll"]
