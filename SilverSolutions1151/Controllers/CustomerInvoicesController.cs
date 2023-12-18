using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SilverSolutions1151.Data;
using SilverSolutions1151.Middleware.Services.Interfaces;
using SilverSolutions1151.Models;
using SilverSolutions1151.Models.Entity;
using SilverSolutions1151.Models.Paganation;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SilverSolutions1151.Controllers
{
    public class CustomerInvoicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ISalesService _salesService;

        public CustomerInvoicesController(ApplicationDbContext context, ISalesService salesService)
        {
            _context = context;
            _salesService = salesService;
        }

        public async Task<IActionResult> Index(string invoiceNumber, DateTime? fromDate, DateTime? fromDateFilter, DateTime? toDateFilter, DateTime? toDate, int? pageNumber, string currentFilter)
        {
            SetPageNumberAndInvoiceNumber(ref pageNumber, ref invoiceNumber, fromDate, toDate, currentFilter);

            SetTempData(invoiceNumber, fromDate, toDate);

            var invoices = await _salesService.GetInvoiceListAsync(invoiceNumber, fromDate, toDate);
            var viewmodel = await PaginatedList<CustomerInvoice>.CreateAsync(invoices.AsQueryable().AsNoTracking(), pageNumber ?? 1, 20);

            return View(viewmodel);
        }

        private void SetPageNumberAndInvoiceNumber(ref int? pageNumber, ref string invoiceNumber, DateTime? fromDate, DateTime? toDate, string currentFilter)
        {
            if ((fromDate != null && toDate != null) || !string.IsNullOrEmpty(invoiceNumber))
            {
                pageNumber = 1;
                if (string.IsNullOrEmpty(invoiceNumber))
                {
                    invoiceNumber = TempData["invoiceNumber"]?.ToString() ?? string.Empty;
                }
                else
                {
                    TempData["invoiceNumber"] = invoiceNumber;
                }
            }
            else
            {
                invoiceNumber = currentFilter;
            }
        }

        private void SetTempData(string invoiceNumber, DateTime? fromDate, DateTime? toDate)
        {
            TempData["CurrentFilter"] = invoiceNumber;
            TempData["fromDateFilter"] = fromDate;
            TempData["toDateFilter"] = toDate;
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.CustomerInvoice == null)
            {
                return NotFound();
            }

            var customerInvoice = await _context.CustomerInvoice.FirstOrDefaultAsync(m => m.Id == id);
            if (customerInvoice == null)
            {
                return NotFound();
            }

            return View(customerInvoice);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceId,InvoiceNumber,InvoiceDate,CustomerName,CustomerPhone,CustomerAddress,Vat,CreatedBy,ModifiedBy,CreatedModifiedDate,Id,CreatedDate,IsDeleted,InvoiceItems")] CustomerInvoice customerInvoice)
        {
            _salesService.CreateInvoice(customerInvoice);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.CustomerInvoice == null)
            {
                return NotFound();
            }

            var customerInvoice = await _context.CustomerInvoice.Include(ci => ci.InvoiceItems).FirstOrDefaultAsync(ci => ci.Id == id);

            if (customerInvoice == null)
            {
                return NotFound();
            }

            return View(customerInvoice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("InvoiceId,InvoiceNumber,InvoiceDate,CustomerName,CustomerPhone,CustomerAddress,Vat,CreatedBy,ModifiedBy,CreatedModifiedDate,Id,CreatedDate,IsDeleted,InvoiceItems")] CustomerInvoice customerInvoice)
        {
            if (id != customerInvoice.Id)
            {
                return NotFound();
            }

            customerInvoice = await _salesService.UpdateInvoice(id, customerInvoice);
            return View(customerInvoice);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.CustomerInvoice == null)
            {
                return NotFound();
            }

            var customerInvoice = await _context.CustomerInvoice.FirstOrDefaultAsync(m => m.Id == id);
            if (customerInvoice == null)
            {
                return NotFound();
            }

            return View(customerInvoice);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.CustomerInvoice == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CustomerInvoice'  is null.");
            }
            var customerInvoice = await _context.CustomerInvoice.FindAsync(id);
            if (customerInvoice != null)
            {
                _context.CustomerInvoice.Remove(customerInvoice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerInvoiceExists(Guid id)
        {
            return (_context.CustomerInvoice?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Print(Guid id)
        {
            return View(await _salesService.GetInvoiceAsync(id));
        }

        public async Task<IActionResult> PrintInvoice(Guid id)
        {
            return View(await _salesService.GetInvoiceAsync(id));
        }

        public IActionResult GetCustomers(string searchTerm)
        {
            var customers = _context.Customers
                .Where(c => c.CustomerName.Contains(searchTerm))
                .Select(c => new { customerName = c.CustomerName })
                .ToList();

            return Json(customers);
        }
    }
}
