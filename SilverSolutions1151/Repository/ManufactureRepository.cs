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
        public decimal GetRawTobaccoBalance(DateTime endDate)
        {
            {
                return _context.Manufacturing
                    .Where(x => x.ProductionStage == Models.Entity.ProductionStage.RawTobacco && x.ManufactureDate <= endDate)
                    .Sum(x => x.Quantity);
            }
        }
        public bool AddRawTobacco(decimal quantity, DateTime manufactureDate)
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

        public decimal GetManufactureBalance(DateTime endDate)
        {
            {
                return (_context.ManufacturingStage
                    .Where(x => x.ProductionStage == ProductionStage.RawTobacco && x.CreatedDate <= endDate)
                    .Sum(x => x.Quantity));
            }
        }

        public bool AddMixedTobacco(TobaccoMixture tobaccoMixture, DateTime manufactureDate)
        {
            _context.Add(new Manufacture
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Quantity = tobaccoMixture.MixtureTotal,
                Glycerine = tobaccoMixture.GlycerinQty,
                Syrup = tobaccoMixture.SyrupQty,
                Flavour = tobaccoMixture.FlavorQty,
                Preservatives = tobaccoMixture.PreservativeQty,
                Tobacco = tobaccoMixture.Tobacco,
                ManufactureDate = manufactureDate,
                ProductionStage = Models.Entity.ProductionStage.Mixing,
                CreatedBy = "Admin",
                ModifiedBy = "Admin"
            });
            _context.SaveChanges();
            return true;
        }

        public bool RemoveRawTobacco(decimal quantity, DateTime manufactureDate)
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

        public decimal GetMixedTobaccoBalance(DateTime endDate)
        {
            return (_context.Manufacturing
                     .Where(x => x.ProductionStage == Models.Entity.ProductionStage.Mixing && x.ManufactureDate <= endDate)
                     .Sum(x => x.Quantity));
        }

        public bool RemoveMixedTobacco(decimal quantity, DateTime manufactureDate)
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

        public bool AddReadyStockTobacco(decimal quantity, decimal pacakgeSize, DateTime manufactureDate)
        {
            _context.Add(new Manufacture
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Quantity = quantity,
                PackagingSize = pacakgeSize,
                ManufactureDate = manufactureDate,
                ProductionStage = Models.Entity.ProductionStage.Complete,
                CreatedBy = "Admin",
                ModifiedBy = "Admin"
            });
            _context.SaveChanges();
            return true;
        }

        public decimal GetReadyStockBalanceByDate(DateTime endDate)
        {
            return (_context.Manufacturing
                     .Where(x => x.ProductionStage == Models.Entity.ProductionStage.Complete && x.ManufactureDate <= endDate)
                     .Sum(x => x.Quantity));
        }

        public List<Manufacture> GetReadyStockBalance(DateTime endDate)
        {
            return _context.Manufacturing
                     .Where(x => x.ProductionStage == Models.Entity.ProductionStage.Complete && x.ManufactureDate <= endDate).ToList();
        }

        public bool RemoveReadyStock(decimal quantity, decimal packageSize, DateTime manufactureDate)
        {
            _context.Add(new Manufacture
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Quantity = -quantity,
                PackagingSize = packageSize,
                ManufactureDate = manufactureDate,
                ProductionStage = Models.Entity.ProductionStage.Complete,
                CreatedBy = "Admin",
                ModifiedBy = "Admin"
            });
            _context.SaveChanges();
            return true;
        }

        public decimal GetSoldStockBalanceByDate(DateTime endDate)
        {
            return (_context.Manufacturing
                     .Where(x => x.ProductionStage == Models.Entity.ProductionStage.Sold && x.ManufactureDate <= endDate)
                     .Sum(x => x.Quantity));
        }

        public decimal GetSoldStockBalanceByDate(DateTime startDate,DateTime endDate)
        {
            return (_context.Manufacturing
                     .Where(x => x.ProductionStage == Models.Entity.ProductionStage.Sold && x.ManufactureDate >= startDate && x.ManufactureDate <= endDate)
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

        public bool AddSoldStock(decimal quantity, decimal packagingSize, DateTime manufactureDate)
        {
            _context.Add(new Manufacture
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Quantity = quantity,
                PackagingSize = packagingSize,
                ManufactureDate = manufactureDate,
                ProductionStage = Models.Entity.ProductionStage.Sold,
                CreatedBy = "Admin",
                ModifiedBy = "Admin"
            });

            _context.SaveChanges();
            return true;
        }

        public bool RemoveSoldStock(decimal quantity, decimal packagingSize, DateTime manufactureDate)
        {
            _context.Add(new Manufacture
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Quantity = -quantity,
                PackagingSize= packagingSize,
                ManufactureDate = manufactureDate,
                ProductionStage = Models.Entity.ProductionStage.Sold,
                CreatedBy = "Admin",
                ModifiedBy = "Admin"
            });
            _context.SaveChanges();
            AddReadyStockTobacco(quantity,0, manufactureDate);
            return true;
        }

        public List<Manufacture> GetSoldStockBalance(DateTime startDate,DateTime endDate)
        {
            return _context.Manufacturing
                     .Where(x => x.ProductionStage == Models.Entity.ProductionStage.Sold && x.ManufactureDate >= startDate && x.ManufactureDate <= endDate).ToList();
        }
    }
}
