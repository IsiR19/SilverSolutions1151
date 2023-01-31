using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SilverSolutions1151.Data;
using SilverSolutions1151.Models.Entity;

namespace SilverSolutions1151.Controllers
{
    [Authorize]
    public class InvoiceDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InvoiceDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InvoiceDetails.Include(i => i.Invoice);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InvoiceDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InvoiceDetails == null)
            {
                return NotFound();
            }

            var invoiceDetails = await _context.InvoiceDetails
                .Include(i => i.Invoice)
                .FirstOrDefaultAsync(m => m.InvoiceDetailsID == id);
            if (invoiceDetails == null)
            {
                return NotFound();
            }

            return View(invoiceDetails);
        }

        // GET: InvoiceDetails/Create
        public IActionResult Create()
        {
            ViewData["InvoiceID"] = new SelectList(_context.Invoice, "InvoiceID", "Notes");
            return View();
        }

        // POST: InvoiceDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceDetailsID,InvoiceID,Article,Qty,Price,VAT,TimeStamp")] InvoiceDetails invoiceDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InvoiceID"] = new SelectList(_context.Invoice, "InvoiceID", "Notes", invoiceDetails.InvoiceID);
            return View(invoiceDetails);
        }

        // GET: InvoiceDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InvoiceDetails == null)
            {
                return NotFound();
            }

            var invoiceDetails = await _context.InvoiceDetails.FindAsync(id);
            if (invoiceDetails == null)
            {
                return NotFound();
            }
            ViewData["InvoiceID"] = new SelectList(_context.Invoice, "InvoiceID", "Notes", invoiceDetails.InvoiceID);
            return View(invoiceDetails);
        }

        // POST: InvoiceDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InvoiceDetailsID,InvoiceID,Article,Qty,Price,VAT,TimeStamp")] InvoiceDetails invoiceDetails)
        {
            if (id != invoiceDetails.InvoiceDetailsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceDetailsExists(invoiceDetails.InvoiceDetailsID))
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
            ViewData["InvoiceID"] = new SelectList(_context.Invoice, "InvoiceID", "Notes", invoiceDetails.InvoiceID);
            return View(invoiceDetails);
        }

        // GET: InvoiceDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InvoiceDetails == null)
            {
                return NotFound();
            }

            var invoiceDetails = await _context.InvoiceDetails
                .Include(i => i.Invoice)
                .FirstOrDefaultAsync(m => m.InvoiceDetailsID == id);
            if (invoiceDetails == null)
            {
                return NotFound();
            }

            return View(invoiceDetails);
        }

        // POST: InvoiceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InvoiceDetails == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InvoiceDetails'  is null.");
            }
            var invoiceDetails = await _context.InvoiceDetails.FindAsync(id);
            if (invoiceDetails != null)
            {
                _context.InvoiceDetails.Remove(invoiceDetails);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceDetailsExists(int id)
        {
          return _context.InvoiceDetails.Any(e => e.InvoiceDetailsID == id);
        }
    }
}
