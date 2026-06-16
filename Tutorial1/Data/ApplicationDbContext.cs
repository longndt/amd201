using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tutorial1.Models;

namespace Tutorial1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Tutorial1.Models.Laptop> Laptop { get; set; }
        public DbSet<Tutorial1.Models.Mobile> Mobile { get; set; }
        public DbSet<Tutorial1.Models.Book> Book { get; set; }
    }
}
