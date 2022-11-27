using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SilverSolutions1151.Data;
using SilverSolutions1151.Data.Entity;
using SilverSolutions1151.Models.Entity;

namespace SilverSolutions1151.Controllers
{
    public class PackingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PackingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Packing
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Packing.Include(p => p.Catalog);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Packing/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Packing == null)
            {
                return NotFound();
            }

            var packing = await _context.Packing
                .Include(p => p.Catalog)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (packing == null)
            {
                return NotFound();
            }

            return View(packing);
        }

        // GET: Packing/Create
        public IActionResult Create()
        {
            ViewData["CatalogId"] = new SelectList(_context.Catalog, "Id", "Name");
            return View();
        }

        // POST: Packing/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CatalogId,Quantity")] Packing packing)
        {
           
                packing.Id = Guid.NewGuid();

                _context.Add(packing);
                await _context.SaveChangesAsync();

                //check quantity and update manufacturing stage
                var catalogItem =  await _context.Catalog.FindAsync(packing.CatalogId);
            if(catalogItem!= null)
            {
                var productType = await _context.ProductType.FindAsync(catalogItem.ProductTypeId);
                var packaging = await _context.Packaging.FindAsync(productType.PackagingId);
                var ingredients = _context.Ingredients.Where(x => x.ProductTypeId == productType.Id);
                var ingredientId = Guid.Empty;
                foreach (var ingredient in ingredients)
                {
                    if(ingredient.Description == "Tobacco")
                    {
                        ingredientId = ingredient.Id;
                    }
                }
     
                    decimal tobaccoUsed = ((decimal)packaging.Quantity * packing.Quantity) / 3.2M;
                    var manufacturingStage = new ManufacturingStage
                    {
                        Id = Guid.NewGuid(),
                        ProductionStage = ProductionStage.Complete,
                        Quantity = tobaccoUsed,
                        IngredientId = ingredientId,
                        ProductTypeId = productType.Id,
                        CreatedDate = DateTime.Now
                    };

                    _context.Add(manufacturingStage);
                    await _context.SaveChangesAsync();

                    var updatemanufacturingStage = new ManufacturingStage
                    {
                        Id = Guid.NewGuid(),
                        ProductionStage = ProductionStage.Packing,
                        Quantity = -tobaccoUsed,
                        IngredientId = ingredientId,
                        ProductTypeId = productType.Id,
                        CreatedDate = DateTime.Now
                    };

                    _context.Add(updatemanufacturingStage);
                    await _context.SaveChangesAsync();
                }
            
            return RedirectToAction(nameof(Index));
            
            ViewData["CatalogId"] = new SelectList(_context.Catalog, "Id", "Name", packing.CatalogId);
            return View(packing);
        }

        // GET: Packing/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Packing == null)
            {
                return NotFound();
            }

            var packing = await _context.Packing.FindAsync(id);
            if (packing == null)
            {
                return NotFound();
            }
            ViewData["CatalogId"] = new SelectList(_context.Catalog, "Id", "Name", packing.CatalogId);
            return View(packing);
        }

        // POST: Packing/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CatalogId,Quantity")] Packing packing)
        {
            if (id != packing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(packing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackingExists(packing.Id))
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
            ViewData["CatalogId"] = new SelectList(_context.Catalog, "Id", "Name", packing.CatalogId);
            return View(packing);
        }

        // GET: Packing/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Packing == null)
            {
                return NotFound();
            }

            var packing = await _context.Packing
                .Include(p => p.Catalog)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (packing == null)
            {
                return NotFound();
            }

            return View(packing);
        }

        // POST: Packing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Packing == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Packing'  is null.");
            }
            var packing = await _context.Packing.FindAsync(id);
            if (packing != null)
            {
                _context.Packing.Remove(packing);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PackingExists(Guid id)
        {
          return _context.Packing.Any(e => e.Id == id);
        }
    }
}
