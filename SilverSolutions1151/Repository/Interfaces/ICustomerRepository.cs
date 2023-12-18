using SilverSolutions1151.Models;

namespace SilverSolutions1151.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers(string searchString);
        Customer GetCustomer(Guid id);
        bool CreateCustomer(Customer customer);
        bool UpdateCustomer(Customer customer); 
    }
}
