using SilverSolutions1151.Middleware.Extensions;
using SilverSolutions1151.Middleware.Services.Interfaces;
using SilverSolutions1151.Repository.Interfaces;

namespace SilverSolutions1151.Middleware.Services
{
    public class TobaccoMixingService : ITobaccoMixingService
    {
        private ILogger<TobaccoMixingService> _logger;
        private IManufactureRepository _manufactureRepository;
        private IRawTobaccoService _tobaccoService;

        public TobaccoMixingService(ILogger<TobaccoMixingService> logger,IManufactureRepository manufactureRepository, IRawTobaccoService tobaccoService)
        {
            _logger = logger;
            _manufactureRepository = manufactureRepository;
            _tobaccoService = tobaccoService;

        }
        public bool AddTobaccoMixing(int quantity, DateTime manufactureDate, int ingredientQty)
        {
            var qty = quantity + ingredientQty;
            if(_manufactureRepository.AddMixedTobacco(qty, manufactureDate))
                return _tobaccoService.RemoveRawTobacco(quantity,manufactureDate);

            return false;
        }

        public int GetMixedTobaccoByDate(DateTime endDate)
        {

            return _manufactureRepository.GetMixedTobaccoBalance(endDate.EndOfDay());
        }

        public bool RemoveMixedTobacco(int quantity, DateTime manufactureDate)
        {
            return _manufactureRepository.RemoveMixedTobacco(quantity,manufactureDate);
        }
    }
}
