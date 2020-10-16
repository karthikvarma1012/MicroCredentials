using MicroCredentials.Data.Models;
using MicroCredentials.Data.Persistence;
using System;
using Xunit;

namespace test
{
    public class CustomerRepositoryTest : IClassFixture<DatabaseFixture>
    {
        private readonly ICustomerRepository _repo;
        DatabaseFixture _fixture;

        public CustomerRepositoryTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _repo = new CustomerRepository(_fixture.dbcontext);
        }

        [Fact]
        public void GetCustomer()
        {
            //Act
            var customer = new Customer { Name = "karthik108", Username = "kvarma108", Password = "Tester99", Address = null, State = null, Country = null, EmailAddress = null, PAN = null, ContactNumber = null, DOB = Convert.ToDateTime("2000-12-31T23:59:59"), AccountType = null };

            _repo.AddCustomer(customer);
            var savedCustomer = _repo.GetCustomerByName("karthik108");

            var actual = _repo.GetCustomer(savedCustomer.Id);

            //Assert
            Assert.IsAssignableFrom<Customer>(actual);
            Assert.True(actual.Name == "karthik108");
        }

        [Fact]
        public void AddCustomer()
        {
            var customer = new Customer { Name = "karthik104", Username = "kvarma104", Password = "Tester99", Address = null, State = null, Country = null, EmailAddress = null, PAN = null, ContactNumber = null, DOB = Convert.ToDateTime("2000-12-31T23:59:59"), AccountType = null };

            _repo.AddCustomer(customer);
            var savedCustomer = _repo.GetCustomerByName("karthik104");

            Assert.Equal("kvarma104", savedCustomer.Username);
        }

        [Fact]
        public void EditCustomer()
        {
            var customer = new Customer { Name = "karthik105", Username = "kvarma105", Password = "Tester99", Address = null, State = null, Country = null, EmailAddress = null, PAN = null, ContactNumber = null, DOB = Convert.ToDateTime("2000-12-31T23:59:59"), AccountType = null };

            _repo.AddCustomer(customer);
            customer.Name = "karthik106";

            _repo.EditCustomer(customer);
            var savedCustomer = _repo.GetCustomerByName("karthik106");

            Assert.Equal("karthik106", savedCustomer.Name);
        }

        [Fact]
        public void DeleteCustomer()
        {
            var customer = new Customer { Name = "karthik107", Username = "kvarma107", Password = "Tester99", Address = null, State = null, Country = null, EmailAddress = null, PAN = null, ContactNumber = null, DOB = Convert.ToDateTime("2000-12-31T23:59:59"), AccountType = null };

            _repo.AddCustomer(customer);
            var savedCustomer = _repo.GetCustomerByName("karthik107");
            _repo.DeleteCustomer(savedCustomer.Id);

            Assert.Null(_repo.GetCustomerByName("karthik107"));
        }
    }
}
