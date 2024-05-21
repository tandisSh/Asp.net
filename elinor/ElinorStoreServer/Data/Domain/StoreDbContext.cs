using ElinorStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ElinorStoreServer.Data.Domain
{
    public class StoreDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Order> Orders { get; set; }
        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            const string ADMIN_ROLE_ID = "a2a2df88-2952-408d-9c34-eca9177d92ac";
            const string ADMIN_ID = "2426167f-842e-4933-ae72-d8dfe34abf78";
            builder.Entity<IdentityRole>().HasData(
                                            new IdentityRole { Id = ADMIN_ROLE_ID, Name = "Admin", NormalizedName = "Admin".ToUpper() },
                                            new IdentityRole { Name = "User", NormalizedName = "User".ToUpper() });

            var hasher = new PasswordHasher<IdentityUser>();
            builder.Entity<AppUser>().HasData(

                new AppUser
                {
                    Id = ADMIN_ID,
                    UserName = "09336540361",
                    NormalizedUserName = "09336540361",
                    Email = "tandis00shojaee@gmail.com",
                    NormalizedEmail = "tandis00shojaee@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin123#"),
                    SecurityStamp = string.Empty,
                    Name = "تندیس ",
                    LastName ="شجاعی پور"
                });

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = ADMIN_ROLE_ID,
                    UserId = ADMIN_ID
                });
        }
    }
}
