#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
RUN curl -L https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh | sh
COPY ["./Nuget.Pipeline.Config", "."]
COPY ["./Directory.Build.props","."]
COPY ["src/TripArc.Case.API/TripArc.Case.Api.csproj", "src/TripArc.Case.Api/"]
COPY ["src/TripArc.Case.Data/TripArc.Case.Data.csproj", "src/TripArc.Case.Data/"]
COPY ["src/TripArc.Case.Domain/TripArc.Case.Domain.csproj", "src/TripArc.Case.Domain/"]
COPY ["src/TripArc.Case.Shared/TripArc.Case.Shared.csproj", "src/TripArc.Case.Shared/"]
ARG FEED_ACCESSTOKEN
ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS="{\"endpointCredentials\": [{\"endpoint\":\"https://pkgs.dev.azure.com/trip-arc/_packaging/trip-arc/nuget/v3/index.json\", \"username\":\"docker\", \"password\":\"$FEED_ACCESSTOKEN\"}]}"
RUN dotnet restore "src/TripArc.Case.Api/TripArc.Case.Api.csproj" --configfile "Nuget.Pipeline.Config"
COPY . .
WORKDIR "/src/src/TripArc.Case.Api"
RUN dotnet build "TripArc.Case.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TripArc.Case.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TripArc.Case.Api.dll"]