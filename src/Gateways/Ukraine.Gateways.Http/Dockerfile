FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:6000
EXPOSE 6000

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src

COPY ["src/BuildingBlocks/Ukraine.Domain/Ukraine.Domain.csproj", "src/BuildingBlocks/Ukraine.Domain/"]
COPY ["src/BuildingBlocks/Ukraine.Infrastructure/Ukraine.Infrastructure.csproj", "src/BuildingBlocks/Ukraine.Infrastructure/"]
COPY ["src/BuildingBlocks/Ukraine.Infrastructure.EfCore/Ukraine.Infrastructure.EfCore.csproj", "src/BuildingBlocks/Ukraine.Infrastructure.EfCore/"]
COPY ["src/BuildingBlocks/Ukraine.Infrastructure.Application/Ukraine.Infrastructure.Application.csproj", "src/BuildingBlocks/Ukraine.Infrastructure.Application/"]
COPY ["src/Services/Test/FirstTestApiService/FirstTestApiService.csproj", "src/Services/Test/FirstTestApiService/"]
COPY ["src/Services/Test/SecondTestApiService/SecondTestApiService.csproj", "src/Services/Test/SecondTestApiService/"]
COPY ["src/Web/Ukraine.Web.Status/Ukraine.Web.Status.csproj", "src/Web/Test/Ukraine.Web.Status/"]
COPY ["src/Gateways/Ukraine.Gateways.Http/Ukraine.Gateways.Http.csproj", "src/Gateways/Ukraine.Gateways.Http/"]
COPY ["NuGet.config", "./"]
COPY ["Ukraine.sln", "./"]
RUN dotnet restore "src/Gateways/Ukraine.Gateways.Http/Ukraine.Gateways.Http.csproj"

COPY . .
WORKDIR "/src/src/Gateways/Ukraine.Gateways.Http"
RUN dotnet build "Ukraine.Gateways.Http.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ukraine.Gateways.Http.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ukraine.Gateways.Http.dll"]
