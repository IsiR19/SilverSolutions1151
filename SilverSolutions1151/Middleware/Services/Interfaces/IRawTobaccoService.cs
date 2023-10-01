namespace SilverSolutions1151.Middleware.Services.Interfaces
{
    public interface IRawTobaccoService
    {
        int GetRawTobaccoByDate(DateTime endDate);
        bool AddRawTobacco(int quantity, DateTime manufactureDate);
        bool RemoveRawTobacco(int quantity, DateTime manufactureDate);
    }
}
