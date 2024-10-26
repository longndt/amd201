using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MVC.Models;
using Microsoft.AspNetCore.Identity;

namespace MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MVC.Models.Genre> Genre { get; set; }
        public DbSet<MVC.Models.Book> Book { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //seed users & roles to DB
            //create users
            var adminAccount = new IdentityUser
            {
                Id = "admin",
                UserName = "admin@fpt.edu.vn",
                Email = "admin@fpt.edu.vn",
                NormalizedUserName = "ADMIN@FPT.EDU.VN",
                NormalizedEmail = "ADMIN@FPT.EDU.VN",
                EmailConfirmed = true
            };
            var customerAccount = new IdentityUser
            {
                Id = "customer",
                UserName = "customer@fpt.edu.vn",
                Email = "customer@fpt.edu.vn",
                NormalizedUserName = "CUSTOMER@FPT.EDU.VN",
                NormalizedEmail = "CUSTOMER@FPT.EDU.VN",
                EmailConfirmed = true
            };
            //encrypt password
            var hasher = new PasswordHasher<IdentityUser>();
            adminAccount.PasswordHash = hasher.HashPassword(adminAccount, "123456");
            customerAccount.PasswordHash = hasher.HashPassword(customerAccount, "123456");
            //add users to DB
            builder.Entity<IdentityUser>().HasData(adminAccount, customerAccount);
            //create roles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "adminRole",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "customerRole",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                }
                );
            //assign role to account
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "admin",
                    RoleId = "adminRole"
                },
                 new IdentityUserRole<string>
                 {
                     UserId = "customer",
                     RoleId = "customerRole"
                 }
                );

            //seed data for table "genre"
            builder.Entity<Genre>().HasData(
                new Genre
                {
                    GenreId = 1,
                    GenreName = "Information Technology"
                },
                new Genre
                {
                    GenreId = 2,
                    GenreName = "Business"
                }
            );

            //seed data for table "book"
            builder.Entity<Book>().HasData(
              new Book
              {
                  BookId = 1,
                  BookTitle = "Clean Code",
                  BookPrice = 99,
                  BookImage = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQCVxCsrGVMj5TB_AxYw2pZ3oAgQ1DzA62P-g&s",
                  GenreId = 1
              },
              new Book
              {
                  BookId = 2,
                  BookTitle = "The Business Book",
                  BookPrice = 88,
                  BookImage = "https://salt.tikicdn.com/cache/w1200/ts/product/28/5d/6a/3f2c0fc6b18f65567b8ad08604d4423a.png",
                  GenreId = 2
              }
          );
        }
    }
}
