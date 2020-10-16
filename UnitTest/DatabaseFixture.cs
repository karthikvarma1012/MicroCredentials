using MicroCredentials.Data.Models;
using MicroCredentials.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace test
{
    public class DatabaseFixture : IDisposable
    {
        private IEnumerable<Customer> Customers { get; set; }
        public ICustomerDbContext dbcontext;

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseInMemoryDatabase(databaseName: "CustomerDbNew")
                .Options;
            dbcontext = new CustomerDbContext(options);

            dbcontext.Customers.Add(new Customer { Name = "karthik110", Username = "kvarma110", Password = "Tester99", Address = null, State = null, Country = null, EmailAddress = null, PAN = null, ContactNumber = null, DOB = Convert.ToDateTime("2000-12-31T23:59:59"), AccountType = null });
            dbcontext.Customers.Add(new Customer { Name = "karthik102", Username = "kvarma102", Password = "Tester99", Address = null, State = null, Country = null, EmailAddress = null, PAN = null, ContactNumber = null, DOB = Convert.ToDateTime("2000-12-31T23:59:59"), AccountType = null });
            dbcontext.Customers.Add(new Customer { Name = "karthik103", Username = "kvarma103", Password = "Tester99", Address = null, State = null, Country = null, EmailAddress = null, PAN = null, ContactNumber = null, DOB = Convert.ToDateTime("2000-12-31T23:59:59"), AccountType = null });
            dbcontext.SaveChanges();
        }

        public void Dispose()
        {
            Customers = null;
            dbcontext = null;
        }
    }
}
