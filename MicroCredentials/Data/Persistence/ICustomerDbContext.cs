using MicroCredentials.Data.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace MicroCredentials.Data.Persistence
{
    public interface ICustomerDbContext
    {
        DbSet<Customer> Customers { get; set; }

        DbSet<Quote> Quote { get; set; }

        EntityEntry Entry(object entity);

        int SaveChanges();
    }
}
