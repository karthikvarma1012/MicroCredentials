using MicroCredentials.Data.Models;
using MicroCredentials.Data.Persistence;
using System.Linq;

namespace MicroCredentials.Services
{
    public class CustomerService : ICustomerService
    {
        private IGenericRepository<Customer> _repo = null;
        public CustomerService(IGenericRepository<Customer> repo)
        {
            _repo = repo;
        }

        public Customer GetCustomer(int id)
        {
            var customer = _repo.GetById(id);
            return customer;
        }

        public Customer GetCustomerByName(string customerName)
        {
            var customers = _repo.GetAll();
            var customer = customers.Where(e => e.Name.ToLower() == customerName.ToLower()).ToList()[0];
            return customer;
        }

        public bool IsCustomerExist(string userName)
        {
            var customers = _repo.GetAll();
            return customers.Any(e => e.Username.ToLower() == userName.ToLower());
        }

        public void AddCustomer(Customer customer)
        {
            _repo.Insert(customer);
        }

        public void EditCustomer(Customer customer)
        {
            _repo.Update(customer);
        }

        public void DeleteCustomer(int id)
        {
            _repo.Delete(id);
        }
    }
}
