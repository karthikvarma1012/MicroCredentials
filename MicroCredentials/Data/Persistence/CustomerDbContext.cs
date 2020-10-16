using MicroCredentials.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroCredentials.Data.Persistence
{
    public class CustomerDbContext : DbContext, ICustomerDbContext
    {
        public CustomerDbContext() { }

        public CustomerDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Quote> Quote { get; set; }
    }
}
