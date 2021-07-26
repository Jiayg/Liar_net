#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/Liar.HttpApi.Host/Liar.HttpApi.Host.csproj", "src/Liar.HttpApi.Host/"]
COPY ["src/Liar.EntityFrameworkCore.DbMigrations/Liar.EntityFrameworkCore.DbMigrations.csproj", "src/Liar.EntityFrameworkCore.DbMigrations/"]
COPY ["src/Liar.EntityFrameworkCore/Liar.EntityFrameworkCore.csproj", "src/Liar.EntityFrameworkCore/"]
COPY ["src/Liar.Domain/Liar.Domain.csproj", "src/Liar.Domain/"]
COPY ["src/Liar.Domain.Shared/Liar.Domain.Shared.csproj", "src/Liar.Domain.Shared/"]
COPY ["src/Liar.HttpApi/Liar.HttpApi.csproj", "src/Liar.HttpApi/"]
COPY ["src/Liar.Application.Contracts/Liar.Application.Contracts.csproj", "src/Liar.Application.Contracts/"]
COPY ["src/Liar.Application/Liar.Application.csproj", "src/Liar.Application/"]
RUN dotnet restore "src/Liar.HttpApi.Host/Liar.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/src/Liar.HttpApi.Host"
RUN dotnet build "Liar.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Liar.HttpApi.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Liar.HttpApi.Host.dll"]