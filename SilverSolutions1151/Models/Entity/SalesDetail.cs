using System.ComponentModel.DataAnnotations.Schema;

namespace SilverSolutions1151.Models.Entity
{
    public partial class SalesDetail
    {
        public int SalesDetailID { get; set; }
        [ForeignKey("Sale")]
        public Nullable<int> SalesID { get; set; }
        [ForeignKey("Product")]
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> ProductName { get; set; }
        public Nullable<double> UnitPrice { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<double> LineTotal { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> Status { get; set; }

        public virtual Sale Sale { get; set; }
    }
}
