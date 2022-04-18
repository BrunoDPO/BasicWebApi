FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publish
COPY *.sln .
COPY src/. ./src
RUN dotnet restore
WORKDIR /src/BrunoDPO.BasicAPI.WebApi
RUN dotnet build -c Release -o /app --no-restore

FROM base AS final
ENV ASPNETCORE_ENVIRONMENT="Development"
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BrunoDPO.BasicAPI.WebApi.dll"]
