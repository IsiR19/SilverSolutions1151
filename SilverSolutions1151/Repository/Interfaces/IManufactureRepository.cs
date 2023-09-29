namespace SilverSolutions1151.Repository.Interfaces
{
    public interface IManufactureRepository
    {
        int GetRawTobaccoBalance(DateTime endDate);
        int GetManufactureBalance(DateTime endDate);
        int GetMixingBalance(DateTime endDate);
        int GetPackagingBalance(DateTime endDate);
        int GetReadyStockBalance(DateTime endDate);
        int GetSalesBalance(DateTime endDate);
    }
}
