namespace SilverSolutions1151.Middleware.Services.Interfaces
{
    public interface IRawTobaccoService
    {
        decimal GetRawTobaccoByDate(DateTime endDate);
        bool AddRawTobacco(decimal quantity, DateTime manufactureDate);
        bool RemoveRawTobacco(decimal quantity, DateTime manufactureDate);
    }
}
