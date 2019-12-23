# BASE
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /app

# DEVELOPMENT
FROM base AS development-env
WORKDIR /SugarFreeHealthyDiet
RUN apt-get update \
 && apt-get install -y --no-install-recommends unzip \
 && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg
COPY ./SugarFreeHealthyDiet/*.csproj ./
RUN dotnet restore
COPY ./SugarFreeHealthyDiet ./
ENTRYPOINT [ "dotnet", "watch", "run", "--urls", "http://0.0.0.0:5000" ]

# TEST
FROM development-env AS test-env
WORKDIR /SugarFreeHealthyDiet.Tests
COPY ./SugarFreeHealthyDiet.Tests/*.csproj ./
RUN dotnet restore
COPY ./SugarFreeHealthyDiet.Tests ./
ENTRYPOINT [ "dotnet", "watch", "test" ]

# PRODUCTION
FROM base AS build-env
COPY ./SugarFreeHealthyDiet/*.csproj ./
RUN dotnet restore
COPY ./SugarFreeHealthyDiet ./
RUN dotnet publish -c Release -o out

# RUNTIME
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS production-env
WORKDIR /app
COPY --from=build-env /app/out .
RUN chown -R www-data:www-data /app
USER www-data
ENTRYPOINT ["dotnet", "SugarFreeHealthyDiet.dll"]
