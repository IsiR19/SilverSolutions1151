namespace SilverSolutions1151.Middleware.Services.Interfaces
{
    public interface ITobaccoMixingService
    {
        bool AddTobaccoMixing(int quantity, DateTime manufactureDate,int ingredientQty);
        int GetMixedTobaccoByDate(DateTime endDate);
        bool RemoveMixedTobacco(int quantity, DateTime manufactureDate);

    }
}
