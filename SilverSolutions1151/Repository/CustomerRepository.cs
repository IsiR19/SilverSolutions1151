using SilverSolutions1151.Data;
using SilverSolutions1151.Models;
using SilverSolutions1151.Repository.Interfaces;

namespace SilverSolutions1151.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ILogger<CustomerRepository> logger, ApplicationDbContext context)
        {
               _context = context;
                _logger = logger;
        }
        public bool CreateCustomer(Customer customer)
        {
           if(customer == null)
            {
                if (!UpdateCustomer(customer))
                {
                    _context.Add(customer);
                    _context.SaveChanges();
                }
                return true;
            }
           return false;
        }

        public Customer GetCustomer(Guid id)
        {
            return _context.Customers.Where(x => x.CustomerId == id).FirstOrDefault();
        }

        public List<Customer> GetCustomers(string searchString)
        {
           return _context.Customers.Where(x => x.CustomerName == searchString).ToList();
        }

        public bool UpdateCustomer(Customer customer)
        {
            var customerold = _context.Customers.Where(x => x.CustomerId == customer.CustomerId);

            if(customerold.Any())
            {
                _context.Customers.Update(customer);
                _context.SaveChanges();
            }
            return true;


        }
    }
}
