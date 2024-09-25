using Microsoft.EntityFrameworkCore;

namespace MygRPC.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext() { }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
    }
}