FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY *.sln .
COPY Cynosura.Template.Core/*.csproj ./Cynosura.Template.Core/
COPY Cynosura.Template.Data/*.csproj ./Cynosura.Template.Data/
COPY Cynosura.Template.Infrastructure/*.csproj ./Cynosura.Template.Infrastructure/
COPY Cynosura.Template.Worker/*.csproj ./Cynosura.Template.Worker/

RUN dotnet restore Cynosura.Template.Worker/Cynosura.Template.Worker.csproj

COPY Cynosura.Template.Core/. ./Cynosura.Template.Core/
COPY Cynosura.Template.Data/. ./Cynosura.Template.Data/
COPY Cynosura.Template.Infrastructure/. ./Cynosura.Template.Infrastructure/
COPY Cynosura.Template.Worker/. ./Cynosura.Template.Worker/

RUN dotnet publish Cynosura.Template.Worker/Cynosura.Template.Worker.csproj -c Release -o ./out

FROM mcr.microsoft.com/dotnet/runtime:5.0
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "Cynosura.Template.Worker.dll"]