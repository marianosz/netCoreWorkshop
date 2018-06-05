FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY MyWebAppDeploy/MyWebAppDeploy.csproj MyWebAppDeploy/
RUN dotnet restore MyWebAppDeploy/MyWebAppDeploy.csproj
COPY . .
WORKDIR /src/MyWebAppDeploy
RUN dotnet build MyWebAppDeploy.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MyWebAppDeploy.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MyWebAppDeploy.dll"]
