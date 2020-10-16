using MicroCredentials.Data.Models;

namespace MicroCredentials.Data.Persistence
{
    public interface ICustomerRepository
    {
        Customer GetCustomer(int id);

        Customer GetCustomerByName(string customerName);

        bool IsCustomerExist(string userName);

        void AddCustomer(Customer customer);

        void EditCustomer(Customer customer);

        void DeleteCustomer(int id);
    }
}
