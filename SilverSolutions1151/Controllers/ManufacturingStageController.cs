using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SilverSolutions1151.Data;
using SilverSolutions1151.Data.Entity;
using X.PagedList;

namespace SilverSolutions1151.Controllers
{
    [Authorize]
    public class ManufacturingStageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManufacturingStageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ManufacturingStage
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var applicationDbContext = _context.ManufacturingStage
                .Include(m => m.ProductType)
                .Include(m => m.Ingredient);
           // return View(await applicationDbContext.OrderByDescending(x=> x.CreatedDate).OrderBy(x=> x.ProductType).ToListAsync());

            var ManufacturingStage = from s in applicationDbContext
                           select s;
          
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(ManufacturingStage.ToPagedList(pageNumber, pageSize));
        }

        // GET: ManufacturingStage/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.ManufacturingStage == null)
            {
                return NotFound();
            }

            var manufacturingStage = await _context.ManufacturingStage
                .Include(m => m.ProductType)
                .Include(m => m.Ingredient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manufacturingStage == null)
            {
                return NotFound();
            }

            return View(manufacturingStage);
        }

        // GET: ManufacturingStage/Create
        public IActionResult Create()
        {
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "Id", "Name");
            return View();
        }

        // POST: ManufacturingStage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductionStage,ProductTypeId,Quantity,CreatedDate")] ManufacturingStage manufacturingStage)
        {

            
                decimal producedQuantity = manufacturingStage.Quantity;
                manufacturingStage.Id = Guid.NewGuid();
                
                    var ingredients = _context.Ingredients.Where(x => x.ProductTypeId == manufacturingStage.ProductTypeId);
                    {
                        foreach (var ingredient in ingredients)
                        {

                            decimal calculated = ((decimal)ingredient.Ratio / 100m) * manufacturingStage.Quantity;

                           if (ingredient.Description == "Tobacco")
                            {
                                manufacturingStage.IngredientId = ingredient.Id;
                            }
                            else
                            {
                        if(manufacturingStage.ProductionStage == ProductionStage.Mixing)
                        {
                            producedQuantity += (decimal)calculated;
                            _context.Add(new ManufacturingStage
                            {
                                Id = Guid.NewGuid(),
                                CreatedDate = DateTime.Now,
                                Quantity = (decimal)calculated,
                                ProductTypeId = manufacturingStage.ProductTypeId,
                                IngredientId = ingredient.Id,
                                ProductionStage = manufacturingStage.ProductionStage
                            });
                        }
                            
                                
                               
                            }

                        }
                

                await _context.SaveChangesAsync();
                _context.Add(manufacturingStage);
                await _context.SaveChangesAsync();

                switch (manufacturingStage.ProductionStage)
                {
                    case ProductionStage.Mixing:
                        var updatedManufacture = new ManufacturingStage
                        {
                            Id = Guid.NewGuid(),
                            ProductionStage = ProductionStage.RawTobacco,
                            Quantity = -manufacturingStage.Quantity,
                            CreatedDate = manufacturingStage.CreatedDate,
                            IngredientId = manufacturingStage.IngredientId,
                            ProductTypeId = manufacturingStage.ProductTypeId
                        };

                        _context.Add(updatedManufacture);
                        await _context.SaveChangesAsync();
                        break;

                    case ProductionStage.Packing:
                        var updateCompelete = new ManufacturingStage
                        {
                            Id = Guid.NewGuid(),
                            ProductionStage = ProductionStage.Complete,
                            Quantity = -manufacturingStage.Quantity,
                            CreatedDate = manufacturingStage.CreatedDate,
                            IngredientId = manufacturingStage.IngredientId,
                            ProductTypeId = manufacturingStage.ProductTypeId
                        };

                        _context.Add(updateCompelete);
                        await _context.SaveChangesAsync();
                        break;

                    case ProductionStage.Complete:
                        var updateMixing = new ManufacturingStage
                        {
                            Id = Guid.NewGuid(),
                            ProductionStage = ProductionStage.Mixing,
                            Quantity = -manufacturingStage.Quantity,
                            CreatedDate = manufacturingStage.CreatedDate,
                            IngredientId = manufacturingStage.IngredientId,
                             ProductTypeId = manufacturingStage.ProductTypeId
                        };

                        _context.Add(updateMixing);
                        await _context.SaveChangesAsync();
                        break;
                     
                       case ProductionStage.Sold:
                        var updatePacking = new ManufacturingStage
                        {
                            Id = Guid.NewGuid(),
                            ProductionStage = ProductionStage.Packing,
                            Quantity = -manufacturingStage.Quantity,
                            CreatedDate = manufacturingStage.CreatedDate,
                            IngredientId = manufacturingStage.IngredientId,
                            ProductTypeId = manufacturingStage.ProductTypeId
                        };

                        _context.Add(updatePacking);
                        await _context.SaveChangesAsync();
                        break;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "Id", "Name", manufacturingStage.ProductType);
            ViewData["IngredientId"] = new SelectList(_context.ProductType, "Id", "Name", manufacturingStage.Ingredient);
            return View(manufacturingStage);
        }

        // GET: ManufacturingStage/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.ManufacturingStage == null)
            {
                return NotFound();
            }

            var manufacturingStage = await _context.ManufacturingStage.FindAsync(id);
            if (manufacturingStage == null)
            {
                return NotFound();
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "Id", "Name", manufacturingStage.ProductType);
            ViewData["IngredientId"] = new SelectList(_context.ProductType, "Id", "Name", manufacturingStage.Ingredient);
            return View(manufacturingStage);
        }

        // POST: ManufacturingStage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ProductionStage,ProductTypeId,Quantity,CreatedDate")] ManufacturingStage manufacturingStage)
        {
            if (id != manufacturingStage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manufacturingStage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManufacturingStageExists(manufacturingStage.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "Id", "Name", manufacturingStage.ProductType);
            ViewData["IngredientId"] = new SelectList(_context.ProductType, "Id", "Name", manufacturingStage.Ingredient);
            return View(manufacturingStage);
        }

        // GET: ManufacturingStage/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.ManufacturingStage == null)
            {
                return NotFound();
            }

            var manufacturingStage = await _context.ManufacturingStage
                .Include(m => m.ProductType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manufacturingStage == null)
            {
                return NotFound();
            }

            return View(manufacturingStage);
        }

        // POST: ManufacturingStage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.ManufacturingStage == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ManufacturingStage'  is null.");
            }
            var manufacturingStage = await _context.ManufacturingStage.FindAsync(id);
            if (manufacturingStage != null)
            {
                _context.ManufacturingStage.Remove(manufacturingStage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManufacturingStageExists(Guid id)
        {
          return _context.ManufacturingStage.Any(e => e.Id == id);
        }
    }
}
