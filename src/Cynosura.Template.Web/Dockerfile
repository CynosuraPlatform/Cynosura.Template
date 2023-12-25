FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY *.sln .
COPY Cynosura.Template.Core/*.csproj ./Cynosura.Template.Core/
COPY Cynosura.Template.Data/*.csproj ./Cynosura.Template.Data/
COPY Cynosura.Template.Infrastructure/*.csproj ./Cynosura.Template.Infrastructure/
COPY Cynosura.Template.Web/*.csproj ./Cynosura.Template.Web/

RUN dotnet restore Cynosura.Template.Web/Cynosura.Template.Web.csproj

COPY Cynosura.Template.Core/. ./Cynosura.Template.Core/
COPY Cynosura.Template.Data/. ./Cynosura.Template.Data/
COPY Cynosura.Template.Infrastructure/. ./Cynosura.Template.Infrastructure/
COPY Cynosura.Template.Web/. ./Cynosura.Template.Web/

RUN dotnet publish Cynosura.Template.Web/Cynosura.Template.Web.csproj -c Release -o ./out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 5000

ENTRYPOINT ["dotnet", "Cynosura.Template.Web.dll"]