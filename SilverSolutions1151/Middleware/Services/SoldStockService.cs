using SilverSolutions1151.Middleware.Extensions;
using SilverSolutions1151.Middleware.Services.Interfaces;
using SilverSolutions1151.Repository.Interfaces;

namespace SilverSolutions1151.Middleware.Services
{
    public class SoldStockService : ISoldStockService
    {
        private readonly ILogger<SoldStockService> _logger;
        private readonly IManufactureRepository _manufactureRepository;
        public SoldStockService(ILogger<SoldStockService> logger,IManufactureRepository manufactureRepository) 
        {
            _logger = logger;
            _manufactureRepository = manufactureRepository;
        }
        public int GeSoldByDate(DateTime endDate)
        {
            return _manufactureRepository.GetSoldStockBalanceByDate(endDate.StartOfDay(),endDate.EndOfDay());
        }
    }
}
