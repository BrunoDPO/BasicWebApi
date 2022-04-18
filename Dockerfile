ENV PROJECT_NAME="BrunoDPO.BasicAPI.WebApi"

FROM mcr.microsoft.com/dotnet/core/aspnet:6 AS base
WORKDIR /app
EXPOSE 34081
EXPOSE 44350

FROM mcr.microsoft.com/dotnet/core/sdk:6 AS build
WORKDIR /src
COPY src/$PROJECT_NAME/$PROJECT_NAME.csproj $PROJECT_NAME/
RUN dotnet restore $PROJECT_NAME/$PROJECT_NAME.csproj
COPY . .
WORKDIR /src/$PROJECT_NAME/$PROJECT_NAME.csproj
RUN dotnet build $PROJECT_NAME.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish $PROJECT_NAME.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "${PROJECT_NAME}.dll"]
