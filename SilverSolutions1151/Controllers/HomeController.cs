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
    [AllowAnonymous]
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
            if (SearchDate != null)
            {
                filterDate = (DateTime)SearchDate;
            }
            productReport.SearchDate = filterDate;

            // Set the ViewBag property
            ViewBag.FilterDate = filterDate.ToString("yyyy-MM-dd");

            // Retrieve the data for the report
            productReport.RawTobaccoBalanceCurrentDay = _rawtobaccoService.GetRawTobaccoByDate(filterDate);
            productReport.RawTobaccoBalancePreviousDay = _rawtobaccoService.GetRawTobaccoByDate(filterDate.AddDays(-1));
            //Mixed
            productReport.MixedTobaccoBalanceCurrentDay = _tabaccoMixingService.GetMixedTobaccoByDate(filterDate);
            productReport.MixedTobaccoBalancePreviousDay = _tabaccoMixingService.GetMixedTobaccoByDate(filterDate.AddDays(-1));

            //Ready Stock
            var readyStock = _readyStockService.GetReadyStockByDate(filterDate);
            var readyStockpreviousDay = _readyStockService.GetReadyStockByDate(filterDate.AddDays(-1));
            var balancesByPackagingSize = readyStock.GroupBy(b => (int)b.PackagingSize);
            var previousDayBalance = readyStockpreviousDay.GroupBy(b => b.PackagingSize);

            var balanceObjectsByPackagingSize = balancesByPackagingSize.Select(group => new Balance
            {
                PackagingSize = group.Key,
                Amount = group.Sum(b => (int)b.Quantity)
            });


            productReport.BalancesByPackagingSize = balanceObjectsByPackagingSize.ToList();


            //Sold 
            var soldStock = _soldstockService.GetSoldStockBalance(filterDate);
            var soldStockkpreviousDay = _soldstockService.GetSoldStockBalance(filterDate.AddDays(-1));
            var soldStockByPackagingSize = soldStock.GroupBy(b => (int)b.PackagingSize);
            var soldStockPreviousBalance = soldStockkpreviousDay.GroupBy(b => b.PackagingSize);

            var soldStockBalance = soldStockByPackagingSize.Select(group => new Balance
            {
                PackagingSize = group.Key,
                Amount = group.Sum(b => (int)b.Quantity)
            });


            productReport.SoldBySize = soldStockBalance.ToList();



            return View(productReport);
        }



        public IActionResult AddTobacco(decimal? quantity,DateTime? manufacturedate)
        {
            // Update the database with the new opening balance
            _rawtobaccoService.AddRawTobacco((int)quantity, (DateTime)manufacturedate);

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

        public IActionResult AddPackaging(decimal? molasesQty, DateTime? packagingDate, decimal? packagingSize)
        {
            // Update the database with the new opening balance
            _readyStockService.AddReadyStock((int)molasesQty, (DateTime)packagingDate, (int)packagingSize);

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