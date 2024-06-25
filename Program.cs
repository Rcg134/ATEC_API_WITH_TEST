using ATEC_API.Data.Context;
using ATEC_API.Data.IRepositories;
using ATEC_API.Data.Repositories;
using ATEC_API.Context;
using ATEC_API.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

//------------------Service Registration----------------
builder.Services.AddScoped<IHRISRepository, HRISRepository>();
builder.Services.AddScoped<IDapperConnection, DapperConnection>();
builder.Services.AddScoped<IStagingRepository, StagingRepository>();
builder.Services.AddScoped<ICantierRepository, CantierRepository>();
//------------------------------------------------------

//------------------CORS Registration----------------
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyHeader();
                      });
});
//------------------------------------------------------

//------------------Logger Configuration-----------------
var logger = new LoggerConfiguration()
                          .WriteTo.Console()
                          .WriteTo.File("Logs/ATECAPI.txt", rollingInterval: RollingInterval.Day)
                          .MinimumLevel
                          .Information()
                          .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
//-------------------------------------------------------

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ValidateModelAttribute));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//----------------------Context Connection----------------------
builder.Services.AddDbContext<HrisContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("HRIS_Connection"));
});

builder.Services.AddDbContext<UserContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("CentralAccess_Connection"));
});
//---------------------------------------------------------------


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
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();

// app.Run("http://0.0.0.0:431");

app.Run();

public partial class Program { }
