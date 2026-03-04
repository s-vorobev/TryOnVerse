using Microsoft.EntityFrameworkCore;
using TryOnVerse.API.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services
builder.Services.AddControllers(); // Enable API controllers

// 2. Add DbContext for MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// 3. Add Swagger for API documentation and testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 4. Configure CORS to allow requests from React frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
        policy.WithOrigins("http://localhost:5173") // React dev server
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// 5. Use middleware
app.UseCors("AllowReact");          // Enable CORS
app.UseHttpsRedirection();          // Redirect HTTP to HTTPS

// 6. Enable Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TryOnVerse API V1");
    c.RoutePrefix = string.Empty; // Swagger UI at root: http://localhost:<port>/
});

app.MapControllers();              // Map controller routes

app.Run();                          // Start the web server