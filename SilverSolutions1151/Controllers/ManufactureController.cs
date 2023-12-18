using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SilverSolutions1151.Data;
using SilverSolutions1151.Data.Entity;
using SilverSolutions1151.Models.Entity;
using SilverSolutions1151.Models.Paganation;
using SilverSolutions1151.Repository.Interfaces;
using X.PagedList;

namespace SilverSolutions1151.Controllers
{
    [Authorize]
    public class ManufactureController : Controller
    {
        private readonly IManufactureRepository _manufactureRepository;

        public ManufactureController(IManufactureRepository manufactureRepository)
        {
            _manufactureRepository = manufactureRepository;
        }

        // GET: ManufacturingStage
        public async Task<IActionResult> Index(string stage,
            DateTime? fromDate,
            DateTime? fromDateFilter,
            DateTime? toDateFilter,
            DateTime? toDate,
            int? pageNumber, string currentFilter)
        {
            if (fromDate != null && toDate != null || !String.IsNullOrEmpty(stage))
            {
                pageNumber = 1;
                if (String.IsNullOrEmpty(stage))
                    stage = TempData["stage"]?.ToString() ?? string.Empty;
                else
                    TempData["stage"] = stage;
            }
            else
            {
                fromDate = fromDateFilter;
                toDate = toDateFilter;
                stage = currentFilter;
            }


            TempData["CurrentFilter"] = stage;
            TempData["fromDateFilter"] = fromDate;
            TempData["toDateFilter"] = toDate;
            var manufacturing = new List<Manufacture>();
            if (stage == "MixingTotal")
                stage = "Mixing";
            if (Enum.TryParse(typeof(Models.Entity.ProductionStage),stage,out var result))
            {
                manufacturing = _manufactureRepository.GetManufactureItemsByDateAndType((Models.Entity.ProductionStage)result, fromDate, toDate);
            }
            
            
            
            var viewmodel = await PaginatedList<Manufacture>.CreateAsync(manufacturing.AsQueryable().AsNoTracking(), pageNumber ?? 1, 20);
     
            return View("Index", viewmodel);
        }

        public async Task<IActionResult> Mixing(string stage,
           DateTime? fromDate,
           DateTime? fromDateFilter,
           DateTime? toDateFilter,
           DateTime? toDate,
           int? pageNumber, string currentFilter)
        {
            if (fromDate != null && toDate != null || !String.IsNullOrEmpty(stage))
            {
                pageNumber = 1;
                if (String.IsNullOrEmpty(stage))
                    stage = TempData["stage"].ToString();
                else
                    TempData["stage"] = stage;
            }
            else
            {
                fromDate = fromDateFilter;
                toDate = toDateFilter;
                stage = currentFilter;
            }


            TempData["CurrentFilter"] = stage;
            TempData["fromDateFilter"] = fromDate;
            TempData["toDateFilter"] = toDate;
            var manufacturing = new List<Manufacture>();
            if (Enum.TryParse(typeof(Models.Entity.ProductionStage), stage, out var result))
            {
                manufacturing = _manufactureRepository.GetManufactureItemsByDateAndType((Models.Entity.ProductionStage)result, fromDate, toDate);
            }


            var viewmodel = await PaginatedList<Manufacture>.CreateAsync(manufacturing.AsQueryable().AsNoTracking(), pageNumber ?? 1, 20);

            return View(viewmodel);
        }

        public async Task<IActionResult> Packaging(string stage,
    DateTime? fromDate,
    DateTime? fromDateFilter,
    DateTime? toDateFilter,
    DateTime? toDate,
    int? pageNumber, string currentFilter)
        {
            // Check if search parameters are provided
            if (fromDate != null && toDate != null || !String.IsNullOrEmpty(stage))
            {
                // Reset page number to 1 and save search parameters to TempData
                pageNumber = 1;
                if (String.IsNullOrEmpty(stage))
                {
                    stage = TempData["stage"]?.ToString();
                }
                else
                {
                    TempData["stage"] = stage;
                }
                
                TempData["fromDateFilter"] = fromDate;
                TempData["toDateFilter"] = toDate;
            }
            else
            {
                // Use previous search parameters from TempData
                stage = TempData["stage"]?.ToString();
                fromDate = fromDateFilter ?? TempData["fromDateFilter"] as DateTime?;
                toDate = toDateFilter ?? TempData["toDateFilter"] as DateTime?;
            }

            // Save current search filter to TempData
            TempData["CurrentFilter"] = stage;

            // Get manufacturing items based on search parameters
            var manufacturing = new List<Manufacture>();
            if (Enum.TryParse(typeof(Models.Entity.ProductionStage), stage, out var result))
            {
                manufacturing = _manufactureRepository.GetManufactureItemsByDateAndType((Models.Entity.ProductionStage)result, fromDate, toDate);
            }

            // Create paginated list of manufacturing items
            var viewmodel = await PaginatedList<Manufacture>.CreateAsync(manufacturing.AsQueryable().AsNoTracking(), pageNumber ?? 1, 20);

            return View("Packaging", viewmodel);

        }




    }
}
