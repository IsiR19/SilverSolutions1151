using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SilverSolutions1151.Data;
using SilverSolutions1151.Data.Entity;

namespace SilverSolutions1151.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Catalog
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Catalog.Include(c => c.ProductType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Catalog/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Catalog == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalog
                .Include(c => c.ProductType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "Id", "Name");
            return View(catalog);
        }

        // GET: Catalog/Create
        public IActionResult Create()
        {
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "Id", "Name");
            return View();
        }

        // POST: Catalog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,ProductTypeId,Price,Id,CreatedDate,IsDeleted")] Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                catalog.Id = Guid.NewGuid();
                _context.Add(catalog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "Id", "Name", catalog.ProductTypeId);
            return View(catalog);
        }

        // GET: Catalog/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Catalog == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalog.FindAsync(id);
            if (catalog == null)
            {
                return NotFound();
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "Id", "Name", catalog.ProductTypeId);
            return View(catalog);
        }

        // POST: Catalog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,ProductTypeId,Price,Id,CreatedDate,IsDeleted")] Catalog catalog)
        {
            if (id != catalog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogExists(catalog.Id))
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
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "Id", "Name", catalog.ProductTypeId);
            return View(catalog);
        }

        // GET: Catalog/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Catalog == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalog
                .Include(c => c.ProductType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }

            return View(catalog);
        }

        // POST: Catalog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Catalog == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Catalog'  is null.");
            }
            var catalog = await _context.Catalog.FindAsync(id);
            if (catalog != null)
            {
                _context.Catalog.Remove(catalog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogExists(Guid id)
        {
            return _context.Catalog.Any(e => e.Id == id);
        }
    }
}