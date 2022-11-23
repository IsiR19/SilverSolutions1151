using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SilverSolutions1151.Data;
using SilverSolutions1151.Data.Entity;

namespace SilverSolutions1151.Controllers
{
    public class PackagingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PackagingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Packaging
        public async Task<IActionResult> Index()
        {
            return View(await _context.Packaging.ToListAsync());
        }

        // GET: Packaging/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Packaging == null)
            {
                return NotFound();
            }

            var packaging = await _context.Packaging
                .FirstOrDefaultAsync(m => m.Id == id);
            if (packaging == null)
            {
                return NotFound();
            }

            return View(packaging);
        }

        // GET: Packaging/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Packaging/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Quantity")] Packaging packaging)
        {
            if (ModelState.IsValid)
            {
                packaging.Id = Guid.NewGuid();
                _context.Add(packaging);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(packaging);
        }

        // GET: Packaging/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Packaging == null)
            {
                return NotFound();
            }

            var packaging = await _context.Packaging.FindAsync(id);
            if (packaging == null)
            {
                return NotFound();
            }
            return View(packaging);
        }

        // POST: Packaging/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Quantity")] Packaging packaging)
        {
            if (id != packaging.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(packaging);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackagingExists(packaging.Id))
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
            return View(packaging);
        }

        // GET: Packaging/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Packaging == null)
            {
                return NotFound();
            }

            var packaging = await _context.Packaging
                .FirstOrDefaultAsync(m => m.Id == id);
            if (packaging == null)
            {
                return NotFound();
            }

            return View(packaging);
        }

        // POST: Packaging/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Packaging == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Packaging'  is null.");
            }
            var packaging = await _context.Packaging.FindAsync(id);
            if (packaging != null)
            {
                _context.Packaging.Remove(packaging);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PackagingExists(Guid id)
        {
            return _context.Packaging.Any(e => e.Id == id);
        }
    }
}