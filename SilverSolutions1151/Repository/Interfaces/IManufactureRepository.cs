using SilverSolutions1151.Migrations;
using SilverSolutions1151.Models.Entity;

namespace SilverSolutions1151.Repository.Interfaces
{
    public interface IManufactureRepository
    {
        int GetRawTobaccoBalance(DateTime endDate);
        bool AddRawTobacco(int quantity, DateTime manufactureDate);
        bool RemoveRawTobacco(int quantity,DateTime manufactureDate);
        bool AddMixedTobacco(TobaccoMixture tobaccoMixture,DateTime manufactureDate);
        int GetMixedTobaccoBalance(DateTime endDate);
        bool RemoveMixedTobacco(int quantity, DateTime manufactureDate);
        List<Manufacture> GetReadyStockBalance(DateTime endDate);
        List<Manufacture> GetSoldStockBalance(DateTime startDate,DateTime endDate);
        bool AddReadyStockTobacco(int quantity, int packageSize, DateTime manufactureDate);
        bool RemoveReadyStock(int quantity,int packageSize, DateTime manufactureDate);
        int GetSoldStockBalanceByDate(DateTime endDate);
        int GetSoldStockBalanceByDate(DateTime startDate, DateTime endDate);
        bool AddSoldStock(int quantity,int packagingSize, DateTime manufactureDate);
        bool RemoveSoldStock(int quantity, int packagingSize, DateTime manufactureDate);
        List<Manufacture> GetManufactureItemsByDateAndType(Models.Entity.ProductionStage stage,DateTime? startDate,DateTime? endDate);

    }
}
