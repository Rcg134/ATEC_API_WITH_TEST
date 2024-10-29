FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

RUN apt-get update && apt-get install -y iputils-ping && apt-get install -y telnet && apt-get install -y net-tools


COPY ATEC_API.csproj ./

RUN dotnet restore

COPY . .

RUN dotnet publish ATEC_API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish ./

RUN sed -i 's/\[openssl_init\]/# [openssl_init]/' /etc/ssl/openssl.cnf


RUN printf "\n\n[openssl_init]\nssl_conf = ssl_sect" >> /etc/ssl/openssl.cnf
RUN printf "\n\n[ssl_sect]\nsystem_default = ssl_default_sect" >> /etc/ssl/openssl.cnf
RUN printf "\n\n[ssl_default_sect]\nMinProtocol = TLSv1\nCipherString = DEFAULT@SECLEVEL=0\n" >> /etc/ssl/openssl.cnf 

#expose

EXPOSE 431

ENTRYPOINT ["dotnet", "ATEC_API.dll"]
