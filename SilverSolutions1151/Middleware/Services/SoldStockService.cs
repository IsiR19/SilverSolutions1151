using SilverSolutions1151.Middleware.Extensions;
using SilverSolutions1151.Middleware.Services.Interfaces;
using SilverSolutions1151.Models.Entity;
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
        public decimal GeSoldByDate(DateTime endDate)
        {
            return _manufactureRepository.GetSoldStockBalanceByDate(endDate.StartOfDay(),endDate.EndOfDay());
        }

        public List<Manufacture> GetSoldStockBalance(DateTime endDate)
        {
            return _manufactureRepository.GetSoldStockBalance(endDate.StartOfDay(),endDate.EndOfDay());
        }
    }
}
