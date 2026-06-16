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
                new Genre { GenreId = 1, GenreName = "Information Technology" },
                new Genre { GenreId = 2, GenreName = "Business" },
                new Genre { GenreId = 3, GenreName = "Science" },
                new Genre { GenreId = 4, GenreName = "Literature" },
                new Genre { GenreId = 5, GenreName = "History" }
            );

            //seed data for table "book" (2 books per genre)
            //Cover images use Open Library's cover-by-ISBN endpoint (real, stable images).
            builder.Entity<Book>().HasData(
              // ----- Information Technology (GenreId = 1) -----
              new Book { BookId = 1, BookTitle = "Clean Code", BookPrice = 45, GenreId = 1, BookImage = "https://covers.openlibrary.org/b/isbn/9780132350884-L.jpg" },
              new Book { BookId = 3, BookTitle = "The Pragmatic Programmer", BookPrice = 50, GenreId = 1, BookImage = "https://covers.openlibrary.org/b/isbn/9780201616224-L.jpg" },

              // ----- Business (GenreId = 2) -----
              new Book { BookId = 2, BookTitle = "The Business Book", BookPrice = 35, GenreId = 2, BookImage = "https://covers.openlibrary.org/b/isbn/9781465415851-L.jpg" },
              new Book { BookId = 4, BookTitle = "Good to Great", BookPrice = 30, GenreId = 2, BookImage = "https://covers.openlibrary.org/b/isbn/9780066620992-L.jpg" },

              // ----- Science (GenreId = 3) -----
              new Book { BookId = 5, BookTitle = "A Brief History of Time", BookPrice = 28, GenreId = 3, BookImage = "https://covers.openlibrary.org/b/isbn/9780553380163-L.jpg" },
              new Book { BookId = 6, BookTitle = "The Selfish Gene", BookPrice = 26, GenreId = 3, BookImage = "https://covers.openlibrary.org/b/isbn/9780199291151-L.jpg" },

              // ----- Literature (GenreId = 4) -----
              new Book { BookId = 7, BookTitle = "To Kill a Mockingbird", BookPrice = 20, GenreId = 4, BookImage = "https://covers.openlibrary.org/b/isbn/9780061120084-L.jpg" },
              new Book { BookId = 8, BookTitle = "Pride and Prejudice", BookPrice = 18, GenreId = 4, BookImage = "https://covers.openlibrary.org/b/isbn/9780141439518-L.jpg" },

              // ----- History (GenreId = 5) -----
              new Book { BookId = 9, BookTitle = "Sapiens: A Brief History of Humankind", BookPrice = 32, GenreId = 5, BookImage = "https://covers.openlibrary.org/b/isbn/9780062316097-L.jpg" },
              new Book { BookId = 10, BookTitle = "Guns, Germs, and Steel", BookPrice = 29, GenreId = 5, BookImage = "https://covers.openlibrary.org/b/isbn/9780393317558-L.jpg" }
          );
        }
    }
}
