ARG NET_IMAGE=7.0-bullseye-slim
FROM mcr.microsoft.com/dotnet/aspnet:${NET_IMAGE} AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:${NET_IMAGE} AS build
WORKDIR /src

COPY ["src/Framework/Ukraine.Framework.Abstractions/Ukraine.Framework.Abstractions.csproj", "src/Framework/Ukraine.Framework.Abstractions/"]
COPY ["src/Framework/Ukraine.Framework.Core/Ukraine.Framework.Core.csproj", "src/Framework/Ukraine.Framework.Core/"]
COPY ["src/Framework/Ukraine.Framework.Dapr/Ukraine.Framework.Dapr.csproj", "src/Framework/Ukraine.Framework.Dapr/"]
COPY ["src/Framework/Ukraine.Framework.EFCore/Ukraine.Framework.EFCore.csproj", "src/Framework/Ukraine.Framework.EFCore/"]

COPY ["src/Services/Identity/Ukraine.Services.Identity/Ukraine.Services.Identity.csproj", "src/Services/Identity/Ukraine.Services.Identity/"]
COPY ["src/Services/Identity/Ukraine.Services.Identity.Persistence/Ukraine.Services.Identity.Persistence.csproj", "src/Services/Identity/Ukraine.Services.Identity.Persistence/"]

COPY ["NuGet.config", "./"]
COPY ["Directory.Build.props", "./"]

RUN dotnet restore "src/Services/Identity/Ukraine.Services.Identity/Ukraine.Services.Identity.csproj"

COPY . .
WORKDIR "/src/src/Services/Identity/Ukraine.Services.Identity"

FROM build AS publish
RUN dotnet publish --no-restore "Ukraine.Services.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ukraine.Services.Identity.dll"]