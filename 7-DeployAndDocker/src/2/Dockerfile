# Describes how to build a Docker image. Each instruction creates a new layer in the image. 
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

# set up network
EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000

# copy files from current directory to target on container - i.e. if you want your app in the container and want to deploy it
COPY . /app 
WORKDIR /app

#Restore the .NET Core Packages
RUN ["dotnet", "restore"]
#Build the Application
RUN ["dotnet", "build"]

ENTRYPOINT ["dotnet", "run"]
