using System.ComponentModel.DataAnnotations.Schema;

namespace SilverSolutions1151.Data.Entity
{
    public class Ingredients
    {
        public Guid Id { get; set; }
        [ForeignKey("ProductType")]
        public Guid ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
        public string Description { get; set; }
        public int? Ratio { get; set; }
    }
}