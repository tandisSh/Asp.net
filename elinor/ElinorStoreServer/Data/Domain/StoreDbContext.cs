using ElinorStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace ElinorStoreServer.Data.Domain
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categorys { get; set; }
        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
        }
    }
}
