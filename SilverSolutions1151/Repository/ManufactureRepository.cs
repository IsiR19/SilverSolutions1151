using SilverSolutions1151.Controllers;
using SilverSolutions1151.Data;
using SilverSolutions1151.Repository.Interfaces;

namespace SilverSolutions1151.Repository
{
    public class ManufactureRepository : IManufactureRepository
    {
        private readonly ILogger<ManufactureRepository> _logger;
        private readonly ApplicationDbContext _context;

        public ManufactureRepository(ILogger<ManufactureRepository> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public int GetRawTobaccoBalance(DateTime endDate)
        {
            {
                return Decimal.ToInt32(_context.ManufacturingStage
                    .Where(x => x.ProductionStage == ProductionStage.RawTobacco && x.CreatedDate <= endDate)
                    .Sum(x => x.Quantity));
            }
        }

        public int GetManufactureBalance(DateTime endDate)
        {
            {
                return Decimal.ToInt32(_context.ManufacturingStage
                    .Where(x => x.ProductionStage == ProductionStage.RawTobacco && x.CreatedDate <= endDate)
                    .Sum(x => x.Quantity));
            }
        }

        public int GetMixingBalance(DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public int GetPackagingBalance(DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public int GetReadyStockBalance(DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public int GetSalesBalance(DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
