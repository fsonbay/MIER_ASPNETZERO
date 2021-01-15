#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/DDM.Web.Host/DDM.Web.Host.csproj", "src/DDM.Web.Host/"]
COPY ["src/DDM.Web.Core/DDM.Web.Core.csproj", "src/DDM.Web.Core/"]
COPY ["src/DDM.Application/DDM.Application.csproj", "src/DDM.Application/"]
COPY ["src/DDM.Application.Shared/DDM.Application.Shared.csproj", "src/DDM.Application.Shared/"]
COPY ["src/DDM.Core.Shared/DDM.Core.Shared.csproj", "src/DDM.Core.Shared/"]
COPY ["src/DDM.Core/DDM.Core.csproj", "src/DDM.Core/"]
COPY ["src/DDM.EntityFrameworkCore/DDM.EntityFrameworkCore.csproj", "src/DDM.EntityFrameworkCore/"]
COPY ["src/DDM.GraphQL/DDM.GraphQL.csproj", "src/DDM.GraphQL/"]
RUN dotnet restore "src/DDM.Web.Host/DDM.Web.Host.csproj"
COPY . .
WORKDIR "/src/src/DDM.Web.Host"
RUN dotnet build "DDM.Web.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DDM.Web.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DDM.Web.Host.dll"]
