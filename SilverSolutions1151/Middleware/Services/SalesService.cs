using SilverSolutions1151.Middleware.Extensions;
using SilverSolutions1151.Middleware.Services.Interfaces;
using SilverSolutions1151.Models;
using SilverSolutions1151.Models.Entity;
using SilverSolutions1151.Repository.Interfaces;

namespace SilverSolutions1151.Middleware.Services
{
    public class SalesService : ISalesService
    {
        private readonly ILogger<SalesService> _logger;
        private readonly ISalesRepository _salesRepository;
        private readonly IManufactureRepository _manufactureRepository;
        private readonly ICustomerRepository _customerRepository;

        public SalesService(ILogger<SalesService> logger, ISalesRepository salesRepository,
            IManufactureRepository manufactureRepository, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _salesRepository = salesRepository;
            _manufactureRepository = manufactureRepository;
            _customerRepository = customerRepository;
        }

        public bool CreateInvoice(CustomerInvoice customerInvoice)
        {
            if (_salesRepository.CreateInvoiceDetails(customerInvoice))
                AddManufacturingSoldStock(customerInvoice);



            var customer = new Customer
            {
                CustomerName = customerInvoice.CustomerName,
                Address = customerInvoice.CustomerAddress,
                Phone = customerInvoice.CustomerPhone,
                
            };

            
            _customerRepository.CreateCustomer(customer);
            
            return true ;
        }

        public async Task<InvoiceTotals> GetInvoiceAsync(Guid invoiceId)
        {
            return await _salesRepository.GetSalesInvoiceDetailsAsync(invoiceId);
        }

        public async Task<List<CustomerInvoice>> GetInvoiceListAsync(string invoiceNumber,DateTime? startDate,DateTime? endDate)
        {
            return await _salesRepository.GetCustomerInvoiceList(invoiceNumber,startDate?.StartOfDay(),endDate?.EndOfDay());
        }

        public async Task<CustomerInvoice> UpdateInvoice(Guid id,CustomerInvoice customerInvoice)
        {
            return await _salesRepository.UpdateInvoiceDetailsAsync(id,customerInvoice);
        }

        private async Task<bool> AddManufacturingSoldStock(CustomerInvoice customerInvoice)
        {
            if(customerInvoice.InvoiceItems.Count > 0)
            {
                
                foreach(var item in customerInvoice.InvoiceItems)
                {
                    if (_manufactureRepository.AddSoldStock(item.Quantity,item.Weight, customerInvoice.InvoiceDate))
                        _manufactureRepository.RemoveReadyStock(item.Quantity,item.Weight , customerInvoice.InvoiceDate);
                }

                
            }
            return true;
        }
    }
}
