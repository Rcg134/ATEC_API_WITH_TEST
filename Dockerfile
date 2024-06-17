# Use the official .NET 8 SDK image for Windows to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Install ping utility (if not included by default)
RUN apt-get update && apt-get install -y iputils-ping && apt-get install -y telnet

# Copy the solution file and project files
COPY ATEC_API.csproj ./

# Restore dependencies for the solution
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build and publish the application projects separately
RUN dotnet publish ATEC_API.csproj -c Release -o /app/publish

# Use the official .NET 8 ASP.NET Core runtime image for Windows
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the build output from the build stage for ATEC_API
COPY --from=build /app/publish ./

RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /etc/ssl/openssl.cnf
RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /usr/lib/ssl/openssl.cnf
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /usr/lib/ssl/openssl.cnf

# Expose the port that the application will run on
EXPOSE 400

# Set the entry point for the application
ENTRYPOINT ["dotnet", "ATEC_API.dll"]
