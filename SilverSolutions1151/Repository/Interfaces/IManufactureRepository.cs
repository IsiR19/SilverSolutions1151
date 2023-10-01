namespace SilverSolutions1151.Repository.Interfaces
{
    public interface IManufactureRepository
    {
        int GetRawTobaccoBalance(DateTime endDate);
        bool AddRawTobacco(int quantity, DateTime manufactureDate);
        bool RemoveRawTobacco(int quantity,DateTime manufactureDate);
        bool AddMixedTobacco(int quantity,DateTime manufactureDate);
        int GetMixedTobaccoBalance(DateTime endDate);
        bool RemoveMixedTobacco(int quantity, DateTime manufactureDate);
        int GetReadyStockBalance(DateTime endDate);
        bool AddReadyStockTobacco(int quantity, DateTime manufactureDate);
        bool RemoveReadyStock(int quantity, DateTime manufactureDate);

    }
}
