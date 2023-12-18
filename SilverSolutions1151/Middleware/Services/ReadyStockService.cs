using SilverSolutions1151.Middleware.Extensions;
using SilverSolutions1151.Middleware.Services.Interfaces;
using SilverSolutions1151.Models.Entity;
using SilverSolutions1151.Repository.Interfaces;

namespace SilverSolutions1151.Middleware.Services
{
    public class ReadyStockService : IReadyStockService
    {
        private readonly ILogger<ReadyStockService> _logger;
        private readonly IManufactureRepository _manufactureRepository;
        private readonly ITobaccoMixingService _tabaccoMixingService;

        public ReadyStockService(ILogger<ReadyStockService> logger, IManufactureRepository manufactureRepository, ITobaccoMixingService tobaccoMixingService )
        {
            _logger = logger;
            _manufactureRepository = manufactureRepository;
            _tabaccoMixingService = tobaccoMixingService;
        }
        public bool AddReadyStock(decimal molasesQty, DateTime manufactureDate, decimal packageSize)
        {
            //Molases KG = number of boxes ---Change
            //Convert g to kg
            var packagingSize = ((packageSize) / 1000);
            var boxes = molasesQty / packagingSize;
            // grams = box size -- grams per box
            if (_manufactureRepository.AddReadyStockTobacco(boxes,packageSize, manufactureDate.EnsureTime()))
                return _tabaccoMixingService.RemoveMixedTobacco(molasesQty, manufactureDate.EnsureTime());

            return false;
        }

        public List<Manufacture> GetReadyStockByDate(DateTime endDate)
        {
            var readyStock = _manufactureRepository.GetReadyStockBalance(endDate);
            
            return readyStock;
        }

        public bool RemoveReadyStock(decimal quantity, DateTime manufactureDate)
        {
            return _manufactureRepository.RemoveReadyStock(quantity,0, manufactureDate);
        }
    }
}
