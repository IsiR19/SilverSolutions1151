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
            if (Enum.TryParse(typeof(Models.Entity.ProductionStage),stage,out var result))
            {
                manufacturing = _manufactureRepository.GetManufactureItemsByDateAndType((Models.Entity.ProductionStage)result, fromDate, toDate);
            }
    
            
            var viewmodel = await PaginatedList<Manufacture>.CreateAsync(manufacturing.AsQueryable().AsNoTracking(), pageNumber ?? 1, 20);

            return View(viewmodel);  
        }

        
    }
}
