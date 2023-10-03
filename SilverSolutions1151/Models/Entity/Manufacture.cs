using SilverSolutions1151.Data.Entity;

namespace SilverSolutions1151.Models.Entity
{
    public class Manufacture : AuditEntity
    {
        public ProductionStage ProductionStage { get; set; }
        public decimal Quantity { get; set; }
        public DateTime ManufactureDate { get; set; }
        public decimal Tobacco { get; set; } = 0;
        public decimal Flavour { get; set; } = 0;
        public decimal Preservatives { get; set; } = 0;
        public decimal Syrup { get; set; } = 0;
        public decimal Glycerine { get; set; } = 0;
    }

    public enum ProductionStage
    {
        RawTobacco = 0,
        Mixing = 1, // Will stay for a while depends on season, Expectation for repoting
        Packing = 2,
        Complete = 3,
        Sold = 4
    }
}
