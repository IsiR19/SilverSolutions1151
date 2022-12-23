using System.ComponentModel.DataAnnotations;

namespace SilverSolutions1151.Data.Entity
{
    public class ProductionReport : Entity
    {
        [DataType(DataType.Date)]
        public DateTime SearchDate { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal ClosedBalance { get; set; }
        public decimal InProgress { get; set; }
        public decimal ReadyStockk { get; set; }
        public decimal Sold { get; set; }
        public decimal Packing { get; set; }
    }
}