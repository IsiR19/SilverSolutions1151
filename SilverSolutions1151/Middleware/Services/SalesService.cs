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

        public SalesService(ILogger<SalesService> logger, ISalesRepository salesRepository)
        {
            _logger = logger;
            _salesRepository = salesRepository;
        }

        public bool CreateInvoice(CustomerInvoice customerInvoice)
        {
            return _salesRepository.CreateInvoiceDetails(customerInvoice);
        }

        public async Task<InvoiceTotals> GetInvoiceAsync(Guid invoiceId)
        {
            return await _salesRepository.GetSalesInvoiceDetailsAsync(invoiceId);
        }

        public async Task<CustomerInvoice> UpdateInvoice(Guid id,CustomerInvoice customerInvoice)
        {
            return await _salesRepository.UpdateInvoiceDetailsAsync(id,customerInvoice);
        }
    }
}
