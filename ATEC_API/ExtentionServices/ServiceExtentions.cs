// <copyright file="ServiceExtentions.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

namespace ATEC_API.ExtentionServices
{
    using ATEC_API.Context;
    using ATEC_API.Data.Context;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Serilog;

    public static class ServiceExtentions
    {
      #region CORS
       public static void ConfigureCorsProd(this IServiceCollection services) =>
           services.AddCors(options =>
           {
               options.AddPolicy("CorsPolicy",
                                policy =>
                                {
                                    policy.WithOrigins(
                                        "http://192.168.5.9:400",
                                        "http://prod.atecmes.com:400",
                                        "http://192.168.1.65:500",
                                        "http://192.168.1.65:80",
                                        "http://192.168.1.65",
                                        "http://192.168.1.65:5001")
                                          .AllowAnyHeader()
                                          .AllowAnyMethod()
                                          .AllowCredentials();
                                });
          });

       public static void ConfigureCorsDev(this IServiceCollection services) =>
           services.AddCors(options =>
           {
               options.AddPolicy("CorsPolicy",
                                policy =>
                                {
                                    policy.WithOrigins(
                                        "http://localhost:4200",
                                        "http://localhost:6880",
                                        "https://localhost:7250",
                                        "http://localhost:2711",
                                        "https://localhost:7041",
                                        "http://localhost:5099")
                                          .AllowAnyHeader()
                                          .AllowAnyMethod()
                                          .AllowCredentials();
                                });
           });
        #endregion

      #region Logger
        public static void ConfigureLogger(this IServiceCollection services , IConfiguration configuration) 
        {
           var currentDirectory = Directory.GetCurrentDirectory();
           var logFilePath = configuration["Logging:LogFilePath"];
           var fullLogFilePath = Path.Combine(currentDirectory, logFilePath);

            // Ensure the directory exists
            var logDirectory = Path.GetDirectoryName(fullLogFilePath);
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
                var logger = new LoggerConfiguration()
                  .WriteTo.Console()
                  .WriteTo.File(fullLogFilePath, rollingInterval: RollingInterval.Day)
                  .MinimumLevel
                  .Information()
                  .CreateLogger();
            Log.Logger = logger;
            services.AddSingleton<Serilog.ILogger>(logger);
          
        }
        #endregion

      #region Context
        public static void ConfigureDatabasesContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HrisContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("HRIS_Connection"));
            });
    
            services.AddDbContext<UserContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CentralAccess_Connection"));
            });
        }
       #endregion

      #region HealthCheck
        public static void ConfigureHealthCheck(this IServiceCollection services)
        {
            services.AddHealthChecks().
                    AddCheck<DatabaseHealthCheck>("custom-sql",HealthStatus.Unhealthy);; 
        }
       #endregion

    }
}