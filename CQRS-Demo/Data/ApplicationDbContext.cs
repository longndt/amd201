using Microsoft.EntityFrameworkCore;
using CQRS_Demo.Models;

namespace CQRS_Demo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}

