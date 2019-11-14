FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Api/*.csproj ./Api/
RUN dotnet restore

# copy everything else and build app
COPY Api/. ./Api/
WORKDIR /app/Api
RUN dotnet publish -c Release -r linux-musl-x64 -o out --self-contained

FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app/Api/out ./   
ENTRYPOINT ["./Api"]
