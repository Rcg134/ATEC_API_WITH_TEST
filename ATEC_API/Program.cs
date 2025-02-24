using ATEC_API.Context;
using ATEC_API.Data.Context;
using ATEC_API.Data.IRepositories;
using ATEC_API.Data.Repositories;
using ATEC_API.Data.Service;
using ATEC_API.ExtentionServices;
using ATEC_API.Filters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IHRISRepository, HRISRepository>();
builder.Services.AddScoped<IDapperConnection, DapperConnection>();
builder.Services.AddScoped<IStagingRepository, StagingRepository>();
builder.Services.AddScoped<ICantierRepository, CantierRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IDownloadRepository, DownloadRepository>();
builder.Services.AddScoped<IElogRepository, ElogRepository>();
builder.Services.AddScoped<ILogSheetRepository, LogSheetRepository>();
builder.Services.AddScoped<DapperModelPagination>();
builder.Services.AddScoped<DownloadService>();
builder.Services.AddSingleton<CacheManagerService>();

builder.Services.ConfigureCorsProd();
builder.Services.ConfigureLogger(builder.Configuration);
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);
builder.Services.ConfigureDatabasesContext(builder.Configuration);
builder.Services.ConfigureHealthCheck();

builder.Services.AddControllers(options =>
{
options.Filters.Add(typeof(ValidateModelAttribute));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
options.User.RequireUniqueEmail = false;
options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
})
.AddDefaultTokenProviders()
.AddEntityFrameworkStores<UserContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "BasicAuthentication";
    options.DefaultChallengeScheme = "BasicAuthentication";
}).AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Ensure this line is present
app.UseAuthorization();  // Ensure this line is present

app.MapHealthChecks("health");
app.UseCors("CorsPolicy");
app.MapControllers();
app.Run();

public partial class Program { }