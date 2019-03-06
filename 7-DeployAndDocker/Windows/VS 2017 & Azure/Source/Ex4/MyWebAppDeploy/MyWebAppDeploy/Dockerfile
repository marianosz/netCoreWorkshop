FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["MyWebAppDeploy/MyWebAppDeploy.csproj", "MyWebAppDeploy/"]
RUN dotnet restore "MyWebAppDeploy/MyWebAppDeploy.csproj"
COPY . .
WORKDIR "/src/MyWebAppDeploy"
RUN dotnet build "MyWebAppDeploy.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MyWebAppDeploy.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MyWebAppDeploy.dll"]