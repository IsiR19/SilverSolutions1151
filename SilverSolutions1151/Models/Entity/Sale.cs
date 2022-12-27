using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SilverSolutions1151.Models.Entity
{
    public partial class Sale
    {
        public Sale()
        {
            //this.SalesDetails = new HashSet<SalesDetail>();
        }

        [Key]
        public int SalesID { get; set; }
        public string OrderNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> OrderDate { get; set; }
        public string PaymentMethod { get; set; }
        public Nullable<double> TotalAmout { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<double> Subtotal { get; set; }
        public Nullable<int> DiscountParcentage { get; set; }
        public Nullable<int> VatParcentage { get; set; }
        public List<SalesDetail> SalesDetails { get; set; }
        [NotMapped]
        public int SalesDetailId { get; set; }
        [NotMapped]
        public Nullable<int> ProductId { get; set; }
        [NotMapped]
        public Nullable<double> UnitPrice { get; set; }
        [NotMapped]
        public Nullable<int> Quantity { get; set; }
        [NotMapped]
        public Nullable<double> LineTotal { get; set; }
    }
}

