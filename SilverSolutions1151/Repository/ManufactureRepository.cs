using SilverSolutions1151.Controllers;
using SilverSolutions1151.Data;
using SilverSolutions1151.Data.Entity;
using SilverSolutions1151.Models.Entity;
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
                return Decimal.ToInt32(_context.Manufacturing
                    .Where(x => x.ProductionStage == Models.Entity.ProductionStage.RawTobacco && x.ManufactureDate <= endDate)
                    .Sum(x => x.Quantity));
            }
        }
        public bool AddRawTobacco(int quantity, DateTime manufactureDate)
        {
            _context.Add(new Manufacture
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Quantity = quantity,
                ManufactureDate = manufactureDate,
                ProductionStage = Models.Entity.ProductionStage.RawTobacco,
                CreatedBy = "Admin",
                ModifiedBy = "Admin"
            });
            _context.SaveChanges();
            return true;

        }

        public int GetManufactureBalance(DateTime endDate)
        {
            {
                return Decimal.ToInt32(_context.ManufacturingStage
                    .Where(x => x.ProductionStage == ProductionStage.RawTobacco && x.CreatedDate <= endDate)
                    .Sum(x => x.Quantity));
            }
        }

        public bool AddMixedTobacco(int quantity, DateTime manufactureDate)
        {
            _context.Add(new Manufacture
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Quantity = quantity,
                ManufactureDate = manufactureDate,
                ProductionStage = Models.Entity.ProductionStage.Mixing,
                CreatedBy = "Admin",
                ModifiedBy = "Admin"
            });
            _context.SaveChanges();
            return true;
        }

        public bool RemoveRawTobacco(int quantity, DateTime manufactureDate)
        {
            _context.Add(new Manufacture
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Quantity = -quantity,
                ManufactureDate = manufactureDate,
                ProductionStage = Models.Entity.ProductionStage.RawTobacco,
                CreatedBy = "Admin",
                ModifiedBy = "Admin"
            });
            _context.SaveChanges();
            return true;
        }

        public int GetMixedTobaccoBalance(DateTime endDate)
        {
            return Decimal.ToInt32(_context.Manufacturing
                     .Where(x => x.ProductionStage == Models.Entity.ProductionStage.Mixing && x.ManufactureDate <= endDate)
                     .Sum(x => x.Quantity));
        }

        public bool RemoveMixedTobacco(int quantity, DateTime manufactureDate)
        {
            _context.Add(new Manufacture
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Quantity = -quantity,
                ManufactureDate = manufactureDate,
                ProductionStage = Models.Entity.ProductionStage.Mixing,
                CreatedBy = "Admin",
                ModifiedBy = "Admin"
            });
            _context.SaveChanges();
            return true;
        }

        public bool AddReadyStockTobacco(int quantity, DateTime manufactureDate)
        {
            _context.Add(new Manufacture
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Quantity = quantity,
                ManufactureDate = manufactureDate,
                ProductionStage = Models.Entity.ProductionStage.Complete,
                CreatedBy = "Admin",
                ModifiedBy = "Admin"
            });
            _context.SaveChanges();
            return true;
        }

        public int GetReadyStockBalance(DateTime endDate)
        {
            return Decimal.ToInt32(_context.Manufacturing
                     .Where(x => x.ProductionStage == Models.Entity.ProductionStage.Complete && x.ManufactureDate <= endDate)
                     .Sum(x => x.Quantity));
        }

        public bool RemoveReadyStock(int quantity, DateTime manufactureDate)
        {
            _context.Add(new Manufacture
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Quantity = -quantity,
                ManufactureDate = manufactureDate,
                ProductionStage = Models.Entity.ProductionStage.Complete,
                CreatedBy = "Admin",
                ModifiedBy = "Admin"
            });
            _context.SaveChangesAsync();
            return true;
        }

        public int GetSoldStockBalanceByDate(DateTime endDate)
        {
            return Decimal.ToInt32(_context.Manufacturing
                     .Where(x => x.ProductionStage == Models.Entity.ProductionStage.Sold && x.ManufactureDate <= endDate)
                     .Sum(x => x.Quantity));
        }

        public List<Manufacture> GetManufactureItemsByDateAndType(Models.Entity.ProductionStage stage, DateTime? startDate, DateTime? endDate)
        {
            if(startDate == null || endDate == null)
            {
                return _context.Manufacturing
                    .Where(x => x.ProductionStage == stage)
                    .OrderByDescending(x => x.ManufactureDate).ToList();
            }
            else
            {
                return _context.Manufacturing
                   .Where(x => x.ProductionStage == stage &&
                   x.ManufactureDate >= startDate &&
                   x.ManufactureDate <= endDate)
                   .OrderByDescending(x => x.ManufactureDate).ToList();
            }
        }

        public bool AddSoldStock(int quantity, DateTime manufactureDate)
        {
            _context.Add(new Manufacture
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Quantity = quantity,
                ManufactureDate = manufactureDate,
                ProductionStage = Models.Entity.ProductionStage.Sold,
                CreatedBy = "Admin",
                ModifiedBy = "Admin"
            });

            _context.SaveChangesAsync();

            RemoveReadyStock(quantity, manufactureDate);
            return true;
        }

        public bool RemoveSoldStock(int quantity, DateTime manufactureDate)
        {
            _context.Add(new Manufacture
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Quantity = -quantity,
                ManufactureDate = manufactureDate,
                ProductionStage = Models.Entity.ProductionStage.Sold,
                CreatedBy = "Admin",
                ModifiedBy = "Admin"
            });
            _context.SaveChangesAsync();
            AddReadyStockTobacco(quantity, manufactureDate);
            return true;
        }

    }
}
