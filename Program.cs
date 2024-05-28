using ATEC_API.Data.Context;
using ATEC_API.Data.IRepositories;
using ATEC_API.Data.Repositories;
using ATEC_API.Filters;
using Microsoft.EntityFrameworkCore;

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
                          //policy.WithOrigins("https://localhost:7041",
                          //                   "http://192.168.5.9:400",
                          //                   "http://prod.atecmes.com:400",
                          //                   "https://localhost:32536")
                          //      .AllowAnyHeader();
                          policy.AllowAnyOrigin()
                                .AllowAnyHeader();
                      });
});
//------------------------------------------------------


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
//---------------------------------------------------------------



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }