using System.ComponentModel.DataAnnotations.Schema;

namespace SilverSolutions1151.Data.Entity
{
    public class ManufacturingStage
    {
        public Guid Id { get; set; }
        public ProductionStage ProductionStage { get; set; }
        public decimal Quantity { get; set; }
        private DateTime createdDate;

        [Column("CreatedDate")]
        public DateTime CreatedDate
        {
            get
            {
                this.createdDate = DateTime.Now;
                return this.createdDate;
            }
            set { this.createdDate = value; }
        }
    }
}

public enum ProductionStage
{
    RawTobacco = 0,
    Mixing = 1, // Will stay for a while depends on season, Expectation for repoting
    Packing = 2,
    Complete = 3,
    Sold = 4
}