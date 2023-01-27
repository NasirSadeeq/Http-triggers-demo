using HttpTrigger.Db_Context_class;
using HttpTrigger.Interfaces;
using HttpTrigger.Models;
using HttpTrigger.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<EmployeeDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
//builder.Services.AddScoped<ILogger, ILogger>();
builder.Services.AddScoped<Iemployee,EmployeeRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
