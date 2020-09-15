FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

ENV ASPNETCORE_URLS http://+:80
ENV ASPNETCORE_ENVIRONMENT production

WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]
