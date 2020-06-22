#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/BCF.MovieRental.Api/BCF.MovieRental.Api.csproj", "src/BCF.MovieRental.Api/"]
COPY ["src/BCF.MovieRental.Business/BCF.MovieRental.Business.csproj", "src/BCF.MovieRental.Business/"]
COPY ["src/BCF.MovieRental.Data/BCF.MovieRental.Data.csproj", "src/BCF.MovieRental.Data/"]
RUN dotnet restore "src/BCF.MovieRental.Api/BCF.MovieRental.Api.csproj"
COPY . .
WORKDIR "/src/src/BCF.MovieRental.Api"
RUN dotnet build "BCF.MovieRental.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BCF.MovieRental.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BCF.MovieRental.Api.dll"]