using MicroCredentials.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MicroCredentials.Data.Persistence
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ICustomerDbContext _context;

        public CustomerRepository(ICustomerDbContext context)
        {
            _context = context;
        }

        public Customer GetCustomer(int id)
        {
            var _customer = _context.Customers.FirstOrDefault(x => x.Id == id);
            return _customer;
        }

        public Customer GetCustomerByName(string customerName) {
            var _customer = _context.Customers.FirstOrDefault(x => x.Name == customerName);
            return _customer;
        }

        public bool IsCustomerExist(string username) {
            return _context.Customers.Any(e => e.Username.ToLower() == username.ToLower());
        }

        public void AddCustomer(Customer customer) {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }
        public void DeleteCustomer(int id)
        {
            var customer = GetCustomer(id);
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }

        public void EditCustomer(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
