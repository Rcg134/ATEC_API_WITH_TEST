# AR_Tracking_App

 <!-- @format -->

## Package need to install

- Microsoft.EntityFrameworkCore 7.0.14
- Microsoft.EntityFrameWorkCore.SqlServer 7.0.14
- Microsoft.EntityFrameWorkCore.Design 7.0.14
- Microsoft.EntityFrameworkCore.Tools 7.0.14 (for commands)
- AutoMapper.Extensions.Microsoft.DependencyInjection 11.0.0
- BCrypt.Net-Core 1.6.0
- Microsoft.Data.SqlClient 5.2.0

## Script for DB First Migration

```bash
-Scaffold-DbContext "Server=ISPL-DB1;Database=HRIS;User Id=sa;Password=enola845&*;TrustServerCertificate=True;"Microsoft.EntityFrameworkCore.SqlServer -OutputDir ScaffoldContextModel -f
```

## Script for DB First Migration for selected Table PM AND VISUAL STUDIO CODE

```bash
-Scaffold-DbContext "Server=ISPL-DB1;Database=HRIS;User Id=sa;Password=enola845&*;TrustServerCertificate=True;"Microsoft.EntityFrameworkCore.SqlServer -OutputDir ScaffoldContextModel -f
-table tbl_Cert


dotnet ef dbcontext scaffold "Server=ISPL-DB1;Database=HRIS;User Id=sa;Password=enola845&*;TrustServerCertificate=True;"Microsoft.EntityFrameworkCore.SqlServer -t tbl_Cert -o ScaffoldContextModel -f
```

## Securing your Connection String

- include this to appsettings.json

  ### Codedotnet

  ```bash
  "ConnectionStrings": {
  "DefaultConnection": "Server=RUSSELVIEMWAKIN\\AKEMSSQLSERVER;Database=Student;User Id=sa;Password=p@ssw0rd;TrustServerCertificate=True;"
   },
  ```

- in your context inject IConfifuration
  ### Code
        private readonly IConfiguration _configuration;

```bash
          public StudentContext(DbContextOptions<Your Context here> options, IConfiguration configuration)
              : base(options)
          {
              _configuration = configuration;
          }

          #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
           => optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:DefaultConnection"]);
```

## In Program.cs

- add your context before build

### Code

     #### Register DB CONTEXT
     builder.Services.AddDbContext<Your Context>();

     or

     builder.Services.AddDbContext<BookContext>(options =>
      {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
      });

     var app = builder.Build();

# DEVELOPMENT

### Intitial Migration

```bash
 -add-migration <Message>


 -dotnet ef migrations add <InitialMigrationName>
```

### Update Database

```bash
-update-database


-dotnet ef database update
```

### if you want to Roll Back

```bash
-dotnet ef database update <PreviousMigrationName>
```

# Dapper Installation

- Install Dapper from Nuget
- Install SQL Client from Nuget

## In Package Manager

```bash

PM> Install-Package Dapper

```

```bash

PM> Install-Package Microsoft.Data.SqlClient

```

# Adding sln

```bash
 dotnet new sln



 dotnet new sln add ATEC_API.csproj
```

# Publish

```bash

 dotnet publish -o <path> --force


```
