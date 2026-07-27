using Microsoft.EntityFrameworkCore;
using ProductService.Models;

namespace ProductService.Data;

// EF Core context for the Product service. Uses its OWN database (ProductDb).
public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        // Seed a couple of sample rows so the UI isn't empty on first run.
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Wireless Mouse", Description = "2.4GHz ergonomic mouse", Price = 19.99m, Stock = 50, CreatedAt = new DateTime(2024, 1, 1) },
            new Product { Id = 2, Name = "Mechanical Keyboard", Description = "RGB, blue switches", Price = 59.90m, Stock = 30, CreatedAt = new DateTime(2024, 1, 1) }
        );
    }
}
