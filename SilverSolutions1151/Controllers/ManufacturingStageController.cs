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
    public class ManufacturingStageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManufacturingStageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ManufacturingStage
        public async Task<IActionResult> Index()
        {
              return View(await _context.ManufacturingStage.ToListAsync());
        }

        // GET: ManufacturingStage/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.ManufacturingStage == null)
            {
                return NotFound();
            }

            var manufacturingStage = await _context.ManufacturingStage
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
            return View();
        }

        // POST: ManufacturingStage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductionStage,Quantity,CreatedDate")] ManufacturingStage manufacturingStage)
        {
            if (ModelState.IsValid)
            {
                manufacturingStage.Id = Guid.NewGuid();
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
                        };

                        _context.Add(updatedManufacture);
                        await _context.SaveChangesAsync();
                        break;

                    case ProductionStage.Packing:
                        var updateMixing = new ManufacturingStage
                        {
                            Id = Guid.NewGuid(),
                            ProductionStage = ProductionStage.Mixing,
                            Quantity = -manufacturingStage.Quantity,
                            CreatedDate = manufacturingStage.CreatedDate,
                        };

                        _context.Add(updateMixing);
                        await _context.SaveChangesAsync();
                        break;

                    case ProductionStage.Complete:
                        var updatePacking = new ManufacturingStage
                        {
                            Id = Guid.NewGuid(),
                            ProductionStage = ProductionStage.Packing,
                            Quantity = -manufacturingStage.Quantity,
                            CreatedDate = manufacturingStage.CreatedDate,
                        };

                        _context.Add(updatePacking);
                        await _context.SaveChangesAsync();
                        break;

                }

                return RedirectToAction(nameof(Index));
            }
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
            return View(manufacturingStage);
        }

        // POST: ManufacturingStage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ProductionStage,Quantity,CreatedDate")] ManufacturingStage manufacturingStage)
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
