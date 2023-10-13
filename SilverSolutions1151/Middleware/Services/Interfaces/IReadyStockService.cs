using SilverSolutions1151.Models.Entity;

namespace SilverSolutions1151.Middleware.Services.Interfaces
{
    public interface IReadyStockService
    {
        List<Manufacture> GetReadyStockByDate(DateTime endDate);
        bool AddReadyStock(int molasesQty, DateTime manufactureDate,int packageSize);
        bool RemoveReadyStock(int quantity, DateTime manufactureDate);
    }
}
