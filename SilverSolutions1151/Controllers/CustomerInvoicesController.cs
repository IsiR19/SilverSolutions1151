using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SilverSolutions1151.Data;
using SilverSolutions1151.Middleware.Services.Interfaces;
using SilverSolutions1151.Models;
using SilverSolutions1151.Models.Entity;
using SilverSolutions1151.Models.Paganation;

namespace SilverSolutions1151.Controllers
{
    public class CustomerInvoicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ISalesService _salesService;

        public CustomerInvoicesController(ApplicationDbContext context,ISalesService salesService)
        {
            _context = context;
            _salesService = salesService;
        }

        // GET: CustomerInvoices
        //public async Task<IActionResult> Index()
        //{
        //    if (_context.CustomerInvoice != null)
        //    {
        //        return View(await _context.CustomerInvoice.OrderByDescending(invoice => invoice.InvoiceDate).ToListAsync());
        //    }
        //    else
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.CustomerInvoice' is null.");
        //    }
        //}

        public async Task<IActionResult> Index(string invoiceNumber,
     DateTime? fromDate,
     DateTime? fromDateFilter,
     DateTime? toDateFilter,
     DateTime? toDate,
     int? pageNumber,
     string currentFilter)
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



        // GET: CustomerInvoices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.CustomerInvoice == null)
            {
                return NotFound();
            }

            var customerInvoice = await _context.CustomerInvoice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerInvoice == null)
            {
                return NotFound();
            }

            return View(customerInvoice);
        }

        // GET: CustomerInvoices/Create
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: CustomerInvoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceId,InvoiceNumber,InvoiceDate,CustomerName,CustomerPhone,CustomerAddress,Vat,CreatedBy,ModifiedBy,CreatedModifiedDate,Id,CreatedDate,IsDeleted,InvoiceItems")] CustomerInvoice customerInvoice)
        {
            //Add validation here for package size
            _salesService.CreateInvoice(customerInvoice); 
                return RedirectToAction(nameof(Index));
           
        }


        // GET: CustomerInvoices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.CustomerInvoice == null)
            {
                return NotFound();
            }

            var customerInvoice = await _context.CustomerInvoice
                .Include(ci => ci.InvoiceItems)
                .FirstOrDefaultAsync(ci => ci.Id == id);

            if (customerInvoice == null)
            {
                return NotFound();
            }

            return View(customerInvoice);
        }

        // POST: CustomerInvoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: CustomerInvoices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.CustomerInvoice == null)
            {
                return NotFound();
            }

            var customerInvoice = await _context.CustomerInvoice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerInvoice == null)
            {
                return NotFound();
            }

            return View(customerInvoice);
        }

        // POST: CustomerInvoices/Delete/5
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
    }
}
