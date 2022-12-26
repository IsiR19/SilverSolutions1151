namespace SilverSolutions1151.Models.Entity
{
    public partial class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        public int CategoryID { get; set; }
        public string Name { get; set; }
        public Nullable<int> Status { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
