using SilverSolutions1151.Middleware.Extensions;
using SilverSolutions1151.Models.Entity;
using SilverSolutions1151.Repository;

namespace SilverSolutions1151.Middleware.Calculations
{
    public class Manufacturing
    {
        private readonly ILogger<Manufacturing> _logger;

        public async Task<Balances> GetBalances(DateTime? startDate)
        {
            if(!startDate.HasValue) 
            { 
                startDate= DateTime.Now.StartOfDay();
            }
            
            DateTime endDate = DateTime.Now.EndOfDay();

            return new Balances();
        }
    }
}
