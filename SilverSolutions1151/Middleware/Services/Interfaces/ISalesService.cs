using SilverSolutions1151.Models;
using SilverSolutions1151.Models.Entity;

namespace SilverSolutions1151.Middleware.Services.Interfaces
{
    public interface ISalesService
    {
        bool CreateInvoice(CustomerInvoice customerInvoice);
        Task<List<CustomerInvoice>> GetInvoiceListAsync(string invoiceNumber, DateTime? startDate, DateTime? endDate);
        Task<CustomerInvoice> UpdateInvoice(Guid id, CustomerInvoice customerInvoice);
        Task<InvoiceTotals> GetInvoiceAsync(Guid invoiceId);

    }
}
