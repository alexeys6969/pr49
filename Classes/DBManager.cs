using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using REST_API_Shashin.Models;

namespace REST_API_Shashin.Classes
{
    public class DBManager : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Dishes> Dishes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DBManager() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=127.0.0.1;uid=root;pwd=;database=pr49", new MySqlServerVersion(new Version(8, 0, 11)));
        }
    }
}
