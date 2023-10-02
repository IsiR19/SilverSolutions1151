using SilverSolutions1151.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SilverSolutions1151.Models.Entity
{
    public class CustomerInvoice : AuditEntity
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public int Vat { get; set; }
        public List<InvoiceItem>? InvoiceItems { get; set; }

    }

    public class InvoiceItem
    {
    
        public Guid InvoiceItemId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Weight { get; set; }
        public Guid CustomerInvoiceId { get; set; }
        public CustomerInvoice Invoice { get; set; }
    }
}
