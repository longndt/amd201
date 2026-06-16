using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LaptopService.Models;

namespace LaptopService.Data
{
    public class LaptopServiceContext : DbContext
    {
        public LaptopServiceContext (DbContextOptions<LaptopServiceContext> options)
            : base(options)
        {
        }

        public DbSet<LaptopService.Models.Laptop> Laptop { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed data for table Laptop
            SeedLaptop(builder);
        }

        private void SeedLaptop(ModelBuilder builder)
        {
            builder.Entity<Laptop>().HasData(
                new Laptop { Id = 1, Brand = "Apple", Model = "Macbook Pro M2", Quantity = 10, Price = 2345 },
                new Laptop { Id = 2, Brand = "Dell", Model = "XPS 15", Quantity = 15, Price = 1999 },
                new Laptop { Id = 3, Brand = "LG", Model = "Gram 17", Quantity = 22, Price = 2024 },
                new Laptop { Id = 4, Brand = "HP", Model = "Spectre x360", Quantity = 8, Price = 1799 },
                new Laptop { Id = 5, Brand = "Asus", Model = "ZenBook 14", Quantity = 18, Price = 1499 },
                new Laptop { Id = 6, Brand = "Lenovo", Model = "ThinkPad X1 Carbon", Quantity = 12, Price = 1999 },
                new Laptop { Id = 7, Brand = "Acer", Model = "Aspire 5", Quantity = 25, Price = 799 },
                new Laptop { Id = 8, Brand = "Microsoft", Model = "Surface Laptop 4", Quantity = 20, Price = 1699 },
                new Laptop { Id = 9, Brand = "Razer", Model = "Blade 15", Quantity = 10, Price = 2499 },
                new Laptop { Id = 10, Brand = "MSI", Model = "GE66 Raider", Quantity = 5, Price = 2299 },
                new Laptop { Id = 11, Brand = "Samsung", Model = "Galaxy Book Pro", Quantity = 16, Price = 1799 },
                new Laptop { Id = 12, Brand = "Google", Model = "Pixelbook Go", Quantity = 14, Price = 999 },
                new Laptop { Id = 13, Brand = "Acer", Model = "Chromebook 14", Quantity = 30, Price = 599 },
                new Laptop { Id = 14, Brand = "HP", Model = "Omen 15", Quantity = 8, Price = 1899 },
                new Laptop { Id = 15, Brand = "Alienware", Model = "m15 R4", Quantity = 6, Price = 2299 },
                new Laptop { Id = 16, Brand = "Dell", Model = "Inspiron 15", Quantity = 25, Price = 799 },
                new Laptop { Id = 17, Brand = "Apple", Model = "MacBook Air", Quantity = 12, Price = 999 },
                new Laptop { Id = 18, Brand = "Lenovo", Model = "Legion 5", Quantity = 9, Price = 1299 },
                new Laptop { Id = 19, Brand = "Huawei", Model = "MateBook X Pro", Quantity = 7, Price = 1799 },
                new Laptop { Id = 20, Brand = "Microsoft", Model = "Surface Book 3", Quantity = 11, Price = 1599 }
            );
        }

    }
}
