using ATEC_API.Data.Context;
using ATEC_API.Data.IRepositories;
using ATEC_API.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//------------------Service Registration----------------
builder.Services.AddScoped<IHRISRepository, HRISRepository>();
//------------------------------------------------------


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

app.UseAuthorization();

app.MapControllers();

app.Run();
