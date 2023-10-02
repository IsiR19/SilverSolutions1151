using Microsoft.EntityFrameworkCore;
using SilverSolutions1151.Data;
using SilverSolutions1151.Models;
using SilverSolutions1151.Models.Entity;
using SilverSolutions1151.Repository.Interfaces;

namespace SilverSolutions1151.Repository
{
    public class SalesRepository : ISalesRepository
    {
        private readonly ILogger<SalesRepository> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IManufactureRepository _manufactureRepository;

        public SalesRepository(ILogger<SalesRepository> logger, ApplicationDbContext context,IManufactureRepository manufactureRepository)
        {
                _context = context;
            _logger = logger;   
            _manufactureRepository = manufactureRepository;
        }

        public bool CreateInvoiceDetails(CustomerInvoice customerInvoice)
        {
            var totalStockSold = 0;
            customerInvoice.Id = Guid.NewGuid();
            foreach (var item in customerInvoice.InvoiceItems)
            {
                item.InvoiceItemId = Guid.NewGuid();
                item.CustomerInvoiceId = customerInvoice.Id;
                _context.Add(item);
                totalStockSold += item.Quantity ;
            }
            customerInvoice.CreatedBy = "Admin";
            customerInvoice.ModifiedBy = "Admin";
            customerInvoice.CreatedModifiedDate = DateTime.Now;
            customerInvoice.CreatedDate = DateTime.Now;
            customerInvoice.IsDeleted = false;
            _context.Add(customerInvoice);
            _context.SaveChangesAsync();
            _manufactureRepository.AddSoldStock(totalStockSold, customerInvoice.InvoiceDate);
            return true;
        }

        public async Task<CustomerInvoice> UpdateInvoiceDetailsAsync(Guid id, CustomerInvoice customerInvoice)
        {

            var existingInvoice = await _context.CustomerInvoice
                .Include(ci => ci.InvoiceItems)
                .FirstOrDefaultAsync(ci => ci.Id == id);

            
            if (existingInvoice == null)
            {
                throw new ArgumentException("Invoice Not Found");
            }

            // Update the properties of the existing invoice with the values from the form data
            existingInvoice.InvoiceNumber = customerInvoice.InvoiceNumber;
            existingInvoice.InvoiceDate = customerInvoice.InvoiceDate;
            existingInvoice.CustomerName = customerInvoice.CustomerName;
            existingInvoice.CustomerPhone = customerInvoice.CustomerPhone;
            existingInvoice.CustomerAddress = customerInvoice.CustomerAddress;
            existingInvoice.Vat = customerInvoice.Vat;
            existingInvoice.InvoiceItems = customerInvoice.InvoiceItems;

            // Remove any invoice items that were deleted in the form data
            var totalStockAdded = 0;
            foreach (var existingItem in existingInvoice.InvoiceItems)
            {
                if (!customerInvoice.InvoiceItems.Any(ci => ci.InvoiceItemId == existingItem.InvoiceItemId))
                {
                    _context.Remove(existingItem);
                    totalStockAdded += existingItem.Quantity;
                }
            }
            _manufactureRepository.RemoveSoldStock(totalStockAdded, DateTime.Now);
           
            // Update or add any invoice items that were modified or added in the form data
            totalStockAdded = 0;
            foreach (var item in customerInvoice.InvoiceItems)
            {
                if (item.InvoiceItemId == Guid.Empty)
                {
                    // This is a new invoice item, so add it to the context
                    item.InvoiceItemId = Guid.NewGuid();
                    item.CustomerInvoiceId = existingInvoice.Id;
                    _context.Add(item);
                    totalStockAdded += item.Quantity;
                }
                else
                {
                    // This is an existing invoice item, so update its properties
                    var existingItem = existingInvoice.InvoiceItems.FirstOrDefault(ci => ci.InvoiceItemId == item.InvoiceItemId);
                    if (existingItem != null)
                    {
                        existingItem.Description = item.Description;
                        existingItem.Price = item.Price;
                        existingItem.Quantity = item.Quantity;
                        existingItem.Weight = item.Weight;

                        if(existingItem.Quantity != item.Quantity)
                        {
                            totalStockAdded += item.Quantity;
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();

            _manufactureRepository.AddSoldStock(totalStockAdded, customerInvoice.InvoiceDate);


            customerInvoice = await _context.CustomerInvoice
                .Include(ci => ci.InvoiceItems)
                .FirstOrDefaultAsync(ci => ci.Id == id);

            return customerInvoice;
        }
        public async Task<InvoiceTotals> GetSalesInvoiceDetailsAsync(Guid Id)
        {
            var customerInvoice = await _context.CustomerInvoice
                .Include(ci => ci.InvoiceItems)
                .FirstOrDefaultAsync(ci => ci.Id == Id);

            InvoiceTotals invoiceTotals = new InvoiceTotals
            {
                SubTotal = CalculateSubTotal(customerInvoice.InvoiceItems),
                VatPercentage = customerInvoice.Vat,
                VatTotal = CalculateVat(customerInvoice),
                Invoice = customerInvoice,
                
            };

            invoiceTotals.Total = invoiceTotals.SubTotal + invoiceTotals.VatTotal;

           return invoiceTotals;
        }

        private decimal CalculateSubTotal(List<InvoiceItem> invoiceItems)
        {
            decimal total = 0;
            foreach (var item in invoiceItems)
            {
                total =+ (item.Price * item.Quantity);
                item.TotalPrice = total;
            }

            return total;
        }

        private decimal CalculateVat(CustomerInvoice customerInvoice)
        {
            decimal total = 0;
            decimal vatPercentage = ((decimal)customerInvoice.Vat / 100);


            foreach (var item in customerInvoice.InvoiceItems)
            {
             
                total =+ (item.Price * vatPercentage);
            }

            return total;
        }
    }
}
