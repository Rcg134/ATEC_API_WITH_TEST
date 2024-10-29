//------------------Service Registration----------------
// <copyright file="Program.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

using ATEC_API.Context;
using ATEC_API.Data.IRepositories;
using ATEC_API.Data.Repositories;
using ATEC_API.Data.Service;
using ATEC_API.ExtentionServices;
using ATEC_API.Filters;
using Microsoft.AspNetCore.Identity;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//------------------Service Registration----------------
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IHRISRepository, HRISRepository>();
builder.Services.AddScoped<IDapperConnection, DapperConnection>();
builder.Services.AddScoped<IStagingRepository, StagingRepository>();
builder.Services.AddScoped<ICantierRepository, CantierRepository>();
//------------------------------------------------------

builder.Services.ConfigureCors();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IDownloadRepository, DownloadRepository>();
builder.Services.AddScoped<DapperModelPagination>();
builder.Services.AddScoped<DownloadService>();
builder.Services.AddSingleton<CacheManagerService>();
//------------------------------------------------------

builder.Services.ConfigureCorsDev();
//builder.Services.ConfigureCorsProd();
builder.Services.ConfigureLogger(builder.Configuration);
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);
builder.Services.ConfigureDatabasesContext(builder.Configuration);
builder.Services.ConfigureHealthCheck();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ValidateModelAttribute));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//----------------------Auth Config----------------------
builder.Services.AddAuthentication();

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                .AddEntityFrameworkStores<UserContext>();
//-------------------------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<IdentityUser>();
app.UseHttpsRedirection();
app.MapHealthChecks("health");
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();

// app.Run("http://0.0.0.0:431");
app.Run();

=======
app.Run();

// User for Integration Testing project
public partial class Program { }
