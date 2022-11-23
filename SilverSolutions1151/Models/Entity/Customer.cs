using System.ComponentModel.DataAnnotations;

namespace SilverSolutions1151.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Display(Name = "Street Address")]
        public string Address { get; set; }

        public string City { get; set; }
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }

        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
    }
}