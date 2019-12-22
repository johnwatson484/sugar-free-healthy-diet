# BASE
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /app

# DEVELOPMENT
FROM base AS development-env
RUN apt-get update \
 && apt-get install -y --no-install-recommends unzip \
 && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg
COPY *.csproj ./
RUN dotnet restore
COPY . ./
ENTRYPOINT [ "dotnet", "watch", "run", "--urls", "http://0.0.0.0:5000" ]

#PRODUCTION
FROM base AS build-env
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out

# RUNTIME
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS production-env
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SugarFreeHealthyDiet.dll"]
