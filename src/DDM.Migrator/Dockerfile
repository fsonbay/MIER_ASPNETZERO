#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/DDM.Migrator/DDM.Migrator.csproj", "src/DDM.Migrator/"]
COPY ["src/DDM.EntityFrameworkCore/DDM.EntityFrameworkCore.csproj", "src/DDM.EntityFrameworkCore/"]
COPY ["src/DDM.Core/DDM.Core.csproj", "src/DDM.Core/"]
COPY ["src/DDM.Core.Shared/DDM.Core.Shared.csproj", "src/DDM.Core.Shared/"]
RUN dotnet restore "src/DDM.Migrator/DDM.Migrator.csproj"
COPY . .
WORKDIR "/src/src/DDM.Migrator"
RUN dotnet build "DDM.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DDM.Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DDM.Migrator.dll"]
