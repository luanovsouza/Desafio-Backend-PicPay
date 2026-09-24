using System.Text.Json.Serialization;
using DesafioBackEndPicpay;
using DesafioBackEndPicpay.Repositories;
using DesafioBackEndPicpay.Repositories.Interfaces;
using DesafioBackEndPicpay.Services;
using DesafioBackEndPicpay.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
//Para poder escrever os enums como string no JSON, ao invés de números inteiros
builder.Services.AddControllers()
    .AddJsonOptions(opt => opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(connectionString);
});

//Repositories
builder.Services.AddScoped(typeof(IRepository<>), (typeof(Repository<>)));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Services
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
}


//app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();