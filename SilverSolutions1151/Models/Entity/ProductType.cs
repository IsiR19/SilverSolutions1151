namespace SilverSolutions1151.Data.Entity
{
    public class ProductType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<RawMaterial>? RawMaterials { get; set; }
    }
}
