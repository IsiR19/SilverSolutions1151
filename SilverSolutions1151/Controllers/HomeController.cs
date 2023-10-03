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
        private readonly ISoldStockService _soldstockService;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context,
            IRawTobaccoService rawTobaccoService, ITobaccoMixingService tabaccoMixingService, 
            IReadyStockService readyStockService, ISoldStockService soldstockService)
        {
            _logger = logger;
            _context = context;
            _rawtobaccoService = rawTobaccoService;
            _tabaccoMixingService = tabaccoMixingService;
            _readyStockService = readyStockService;
            _soldstockService = soldstockService;
        }
        [AllowAnonymous]
        public IActionResult Index(DateTime? SearchDate)
        {
            ProductionReport productReport = new ProductionReport();
            DateTime filterDate = DateTime.Now;
            if(SearchDate != null)
            {
                filterDate = (DateTime)SearchDate;
            }
            productReport.SearchDate = filterDate;
         
            productReport.RawTobaccoBalanceCurrentDay = _rawtobaccoService.GetRawTobaccoByDate(filterDate);
            productReport.RawTobaccoBalancePreviousDay = _rawtobaccoService.GetRawTobaccoByDate(filterDate.AddDays(-1));
            //Mixed
            productReport.MixedTobaccoBalanceCurrentDay = _tabaccoMixingService.GetMixedTobaccoByDate(filterDate);
            productReport.MixedTobaccoBalancePreviousDay = _tabaccoMixingService.GetMixedTobaccoByDate(filterDate.AddDays(-1));
            //Ready Stock
            productReport.ReadyStockBalanceCurrentDay = _readyStockService.GetReadyStockByDate(filterDate);
            productReport.ReadyStockBalancePreviousDay = _readyStockService.GetReadyStockByDate(DateTime.Now.AddDays(-1));
            //Sold 
            productReport.SoldBalanceCurrentDay = _soldstockService.GeSoldByDate(filterDate);
            productReport.SoldBalancePreviousDay = _soldstockService.GeSoldByDate(filterDate.AddDays(-1));

            
            return View(productReport);
        }

      
        public IActionResult AddTobacco(decimal? openingBalance,DateTime? manufacturedate)
        {
            // Update the database with the new opening balance
            _rawtobaccoService.AddRawTobacco((int)openingBalance, (DateTime)manufacturedate);

            // Return the view 
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddManufacturing(decimal? manufacturingQty, DateTime? manufacturedate,decimal? glycerineQty, decimal? flavourQty
            , decimal? syrupQty, decimal? preservativeQty)
        {
            // Update the database with the new opening balance
            _tabaccoMixingService.AddTobaccoMixing((int)manufacturingQty, (DateTime)manufacturedate,glycerineQty,flavourQty,syrupQty,preservativeQty);

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