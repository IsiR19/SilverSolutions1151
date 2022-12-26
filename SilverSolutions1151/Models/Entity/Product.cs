namespace SilverSolutions1151.Models.Entity
{
    public partial class Product
    {
        public Product()
        {
            this.ProductStocks = new HashSet<ProductStock>();
        }

        public int ProductID { get; set; }
        public string Name { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> Status { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<ProductStock> ProductStocks { get; set; }
    }
}
