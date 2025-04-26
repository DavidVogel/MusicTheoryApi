FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV PORT=8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MusicTheory.Api/MusicTheory.Api.csproj", "MusicTheory.Api/"]
COPY ["MusicTheory.Domain/MusicTheory.Domain.csproj", "MusicTheory.Domain/"]
COPY ["MusicTheory.Services/MusicTheory.Services.csproj", "MusicTheory.Services/"]
COPY ["MusicTheory.Data/MusicTheory.Data.csproj", "MusicTheory.Data/"]
RUN dotnet restore "MusicTheory.Api/MusicTheory.Api.csproj"
COPY . .
WORKDIR "/src/MusicTheory.Api"
RUN dotnet build "MusicTheory.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MusicTheory.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV PORT=8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "MusicTheory.Api.dll"]
