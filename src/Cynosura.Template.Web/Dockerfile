FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build-env
WORKDIR /app

COPY *.sln .
COPY Cynosura.Template.Core/*.csproj ./Cynosura.Template.Core/
COPY Cynosura.Template.Data/*.csproj ./Cynosura.Template.Data/
COPY Cynosura.Template.Web/*.csproj ./Cynosura.Template.Web/

RUN dotnet restore Cynosura.Template.Web/Cynosura.Template.Web.csproj

COPY Cynosura.Template.Core/. ./Cynosura.Template.Core/
COPY Cynosura.Template.Data/. ./Cynosura.Template.Data/
COPY Cynosura.Template.Web/. ./Cynosura.Template.Web/

RUN sed -i "s|c:\\\temp\\\Cynosura.Template\\\|/my/logs/|g" Cynosura.Template.Web/nlog.config
RUN sed -i "s|c:\\\temp\\\|/my/logs/|g" Cynosura.Template.Web/nlog.config

RUN dotnet publish Cynosura.Template.Web/Cynosura.Template.Web.csproj -c Release -o ../out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Cynosura.Template.Web.dll"]