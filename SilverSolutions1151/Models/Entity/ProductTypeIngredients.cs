using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SilverSolutions1151.Data.Entity
{
    public class ProductTypeIngredients
    {
        public Guid Id { get; set; }
        [ForeignKey("ProductType")]
        [Required]
        public string ProductTypeId { get; set; }
        [ForeignKey("Ingredients")]
        [Required]
        public string ProductName { get; set; }
    }
}
