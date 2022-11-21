using System.ComponentModel.DataAnnotations.Schema;

namespace SilverSolutions1151.Data.Entity
{
    public class Inventory : AuditEntity
    {   
        [ForeignKey("Catalog")]
        [Column("Catalog Item")]
        public Guid CatalogId { get; set; }

        [ForeignKey("RawMaterial")]
        public Guid RawMaterialId { get; set; }
        public virtual RawMaterial RawMaterial { get; set; }

        public int Quantity { get; set; }

        public int Balance{ get; set; }

        public bool IsSellable { get; set; }
    }
}
