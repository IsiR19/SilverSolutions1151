using SilverSolutions1151.Models;
using SilverSolutions1151.Models.Entity;

namespace SilverSolutions1151.Repository.Interfaces
{
    public interface ISalesRepository
    {
        bool CreateInvoiceDetails(CustomerInvoice customerInvoice);

        Task<CustomerInvoice> UpdateInvoiceDetailsAsync(Guid id,CustomerInvoice customerInvoice);
        Task<InvoiceTotals> GetSalesInvoiceDetailsAsync(Guid Id);
        Task<List<CustomerInvoice>> GetCustomerInvoiceList(string invoiceNumber,DateTime? startDate,DateTime? endDate);
       
    }
}
