using Microsoft.EntityFrameworkCore;
using TryOnVerse.API.Models;

namespace TryOnVerse.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
}