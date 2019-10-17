FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

COPY *.sln .
COPY Cynosura.Template.Core/*.csproj ./Cynosura.Template.Core/
COPY Cynosura.Template.Data/*.csproj ./Cynosura.Template.Data/
COPY Cynosura.Template.Worker/*.csproj ./Cynosura.Template.Worker/

RUN dotnet restore Cynosura.Template.Worker/Cynosura.Template.Worker.csproj

COPY Cynosura.Template.Core/. ./Cynosura.Template.Core/
COPY Cynosura.Template.Data/. ./Cynosura.Template.Data/
COPY Cynosura.Template.Worker/. ./Cynosura.Template.Worker/

RUN sed -i "s|c:\\\temp\\\Cynosura.Template\\\|/my/logs/|g" Cynosura.Template.Worker/nlog.config
RUN sed -i "s|c:\\\temp\\\|/my/logs/|g" Cynosura.Template.Worker/nlog.config

RUN dotnet publish Cynosura.Template.Worker/Cynosura.Template.Worker.csproj -c Release -o ../out

FROM mcr.microsoft.com/dotnet/core/runtime:3.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Cynosura.Template.Worker.dll"]