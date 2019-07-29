FROM microsoft/dotnet:2.2-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY OrderMatchingEngine/OrderMatchingEngine.csproj OrderMatchingEngine/
COPY UserOrdersService.Api/UserOrdersService.Api.csproj UserOrdersService.Api/
RUN dotnet restore OrderMatchingEngine/OrderMatchingEngine.csproj
COPY . .
WORKDIR /src/OrderMatchingEngine
RUN dotnet build OrderMatchingEngine.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish OrderMatchingEngine.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OrderMatchingEngine.dll"]
