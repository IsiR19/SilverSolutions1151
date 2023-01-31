using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SilverSolutions1151.Data;
using SilverSolutions1151.Data.Entity;

namespace SilverSolutions1151.Controllers
{
    [Authorize]
    public class ProductionReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductionReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductionReport
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductionReport.ToListAsync());
        }

        // GET: ProductionReport/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.ProductionReport == null)
            {
                return NotFound();
            }

            var productionReport = await _context.ProductionReport
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productionReport == null)
            {
                return NotFound();
            }

            return View(productionReport);
        }

        // GET: ProductionReport/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductionReport/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OpeningBalance,ClosedBalance,InProgress,ReadyStockk,Sold,Packing,Id,CreatedDate,IsDeleted")] ProductionReport productionReport)
        {
            if (ModelState.IsValid)
            {
                productionReport.Id = Guid.NewGuid();
                _context.Add(productionReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productionReport);
        }

        // GET: ProductionReport/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.ProductionReport == null)
            {
                return NotFound();
            }

            var productionReport = await _context.ProductionReport.FindAsync(id);
            if (productionReport == null)
            {
                return NotFound();
            }
            return View(productionReport);
        }

        // POST: ProductionReport/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OpeningBalance,ClosedBalance,InProgress,ReadyStockk,Sold,Packing,Id,CreatedDate,IsDeleted")] ProductionReport productionReport)
        {
            if (id != productionReport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productionReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductionReportExists(productionReport.Id))
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
            return View(productionReport);
        }

        // GET: ProductionReport/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.ProductionReport == null)
            {
                return NotFound();
            }

            var productionReport = await _context.ProductionReport
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productionReport == null)
            {
                return NotFound();
            }

            return View(productionReport);
        }

        // POST: ProductionReport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.ProductionReport == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProductionReport'  is null.");
            }
            var productionReport = await _context.ProductionReport.FindAsync(id);
            if (productionReport != null)
            {
                _context.ProductionReport.Remove(productionReport);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductionReportExists(Guid id)
        {
            return _context.ProductionReport.Any(e => e.Id == id);
        }
    }
}