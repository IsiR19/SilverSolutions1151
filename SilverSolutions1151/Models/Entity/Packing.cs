using SilverSolutions1151.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SilverSolutions1151.Models.Entity
{
    public class Packing
    {
        public Guid Id { get; set; }
        [ForeignKey("Catalog")]
        public Guid CatalogId { get; set; }
        public virtual Catalog? Catalog { get; set; }
        public int Quantity { get; set; }
    }
}
