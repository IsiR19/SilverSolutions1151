using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SilverSolutions1151.Data;
using SilverSolutions1151.Data.Entity;

namespace SilverSolutions1151.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IngredientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ingredients
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ingredients.Include(i => i.ProductType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Ingredients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Ingredients == null)
            {
                return NotFound();
            }

            var ingredients = await _context.Ingredients
                .Include(i => i.ProductType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredients == null)
            {
                return NotFound();
            }

            return View(ingredients);
        }

        // GET: Ingredients/Create
        public IActionResult Create()
        {
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "Id", "Name");
            return View();
        }

        // POST: Ingredients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductTypeId,Description,Ratio")] Ingredients ingredients)
        {
            
                ingredients.Id = Guid.NewGuid();
                _context.Add(ingredients);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "Id", "Name", ingredients.ProductTypeId);
            return View(ingredients);
        }

        // GET: Ingredients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Ingredients == null)
            {
                return NotFound();
            }

            var ingredients = await _context.Ingredients.FindAsync(id);
            if (ingredients == null)
            {
                return NotFound();
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "Id", "Name", ingredients.ProductTypeId);
            return View(ingredients);
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ProductTypeId,Description,Ratio")] Ingredients ingredients)
        {
            if (id != ingredients.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingredients);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientsExists(ingredients.Id))
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
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "Id", "Name", ingredients.ProductTypeId);
            return View(ingredients);
        }

        // GET: Ingredients/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Ingredients == null)
            {
                return NotFound();
            }

            var ingredients = await _context.Ingredients
                .Include(i => i.ProductType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredients == null)
            {
                return NotFound();
            }

            return View(ingredients);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Ingredients == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Ingredients'  is null.");
            }
            var ingredients = await _context.Ingredients.FindAsync(id);
            if (ingredients != null)
            {
                _context.Ingredients.Remove(ingredients);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredientsExists(Guid id)
        {
          return _context.Ingredients.Any(e => e.Id == id);
        }
    }
}
