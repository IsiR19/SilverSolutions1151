using Microsoft.AspNetCore.Mvc;
using SilverSolutions1151.Data;
using SilverSolutions1151.Data.Entity;
using SilverSolutions1151.Models;
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
            ProductionReport productReport = new ProductionReport
            {
                OpeningBalance = _context.ManufacturingStage.Where(x => x.ProductionStage == ProductionStage.RawTobacco).Sum(x => x.Quantity),
                InProgress = _context.ManufacturingStage.Where(x => x.ProductionStage == ProductionStage.Mixing).Sum(x => x.Quantity),
                Packing = _context.ManufacturingStage.Where(x => x.ProductionStage == ProductionStage.Packing).Sum(x => x.Quantity),
                ReadyStockk = _context.ManufacturingStage.Where(x => x.ProductionStage == ProductionStage.Complete).Sum(x => x.Quantity),
                Sold = _context.ManufacturingStage.Where(x => x.ProductionStage == ProductionStage.Sold).Sum(x => x.Quantity)
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