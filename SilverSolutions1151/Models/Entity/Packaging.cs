using System.ComponentModel.DataAnnotations;

namespace SilverSolutions1151.Data.Entity
{
    public class Packaging
    {
        public Guid Id { get; set; }    
        public string Name { get; set; }
        [Display(Name ="Quantity(g)")]
        public decimal ? Quantity { get; set; }
    }
}
