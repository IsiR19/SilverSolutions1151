using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SilverSolutions1151.Data.Entity
{
    public class Catalog : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("ProductType")]
        public Guid ProductTypeId { get; set; }
        
        public virtual ProductType ProductType { get; set; }

        public decimal ? Price { get; set; }

        public List<RawMaterial> Materials { get; set; }
    }
}
