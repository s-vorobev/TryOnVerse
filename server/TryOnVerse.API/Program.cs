using Microsoft.EntityFrameworkCore;
using TryOnVerse.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Read JWT configuration values (issuer, audience, secret key) from appsettings.json
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["Secret"];


// =======================
// Register Core Services
// =======================

// Enable API controllers
builder.Services.AddControllers();

// Make JSON requests case-insensitive
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

// Configure JWT authentication so the API can validate access tokens
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Define how incoming JWT tokens should be validated
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secretKey!))
    };
});

// Enable role-based authorization with [Authorize]
builder.Services.AddAuthorization();


// =======================
// Database Configuration
// =======================

// Register Entity Framework DbContext and connect it to MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);


// =======================
// API Documentation
// =======================

// Enable Swagger so developers can test the API from the browser
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<JwtService>();

// =======================
// CORS Configuration
// =======================

// Allow requests from the React frontend during development
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod());
});


var app = builder.Build();


// =======================
// Middleware Pipeline
// =======================

// Enable CORS policy for the React frontend
app.UseCors("AllowReact");

// Enable authentication and authorization for incoming requests
app.UseAuthentication();
app.UseAuthorization();

// Redirect HTTP requests to HTTPS
app.UseHttpsRedirection();


// =======================
// Swagger UI
// =======================

// Enable Swagger API documentation and interactive testing UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TryOnVerse API V1");
    c.RoutePrefix = string.Empty; // Serve Swagger at the root URL
});


// Map API controller routes
app.MapControllers();

// Start the web server
app.Run();