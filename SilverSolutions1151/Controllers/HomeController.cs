using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SilverSolutions1151.Data;
using SilverSolutions1151.Data.Entity;
using SilverSolutions1151.Models;
using SilverSolutions1151.Models.Entity;
using System.Diagnostics;

namespace SilverSolutions1151.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ProductionReport productReport = new ProductionReport();
            
                productReport.OpeningBalance = _context.ManufacturingStage
                    .Where(x => x.ProductionStage == ProductionStage.RawTobacco)
                    .Where(x => x.CreatedDate == DateTime.Now.AddDays(-1))
                    .Sum(x => x.Quantity);
                productReport.InProgress = _context.ManufacturingStage
                    .Where(x => x.ProductionStage == ProductionStage.Mixing)
                    .Sum(x => x.Quantity);
                productReport.Packing = _context.ManufacturingStage.Where(x => x.ProductionStage == ProductionStage.Packing)
                    .Sum(x => x.Quantity);
                productReport.ReadyStockk = _context.ManufacturingStage
                    .Where(x => x.ProductionStage == ProductionStage.Complete)
                    .Sum(x => x.Quantity);
                productReport.Sold = _context.ManufacturingStage
                    .Where(x => x.ProductionStage == ProductionStage.Sold)
                    .Sum(x => x.Quantity);
            
            return View(productReport);
        }
        [HttpPost]
        public IActionResult Index(DateTime? SearchDate)
        {
            ProductionReport productReport = new ProductionReport();
            if (SearchDate.HasValue)
            {
                DateTime search = SearchDate.Value.AddHours(23).AddMinutes(59).AddSeconds(59);

                productReport.OpeningBalance = _context.ManufacturingStage
                    .Where(x => x.ProductionStage == ProductionStage.RawTobacco && x.CreatedDate <= search)
                    .Sum(x => x.Quantity);
                productReport.InProgress = _context.ManufacturingStage
                    .Where(x => x.ProductionStage == ProductionStage.Mixing && x.CreatedDate <= search)
                    .Sum(x => x.Quantity);
                productReport.Packing = _context.ManufacturingStage
                    .Where(x => x.ProductionStage == ProductionStage.Packing && x.CreatedDate <= search)
                    .Sum(x => x.Quantity);
                productReport.ReadyStockk = _context.ManufacturingStage
                    .Where(x => x.ProductionStage == ProductionStage.Complete && x.CreatedDate <= search)
                    .Sum(x => x.Quantity);
                productReport.Sold = _context.ManufacturingStage
                    .Where(x => x.ProductionStage == ProductionStage.Sold && x.CreatedDate <= search)
                    .Sum(x => x.Quantity);
            }
            else
            {
                productReport.OpeningBalance = _context.ManufacturingStage
                    .Where(x => x.ProductionStage == ProductionStage.RawTobacco)
                    .Sum(x => x.Quantity);
                productReport.InProgress = _context.ManufacturingStage
                    .Where(x => x.ProductionStage == ProductionStage.Mixing)
                    .Sum(x => x.Quantity);
                productReport.Packing = _context.ManufacturingStage.Where(x => x.ProductionStage == ProductionStage.Packing)
                    .Sum(x => x.Quantity);
                productReport.ReadyStockk = _context.ManufacturingStage
                    .Where(x => x.ProductionStage == ProductionStage.Complete)
                    .Sum(x => x.Quantity);
                productReport.Sold = _context.ManufacturingStage
                    .Where(x => x.ProductionStage == ProductionStage.Sold)
                    .Sum(x => x.Quantity);
            };
            return View(productReport);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}