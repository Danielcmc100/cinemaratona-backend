using Microsoft.EntityFrameworkCore;
using cinemaratona.Data;
using cinemaratona.Repositories;
using cinemaratona.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicione esta linha para configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString is null)
{
    throw new ArgumentNullException(connectionString, "Connection string not found");
}

// Register repositories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ReviewRepository>();
builder.Services.AddScoped<EventRepository>();
builder.Services.AddScoped<FriendshipRepository>();

// Register services
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<FriendshipService>();
builder.Services.AddScoped<TokenService>();

// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");

var jwtSigningKey = jwtSettings["Key"];

if (string.IsNullOrEmpty(jwtSigningKey))
{
    throw new InvalidOperationException("JWT Key not configured");
}

var encodedJwtKey = Encoding.UTF8.GetBytes(jwtSigningKey);

void ConfigureJwtBearer(IServiceCollection services, IConfigurationSection jwtSettings, byte[] key)
{
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });
}

void ConfigureDbContext(IServiceCollection services, string connectionString)
{
    services.AddDbContext<CinemaratonaContext>(options =>
        options.UseNpgsql(connectionString));
}

ConfigureJwtBearer(builder.Services, jwtSettings, encodedJwtKey);
ConfigureDbContext(builder.Services, connectionString);

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Adicione esta linha para ativar o CORS
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
