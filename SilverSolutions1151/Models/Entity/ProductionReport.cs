using SilverSolutions1151.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace SilverSolutions1151.Data.Entity
{
    public class ProductionReport : Entity
    {
        [DataType(DataType.Date)]
        public DateTime SearchDate { get; set; }
        public int  RawTobaccoBalancePreviousDay { get; set; }
        public int RawTobaccoBalanceCurrentDay { get; set; }
        public int MixedTobaccoBalancePreviousDay { get; set; }
        public int MixedTobaccoBalanceCurrentDay { get; set; }
        public int ReadyStockBalancePreviousDay { get; set; }
        public int ReadyStockBalanceCurrentDay { get; set; }
        public int SoldBalancePreviousDay { get; set; }
        public int SoldBalanceCurrentDay { get; set; }
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