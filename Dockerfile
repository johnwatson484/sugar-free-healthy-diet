# Development
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS development

RUN apk update \
  && apk --no-cache add curl procps unzip \
  && wget -qO- https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l /vsdbg

RUN addgroup -g 1000 dotnet \
    && adduser -u 1000 -G dotnet -s /bin/sh -D dotnet

USER dotnet
WORKDIR /home/dotnet

RUN mkdir -p /home/dotnet/SugarFreeHealthyDiet/ /home/dotnet/SugarFreeHealthyDiet.Tests/
COPY --chown=dotnet:dotnet ./SugarFreeHealthyDiet.Tests/*.csproj ./SugarFreeHealthyDiet.Tests/
RUN dotnet restore ./SugarFreeHealthyDiet.Tests/SugarFreeHealthyDiet.Tests.csproj
COPY --chown=dotnet:dotnet ./SugarFreeHealthyDiet/*.csproj ./SugarFreeHealthyDiet/
RUN dotnet restore ./SugarFreeHealthyDiet/SugarFreeHealthyDiet.csproj
COPY --chown=dotnet:dotnet ./SugarFreeHealthyDiet.Tests/ ./SugarFreeHealthyDiet.Tests/
RUN true
COPY --chown=dotnet:dotnet ./SugarFreeHealthyDiet/ ./SugarFreeHealthyDiet/
RUN dotnet publish ./SugarFreeHealthyDiet/ -c Release -o /home/dotnet/out

ARG PORT=5000
ENV PORT ${PORT}
ENV ASPNETCORE_ENVIRONMENT development
ENV ASPNETCORE_URLS http://*:5000
EXPOSE ${PORT}
# Override entrypoint using shell form so that environment variables are picked up
ENTRYPOINT dotnet watch --project ./SugarFreeHealthyDiet run

# Production
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS production

RUN addgroup -g 1000 dotnet \
    && adduser -u 1000 -G dotnet -s /bin/sh -D dotnet

USER dotnet
WORKDIR /home/dotnet

COPY --from=development /home/dotnet/out/ ./
ARG PORT=5000
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT production
EXPOSE ${PORT}
# Override entrypoint using shell form so that environment variables are picked up
ENTRYPOINT dotnet SugarFreeHealthyDiet.dll
