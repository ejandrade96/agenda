FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /agenda-api

COPY *.sln .
COPY Api/*.csproj ./Api/
COPY Dominio/*.csproj ./Dominio/
COPY Infraestrutura/*.csproj ./Infraestrutura/
COPY Servicos/*.csproj ./Servicos/
COPY Tests/Integracao/*.csproj ./Tests/Integracao/
COPY Tests/Unidade/*.csproj ./Tests/Unidade/

RUN dotnet restore

COPY Api/. ./Api/
COPY Dominio/. ./Dominio/
COPY Infraestrutura/. ./Infraestrutura/
COPY Servicos/. ./Servicos/

WORKDIR /agenda-api/Api
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /agenda-api
COPY --from=build-env /agenda-api/Api/out ./
ENTRYPOINT ["dotnet", "Api.dll"]
