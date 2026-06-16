using Lab5.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Lab5.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
