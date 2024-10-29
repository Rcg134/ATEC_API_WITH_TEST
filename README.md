
# Docker Command

## - Sample Default Image for .NET8

```bash

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

RUN apt-get update && apt-get install -y iputils-ping && apt-get install -y telnet

COPY ATEC_API.csproj ./

RUN dotnet restore

COPY . .

RUN dotnet publish ATEC_API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish ./

RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /etc/ssl/openssl.cnf
RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /usr/lib/ssl/openssl.cnf
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /usr/lib/ssl/openssl.cnf

EXPOSE 400

ENTRYPOINT ["dotnet", "ATEC_API.dll"]

```

## - Creating Image

```bash

docker build  -t api_atec_image .

```

## - Creating Container

```bash

docker run  -p 400:400 api_atec_image

```

# Docker to DockerHub

## - Tag your Image

```bash

docker tag api_atec_image:latest docker.io/rucatzy/api_atec_image:latest


```

## - Push to your Docker Hub

```bash

docker tag api_atec_image:latest docker.io/rucatzy/api_atec_image:latest

(docker.io(default)/(username)/(imageName))

```
