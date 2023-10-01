using SilverSolutions1151.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SilverSolutions1151.Models.Entity
{
    public class CustomerInvoice : AuditEntity
    {
        [Key]
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public int Vat { get; set; }

    }

    public class InvoiceItem
    {
        [Key]
        public int InvoiceItemId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Weight { get; set; }
        [ForeignKey(nameof(Invoice))]
        public int InvoiceId { get; set; }
        public CustomerInvoice Invoice { get; set; }
    }
}
