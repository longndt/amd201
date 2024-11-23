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
        public LaptopServiceContext(DbContextOptions<LaptopServiceContext> options)
            : base(options)
        {
        }

        public DbSet<LaptopService.Models.Laptop> Laptop { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //seed data for Laptop table
            SeedLaptop(builder);
        }

        private void SeedLaptop(ModelBuilder builder)
        {
            builder.Entity<Laptop>().HasData(
                new Laptop { Id = 1, Name = "Dell XPS 13", Color = "White", Quantity = 10, Image = "https://mac24h.vn/images/detailed/94/XPS_9340.jpg" },
                new Laptop { Id = 2, Name = "Dell XPS 15", Color = "Black", Quantity = 15, Image = "https://mac24h.vn/images/detailed/94/XPS_9500.jpg" },
                new Laptop { Id = 3, Name = "Dell Inspiron 14", Color = "Silver", Quantity = 20, Image = "https://mac24h.vn/images/detailed/94/Inspiron_14.jpg" },
                new Laptop { Id = 4, Name = "Dell Latitude 7420", Color = "Gray", Quantity = 8, Image = "https://mac24h.vn/images/detailed/94/Latitude_7420.jpg" },
                new Laptop { Id = 5, Name = "Dell Precision 5550", Color = "Black", Quantity = 12, Image = "https://mac24h.vn/images/detailed/94/Precision_5550.jpg" },
                new Laptop { Id = 6, Name = "Dell Vostro 3400", Color = "White", Quantity = 18, Image = "https://mac24h.vn/images/detailed/94/Vostro_3400.jpg" },
                new Laptop { Id = 7, Name = "Dell G3 15", Color = "Blue", Quantity = 5, Image = "https://mac24h.vn/images/detailed/94/G3_15.jpg" },
                new Laptop { Id = 8, Name = "Dell Alienware m15", Color = "Black", Quantity = 6, Image = "https://mac24h.vn/images/detailed/94/Alienware_m15.jpg" },
                new Laptop { Id = 9, Name = "Dell XPS 17", Color = "White", Quantity = 7, Image = "https://mac24h.vn/images/detailed/94/XPS_9700.jpg" },
                new Laptop { Id = 10, Name = "Dell Inspiron 15", Color = "Silver", Quantity = 11, Image = "https://mac24h.vn/images/detailed/94/Inspiron_15.jpg" },
                new Laptop { Id = 11, Name = "Dell Latitude 5420", Color = "Gray", Quantity = 13, Image = "https://mac24h.vn/images/detailed/94/Latitude_5420.jpg" },
                new Laptop { Id = 12, Name = "Dell Precision 5750", Color = "Black", Quantity = 9, Image = "https://mac24h.vn/images/detailed/94/Precision_5750.jpg" },
                new Laptop { Id = 13, Name = "Dell Vostro 3500", Color = "White", Quantity = 16, Image = "https://mac24h.vn/images/detailed/94/Vostro_3500.jpg" },
                new Laptop { Id = 14, Name = "Dell G5 15", Color = "Blue", Quantity = 4, Image = "https://mac24h.vn/images/detailed/94/G5_15.jpg" },
                new Laptop { Id = 15, Name = "Dell Alienware m17", Color = "Black", Quantity = 3, Image = "https://mac24h.vn/images/detailed/94/Alienware_m17.jpg" },
                new Laptop { Id = 16, Name = "Dell XPS 13 Plus", Color = "White", Quantity = 9, Image = "https://mac24h.vn/images/detailed/94/XPS_13_Plus.jpg" },
                new Laptop { Id = 17, Name = "Dell Inspiron 16", Color = "Silver", Quantity = 10, Image = "https://mac24h.vn/images/detailed/94/Inspiron_16.jpg" },
                new Laptop { Id = 18, Name = "Dell Latitude 7320", Color = "Gray", Quantity = 14, Image = "https://mac24h.vn/images/detailed/94/Latitude_7320.jpg" },
                new Laptop { Id = 19, Name = "Dell Precision 3550", Color = "Black", Quantity = 7, Image = "https://mac24h.vn/images/detailed/94/Precision_3550.jpg" },
                new Laptop { Id = 20, Name = "Dell Vostro 5500", Color = "White", Quantity = 15, Image = "https://mac24h.vn/images/detailed/94/Vostro_5500.jpg" }
            );
        }
    }
}