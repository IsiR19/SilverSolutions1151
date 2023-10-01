using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SilverSolutions1151.Data;
using SilverSolutions1151.Data.Entity;
using SilverSolutions1151.Middleware.Services.Interfaces;
using SilverSolutions1151.Migrations;
using SilverSolutions1151.Models;
using SilverSolutions1151.Models.Entity;
using System.Diagnostics;

namespace SilverSolutions1151.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IRawTobaccoService _rawtobaccoService;
        private readonly ITobaccoMixingService _tabaccoMixingService;
        private readonly IReadyStockService _readyStockService;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context,
            IRawTobaccoService rawTobaccoService, ITobaccoMixingService tabaccoMixingService, IReadyStockService readyStockService)
        {
            _logger = logger;
            _context = context;
            _rawtobaccoService = rawTobaccoService;
            _tabaccoMixingService = tabaccoMixingService;
            _readyStockService = readyStockService;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            ProductionReport productReport = new ProductionReport();


            productReport.RawTobaccoBalanceCurrentDay = _rawtobaccoService.GetRawTobaccoByDate(DateTime.Now);
            productReport.RawTobaccoBalancePreviousDay = _rawtobaccoService.GetRawTobaccoByDate(DateTime.Now.AddDays(-1));
            //Mixed
            productReport.MixedTobaccoBalanceCurrentDay = _tabaccoMixingService.GetMixedTobaccoByDate(DateTime.Now);
            productReport.MixedTobaccoBalancePreviousDay = _tabaccoMixingService.GetMixedTobaccoByDate(DateTime.Now.AddDays(-1));
            //Ready Stock
            productReport.ReadyStockBalanceCurrentDay = _readyStockService.GetReadyStockByDate(DateTime.Now);
            productReport.ReadyStockBalancePreviousDay = _readyStockService.GetReadyStockByDate(DateTime.Now.AddDays(-1));
            
            return View(productReport);
        }
        [HttpPost]
        public IActionResult Index(DateTime? SearchDate)
        {
            ProductionReport productReport = new ProductionReport();
            if (SearchDate.HasValue)
            {
                DateTime search = SearchDate.Value.AddHours(23).AddMinutes(59).AddSeconds(59);

                productReport.OpeningBalance = _rawtobaccoService.GetRawTobaccoByDate((DateTime)SearchDate);
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
      
        public IActionResult AddTobacco(decimal? openingBalance,DateTime? manufacturedate)
        {
            // Update the database with the new opening balance
            _rawtobaccoService.AddRawTobacco((int)openingBalance, (DateTime)manufacturedate);

            // Return the view 
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddManufacturing(decimal? manufacturingQty, DateTime? manufacturedate,decimal? ingredientQty)
        {
            // Update the database with the new opening balance
            _tabaccoMixingService.AddTobaccoMixing((int)manufacturingQty, (DateTime)manufacturedate, (int)ingredientQty);

            // Return the view 
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddPackaging(decimal? quantityInGrams, DateTime? packingDate, decimal? numberOfBoxes)
        {
            // Update the database with the new opening balance
            _readyStockService.AddReadyStock((int)quantityInGrams, (DateTime)packingDate, (int)numberOfBoxes);

            // Return the view 
            return RedirectToAction("Index", "Home");
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