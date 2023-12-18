using SilverSolutions1151.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace SilverSolutions1151.Data.Entity
{
    public class ProductionReport : Entity
    {
        [DataType(DataType.Date)]
        public DateTime SearchDate { get; set; }
        public decimal RawTobaccoBalancePreviousDay { get; set; }
        public decimal RawTobaccoBalanceCurrentDay { get; set; }
        public decimal MixedTobaccoBalancePreviousDay { get; set; }
        public decimal MixedTobaccoBalanceCurrentDay { get; set; }
        public decimal ReadyStockBalancePreviousDay { get; set; }
        public decimal ReadyStockBalanceCurrentDay { get; set; }
        public decimal SoldBalancePreviousDay { get; set; }
        public decimal SoldBalanceCurrentDay { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal ClosedBalance { get; set; }
        public decimal InProgress { get; set; }
        public decimal ReadyStockk { get; set; }
        public decimal Sold { get; set; }
        public decimal Packing { get; set; }
        public ICollection<Balance> BalancesByPackagingSize { get; set; }
        public ICollection<Balance> SoldBySize { get; set; }
    }
}