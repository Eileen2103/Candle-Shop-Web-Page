using Microsoft.EntityFrameworkCore;
using backend.Models;
using System.Runtime.CompilerServices;

namespace backend.data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            
            this.Database.ExecuteSqlRaw("PRAGMA foreign_keys=ON;");
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}