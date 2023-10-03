namespace SilverSolutions1151.Middleware.Services.Interfaces
{
    public interface ITobaccoMixingService
    {
        bool AddTobaccoMixing(int quantity, DateTime manufactureDate,decimal? glycerineQty, decimal? flavourQty
            , decimal? syrupQty, decimal? preservativeQty);
        int GetMixedTobaccoByDate(DateTime endDate);
        bool RemoveMixedTobacco(int quantity, DateTime manufactureDate);

    }
}
