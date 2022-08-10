FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:6040
EXPOSE 6040

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src

COPY ["src/BuildingBlocks/Ukraine.Domain/Ukraine.Domain.csproj", "src/BuildingBlocks/Ukraine.Domain/"]
COPY ["src/BuildingBlocks/Ukraine.Infrastructure/Ukraine.Infrastructure.csproj", "src/BuildingBlocks/Ukraine.Infrastructure/"]
COPY ["src/BuildingBlocks/Ukraine.Infrastructure.EfCore/Ukraine.Infrastructure.EfCore.csproj", "src/BuildingBlocks/Ukraine.Infrastructure.EfCore/"]
COPY ["src/BuildingBlocks/Ukraine.Infrastructure.Application/Ukraine.Infrastructure.Application.csproj", "src/BuildingBlocks/Ukraine.Infrastructure.Application/"]
COPY ["src/Services/Test/FirstTestApiService/FirstTestApiService.csproj", "src/Services/Test/FirstTestApiService/"]
COPY ["src/Services/Test/SecondTestApiService/SecondTestApiService.csproj", "src/Services/Test/SecondTestApiService/"]
COPY ["src/Web/Ukraine.Web.Status/Ukraine.Web.Status.csproj", "src/Web/Ukraine.Web.Status/"]
COPY ["NuGet.config", "./"]
COPY ["Ukraine.sln", "./"]
RUN dotnet restore "src/Web/Ukraine.Web.Status/Ukraine.Web.Status.csproj"

COPY . .
WORKDIR "/src/src/Web/Ukraine.Web.Status"
RUN dotnet build "Ukraine.Web.Status.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ukraine.Web.Status.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ukraine.Web.Status.dll"]
