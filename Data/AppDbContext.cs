using Simple_Book_Store.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Simple_Book_Store.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                    .Property(x => x.Price)
                    .HasColumnType("decimal(6,2)");

            modelBuilder.Entity<Product>()
                    .Property(x => x.PriceOnSale)
                    .HasColumnType("decimal(6,2)");

            modelBuilder.Entity<Order>()
                    .Property(x => x.OrderTotal)
                    .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<OrderItem>()
                    .Property(x => x.Price)
                    .HasColumnType("decimal(6,2)");

            modelBuilder.Entity<Category>().HasData(

                new Category { CategoryId = 1, CategoryName = "Adventure", Description = "Exciting adventures with historical background" },
                new Category { CategoryId = 2, CategoryName = "Crime", Description = "Dramatic detective stories" },
                new Category { CategoryId = 3, CategoryName = "Fantasy", Description = "Get in the mood for some magic!" },
                new Category { CategoryId = 4, CategoryName = "Horror", Description = "Thrilling horror stories" },
                new Category { CategoryId = 5, CategoryName = "Humour", Description = "Fun, Comic, Farce, all you need is laugh!" },
                new Category { CategoryId = 6, CategoryName = "NonFiction", Description = "Biographies, history and educational stuff" },
                new Category { CategoryId = 7, CategoryName = "Romance", Description = "Sweet sensations and love" },
                new Category { CategoryId = 8, CategoryName = "SciFi", Description = "Science and fiction, a good combo!" },
                new Category { CategoryId = 9, CategoryName = "Western", Description = "Gunsmoke, heroes and villains" }

                );

            modelBuilder.Entity<Product>().HasData(

                new Product
                {
                    ProductId = 1,
                    Title = "Da Vinci koden",
                    Author = "Dan Brown",
                    Description = "A Robert Langdon adventure",
                    CategoryId = 1,
                    Price = 99.95M,
                    PriceOnSale = 89.95M,
                    IsOnSale = false,
                    ImageUrl = "/images/Books/A1.jpg",
                    ImageThumbnailUrl = "/images/Books_small/A1.jpg"
                },
                new Product
                {
                    ProductId = 2,
                    Title = "Röde orm",
                    Author = "Frans G Bengtsson",
                    Description = "Historic viking novel",
                    CategoryId = 1,
                    Price = 77.95M,
                    PriceOnSale = 67.95M,
                    IsOnSale = true,
                    ImageUrl = "/images/Books/A2.jpg",
                    ImageThumbnailUrl = "/images/Books_small/A2.jpg"
                },
                new Product
                {
                    ProductId = 3,
                    Title = "Flicka försvunnen",
                    Author = "Colin Dexter",
                    Description = "A C Dexter crime novel",
                    CategoryId = 2,
                    Price = 77.95M,
                    PriceOnSale = 67.95M,
                    IsOnSale = true,
                    ImageUrl = "/images/Books/C1.jpg",
                    ImageThumbnailUrl = "/images/Books_small/C1.jpg"
                },
                new Product
                {
                    ProductId = 4,
                    Title = "Harry Potter och Dödsrelikerna",
                    Author = "JK Rowling",
                    Description = "JK Rowling magic",
                    CategoryId = 3,
                    Price = 88.95M,
                    PriceOnSale = 78.95M,
                    IsOnSale = false,
                    ImageUrl = "/images/Books/F1.jpg",
                    ImageThumbnailUrl = "/images/Books_small/F1.jpg"
                },
                new Product
                {
                    ProductId = 5,
                    Title = "The Omen",
                    Author = "David Seltzer",
                    Description = "Horror by D Seltzer",
                    CategoryId = 4,
                    Price = 48.95M,
                    PriceOnSale = 38.95M,
                    IsOnSale = false,
                    ImageUrl = "/images/Books/Ho1.jpg",
                    ImageThumbnailUrl = "/images/Books_small/Ho1.jpg"
                },
                new Product
                {
                    ProductId = 6,
                    Title = "The Wilt inheritance",
                    Author = "Tom Sharpe",
                    Description = "Brilliant fun!",
                    CategoryId = 5,
                    Price = 68.95M,
                    PriceOnSale = 59.95M,
                    IsOnSale = false,
                    ImageUrl = "/images/Books/Hu1.jpg",
                    ImageThumbnailUrl = "/images/Books_small/Hu1.jpg"
                },
                new Product
                {
                    ProductId = 7,
                    Title = "Barbarerna",
                    Author = "Terry Jones, Alan Ereira",
                    Description = "Historic facts and myths",
                    CategoryId = 6,
                    Price = 99,
                    PriceOnSale = 89.95M,
                    IsOnSale = false,
                    ImageUrl = "/images/Books/NF1.jpg",
                    ImageThumbnailUrl = "/images/Books_small/NF1.jpg"
                },
                new Product
                {
                    ProductId = 8,
                    Title = "Henrik VIII:s Självbiografi",
                    Author = "Margaret George",
                    Description = "A king's diary",
                    CategoryId = 6,
                    Price = 99.95M,
                    PriceOnSale = 79.95M,
                    IsOnSale = false,
                    ImageUrl = "/images/Books/NF2.jpg",
                    ImageThumbnailUrl = "/images/Books_small/NF2.jpg"
                },
                new Product
                {
                    ProductId = 9,
                    Title = "Skarp programmering med C#",
                    Author = "Jan Skansholm",
                    Description = "A C# programming language tutorial",
                    CategoryId = 6,
                    Price = 42.95M,
                    PriceOnSale = 35.95M,
                    IsOnSale = false,
                    ImageUrl = "/images/Books/NF3.jpg",
                    ImageThumbnailUrl = "/images/Books_small/NF3.jpg"
                },
                new Product
                {
                    ProductId = 10,
                    Title = "Mistrals dotter",
                    Author = "Judith Krantz",
                    Description = "Love and romance",
                    CategoryId = 7,
                    Price = 88.95M,
                    PriceOnSale = 78.95M,
                    IsOnSale = false,
                    ImageUrl = "/images/Books/R1.jpg",
                    ImageThumbnailUrl = "/images/Books_small/R1.jpg"
                },
                new Product
                {
                    ProductId = 11,
                    Title = "The book of Jhereg",
                    Author = "Steven Brust",
                    Description = "The first three Vlad Taltos novels",
                    CategoryId = 8,
                    Price = 99.95M,
                    PriceOnSale = 79.95M,
                    IsOnSale = false,
                    IsProductOfTheWeek = true,
                    ImageUrl = "/images/Books/SF1.jpg",
                    ImageThumbnailUrl = "/images/Books_small/SF1.jpg"
                },
                new Product
                {
                    ProductId = 12,
                    Title = "Bill & Ben - Big Jim",
                    Author = "Marshall Grover (L F Meares)",
                    Description = "Klassiskt möte mellan hjältar!",
                    CategoryId = 9,
                    Price = 39.95M,
                    PriceOnSale = 29.95M,
                    IsOnSale = false,
                    ImageUrl = "/images/Books/W1.jpg",
                    ImageThumbnailUrl = "/images/Books_small/W1.jpg"
                },
                new Product
                {
                    ProductId = 13,
                    Title = "Bill och Ben och Dakota Red",
                    Author = "Marshall Grover (L F Meares)",
                    Description = "Humorfylld western klassiker",
                    CategoryId = 9,
                    Price = 39.95M,
                    PriceOnSale = 29.95M,
                    IsOnSale = false,
                    ImageUrl = "/images/Books/W2.jpg",
                    ImageThumbnailUrl = "/images/Books_small/W2.jpg"
                }

                );

            string customerId = Guid.NewGuid().ToString();
            string adminId = Guid.NewGuid().ToString();

            string customerRoleId = Guid.NewGuid().ToString();
            string adminRoleId = Guid.NewGuid().ToString();

            // Seed admin and customer roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Id = customerRoleId,
                    Name = "Customer",
                    NormalizedName = "CUSTOMER",
                    ConcurrencyStamp = customerRoleId
                });

            // Create users, one for each role to start with
            var adminUser = new ApplicationUser
            {
                // From Identity User class
                Id = adminId,
                Email = "root@superuser.com",
                NormalizedEmail = "ROOT@SUPERUSER.COM",
                EmailConfirmed = true,
                UserName = "root@superuser.com",
                NormalizedUserName = "ROOT@SUPERUSER.COM",
                // From extended class ApplicationUser
                FirstName = "Admin",
                LastName = "Adminsson",
                AddressLine1 = "Adminsgatan 42",
                AddressLine2 = "Plan 9 (from outer space)",
                ZipCode = "12345",
                City = "Adminköping",
                Country = "Adminarnia"
            };

            // Set user password (hard coded exception)
            PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            adminUser.PasswordHash = ph.HashPassword(adminUser, "HardCoded_AA42");

            var customerUser = new ApplicationUser
            {

                // From IdentityUser class
                Id = customerId,
                Email = "vlad@taltos.com",
                NormalizedEmail = "VLAD@TALTOS.COM",
                EmailConfirmed = true,
                UserName = "vlad@taltos.com",
                NormalizedUserName = "VLAD@TALTOS.COM",
                // From extended class ApplicationUser
                FirstName = "Vlad",
                LastName = "Taltos",
                AddressLine1 = "Black Castle",
                AddressLine2 = "(somewhere in the clouds)",
                ZipCode = "42222",
                City = "Adrilankha",
                Country = "Dragaera"
            };
            // Set user password (hard coded exception)
            PasswordHasher<ApplicationUser> ph2 = new PasswordHasher<ApplicationUser>();
            customerUser.PasswordHash = ph2.HashPassword(customerUser, "HardCoded_VT42");

            // Seed users
            modelBuilder.Entity<ApplicationUser>()
                .HasData(adminUser, customerUser);

            // Set user roles (join/assoc table entries)
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = customerRoleId,
                    UserId = customerId
                });
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
