using Microsoft.Ajax.Utilities;
using SilverSolutions1151.Models.Entity;

namespace SilverSolutions1151.Models
{
    public class InvoiceTotals
    {
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public decimal VatPercentage { get; set; }
        public decimal VatTotal { get; set; }
        public CustomerInvoice? Invoice { get; set; }
    }
}