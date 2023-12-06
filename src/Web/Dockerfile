FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

EXPOSE 80
EXPOSE 443

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ./DesafioEclipseworks.WebAPI.csproj .
RUN dotnet restore
COPY . .

RUN dotnet publish "DesafioEclipseworks.WebAPI.csproj" -c Release -o /publish

FROM base AS runtime

COPY --from=build /publish .

ENTRYPOINT ["dotnet", "DesafioEclipseworks.WebAPI.dll"]