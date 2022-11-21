using MessagePack;

namespace SilverSolutions1151.Data.Entity
{
    public class Ingredients
    {

        public Guid Id { get; set; }
        public ProductType ProductType { get; set; }
        public string Description { get; set; }
        public decimal? Quantity { get; set; }
        public RawMaterial RawMaterial { get; set; }
        public decimal? TotalQuantity { get; set; }
    }
}
