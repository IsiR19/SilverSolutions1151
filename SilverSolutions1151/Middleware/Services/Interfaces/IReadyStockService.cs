using SilverSolutions1151.Models.Entity;

namespace SilverSolutions1151.Middleware.Services.Interfaces
{
    public interface IReadyStockService
    {
        List<Manufacture> GetReadyStockByDate(DateTime endDate);
        bool AddReadyStock(decimal molasesQty, DateTime manufactureDate, decimal packageSize);
        bool RemoveReadyStock(decimal quantity, DateTime manufactureDate);
    }
}
