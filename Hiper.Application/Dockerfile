#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

RUN apt-get update -yq 
RUN apt-get install curl gnupg -yq 
RUN curl -sL https://deb.nodesource.com/setup_12.x | bash -
RUN apt-get install -y nodejs

COPY ["Hiper.Application/Hiper.Application.csproj", "Hiper.Application/"]
COPY ["Hiper.Application.Domain/Hiper.Application.Domain.csproj", "Hiper.Application.Domain/"]
COPY ["Hiper.Application.Data/Hiper.Application.Data.csproj", "Hiper.Application.Data/"]
COPY ["Hiper.Application.Core/Hiper.Application.Core.csproj", "Hiper.Application.Core/"]
COPY ["Hiper.Application.Presentation/Hiper.Application.Presentation.csproj", "Hiper.Application.Presentation/"]
COPY ["Hiper.Application.Data.SqlServer/Hiper.Application.Data.SqlServer.csproj", "Hiper.Application.Data.SqlServer/"]
COPY ["Hiper.Application.Util/Hiper.Util.csproj", "Hiper.Application.Util/"]
RUN dotnet restore "Hiper.Application/Hiper.Application.csproj"
COPY . .
WORKDIR "/src/Hiper.Application"
RUN dotnet build "Hiper.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hiper.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hiper.Application.dll"]
