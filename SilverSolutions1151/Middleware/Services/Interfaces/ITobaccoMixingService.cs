namespace SilverSolutions1151.Middleware.Services.Interfaces
{
    public interface ITobaccoMixingService
    {
        bool AddTobaccoMixing(decimal quantity, DateTime manufactureDate,decimal? glycerineQty, decimal? flavourQty
            , decimal? syrupQty, decimal? preservativeQty);
        decimal GetMixedTobaccoByDate(DateTime endDate);
        bool RemoveMixedTobacco(decimal quantity, DateTime manufactureDate);

    }
}
