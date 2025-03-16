using Microsoft.EntityFrameworkCore;
using cinemaratona.Data;
using cinemaratona.Repositories;
using cinemaratona.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString is null)
{
    throw new ArgumentNullException("Connection string not found");
}

builder.Services.AddDbContext<CinemaratonaContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<UserRepository>();

builder.Services.AddScoped<UserService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();