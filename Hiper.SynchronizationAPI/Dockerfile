#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Hiper.SynchronizationAPI/Hiper.SynchronizationAPI.csproj", "Hiper.SynchronizationAPI/"]
COPY ["Hiper.SynchronizationAPI.Data.SqlServer/Hiper.SynchronizationAPI.Data.SqlServer.csproj", "Hiper.SynchronizationAPI.Data.SqlServer/"]
COPY ["Hiper.SynchronizationAPI.Core/Hiper.SynchronizationAPI.Core.csproj", "Hiper.SynchronizationAPI.Core/"]
COPY ["Hiper.SynchronizationAPI.Data/Hiper.SynchronizationAPI.Data.csproj", "Hiper.SynchronizationAPI.Data/"]
COPY ["Hiper.Application.Util/Hiper.Util.csproj", "Hiper.Application.Util/"]
COPY ["Hiper.SynchronizationAPI.Domain/Hiper.SynchronizationAPI.Domain.csproj", "Hiper.SynchronizationAPI.Domain/"]
COPY ["Hiper.SynchronizationAPI.Presentation/Hiper.SynchronizationAPI.Presentation.csproj", "Hiper.SynchronizationAPI.Presentation/"]
RUN dotnet restore "Hiper.SynchronizationAPI/Hiper.SynchronizationAPI.csproj"
COPY . .
WORKDIR "/src/Hiper.SynchronizationAPI"
RUN dotnet build "Hiper.SynchronizationAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hiper.SynchronizationAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hiper.SynchronizationAPI.dll"]
