using System.ComponentModel.DataAnnotations.Schema;

namespace SilverSolutions1151.Models.Entity
{
    [NotMapped]
    public class Balance
    {
        public int PackagingSize { get; set; }
        public int Amount { get; set; }
    }
}
