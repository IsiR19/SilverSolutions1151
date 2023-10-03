using System.ComponentModel.DataAnnotations.Schema;

namespace SilverSolutions1151.Data.Entity
{
    public class ProductType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Packaging")]
        public Guid PackagingId { get; set; }

        public virtual Packaging? Packaging { get; set; }

        public ICollection<RawMaterial>? RawMaterials { get; set; }
    }
}