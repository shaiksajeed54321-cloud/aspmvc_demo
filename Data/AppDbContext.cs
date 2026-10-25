using Microsoft.EntityFrameworkCore;
using SajidMvc.Models;

namespace SajidMvc.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed data (fixed dates so the model stays stable)
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Laptop",   Description = "14-inch ultrabook",   Price = 899.99m, Stock = 12,  CreatedAt = new DateTime(2026, 1, 1) },
            new Product { Id = 2, Name = "Keyboard", Description = "Mechanical keyboard", Price = 59.50m,  Stock = 40,  CreatedAt = new DateTime(2026, 1, 1) },
            new Product { Id = 3, Name = "Mouse",    Description = "Wireless mouse",      Price = 24.00m,  Stock = 75,  CreatedAt = new DateTime(2026, 1, 1) }
        );
    }
}
