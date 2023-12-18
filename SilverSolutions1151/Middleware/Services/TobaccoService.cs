using SilverSolutions1151.Middleware.Extensions;
using SilverSolutions1151.Middleware.Services.Interfaces;
using SilverSolutions1151.Repository.Interfaces;
using System.Web.WebPages;

namespace SilverSolutions1151.Middleware.Services
{
    public class TobaccoService : IRawTobaccoService
    {
        private readonly ILogger<TobaccoService> _logger;
        private readonly IManufactureRepository _manufactureRepository;
        public TobaccoService(ILogger<TobaccoService> logger,IManufactureRepository manufactureRepository)
        {
                _logger = logger;
                _manufactureRepository = manufactureRepository;
        }

        public bool AddRawTobacco(int quantity,DateTime manufactureDate)
        {
            return _manufactureRepository.AddRawTobacco(quantity, manufactureDate.EnsureTime());
        }

        public int GetRawTobaccoByDate(DateTime endDate)
        {
            endDate = endDate.EndOfDay();
            return _manufactureRepository.GetRawTobaccoBalance(endDate);
        }

        public bool RemoveRawTobacco(int quantity,DateTime manufactureDate)
        {
            return _manufactureRepository.RemoveRawTobacco(quantity, manufactureDate);
        }
    }
}
