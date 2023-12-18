using SilverSolutions1151.Migrations;
using SilverSolutions1151.Models.Entity;

namespace SilverSolutions1151.Repository.Interfaces
{
    public interface IManufactureRepository
    {
        decimal GetRawTobaccoBalance(DateTime endDate);
        bool AddRawTobacco(decimal quantity, DateTime manufactureDate);
        bool RemoveRawTobacco(decimal quantity,DateTime manufactureDate);
        bool AddMixedTobacco(TobaccoMixture tobaccoMixture,DateTime manufactureDate);
        decimal GetMixedTobaccoBalance(DateTime endDate);
        bool RemoveMixedTobacco(decimal quantity, DateTime manufactureDate);
        List<Manufacture> GetReadyStockBalance(DateTime endDate);
        List<Manufacture> GetSoldStockBalance(DateTime startDate,DateTime endDate);
        bool AddReadyStockTobacco(decimal quantity, decimal packageSize, DateTime manufactureDate);
        bool RemoveReadyStock(decimal quantity, decimal packageSize, DateTime manufactureDate);
        decimal GetSoldStockBalanceByDate(DateTime endDate);
        decimal GetSoldStockBalanceByDate(DateTime startDate, DateTime endDate);
        bool AddSoldStock(decimal quantity, decimal packagingSize, DateTime manufactureDate);
        bool RemoveSoldStock(decimal quantity, decimal packagingSize, DateTime manufactureDate);
        List<Manufacture> GetManufactureItemsByDateAndType(Models.Entity.ProductionStage stage,DateTime? startDate,DateTime? endDate);

    }
}
