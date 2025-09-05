using System.Data.Entity;
using Web_HouseSale.Models;

namespace Web_HouseSale.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("AppDb") { }

        public DbSet<Product> Products { get; set; }
    }
}
