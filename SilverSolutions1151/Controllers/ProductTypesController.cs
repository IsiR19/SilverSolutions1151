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

namespace SilverSolutions1151.Controllers
{
    //[Authorize]
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductType.Include(p => p.Packaging);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.ProductType == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductType
                .Include(p => p.Packaging)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // GET: ProductTypes/Create
        public IActionResult Create()
        {
            ViewData["PackagingId"] = new SelectList(_context.Packaging, "Id", "Name");
            return View();
        }

        // POST: ProductTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PackagingId")] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                productType.Id = Guid.NewGuid();
                _context.Add(productType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PackagingId"] = new SelectList(_context.Packaging, "Id", "Name", productType.PackagingId);
            return View(productType);
        }

        // GET: ProductTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.ProductType == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductType.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }
            ViewData["PackagingId"] = new SelectList(_context.Packaging, "Id", "Name", productType.PackagingId);
            return View(productType);
        }

        // POST: ProductTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,PackagingId")] ProductType productType)
        {
            if (id != productType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTypeExists(productType.Id))
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
            ViewData["PackagingId"] = new SelectList(_context.Packaging, "Id", "Name", productType.PackagingId);
            return View(productType);
        }

        // GET: ProductTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.ProductType == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductType
                .Include(p => p.Packaging)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // POST: ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.ProductType == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProductType'  is null.");
            }
            var productType = await _context.ProductType.FindAsync(id);
            if (productType != null)
            {
                _context.ProductType.Remove(productType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductTypeExists(Guid id)
        {
          return _context.ProductType.Any(e => e.Id == id);
        }
    }
}
