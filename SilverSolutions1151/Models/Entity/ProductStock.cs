namespace SilverSolutions1151.Models.Entity
{
    public partial class ProductStock
    {
        public Guid ProductStockID { get; set; }
        public int ProductQtyID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> PurchasePrice { get; set; }
        public Nullable<decimal> SalesPrice { get; set; }
        public virtual Product Product { get; set; }
    }
}
