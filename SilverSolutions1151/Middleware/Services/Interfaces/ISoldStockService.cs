using SilverSolutions1151.Models.Entity;

namespace SilverSolutions1151.Middleware.Services.Interfaces
{
    public interface ISoldStockService
    {
        int GeSoldByDate(DateTime endDate);
        List<Manufacture> GetSoldStockBalance(DateTime endDate);
    }
}
