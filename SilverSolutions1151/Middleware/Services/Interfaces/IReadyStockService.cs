namespace SilverSolutions1151.Middleware.Services.Interfaces
{
    public interface IReadyStockService
    {
        int GetReadyStockByDate(DateTime endDate);
        bool AddReadyStock(int numberOfBoxes, DateTime manufactureDate,int grams);
        bool RemoveReadyStock(int quantity, DateTime manufactureDate);
    }
}
