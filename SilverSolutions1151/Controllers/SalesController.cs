using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa;
using SilverSolutions1151.Data;
using SilverSolutions1151.Data.Entity;
using SilverSolutions1151.Models.Entity;

namespace SilverSolutions1151.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
              return View(await _context.Sale.ToListAsync());
        }

        public async Task<IActionResult> Print(int id)
        {
            if (id == null || _context.Sale == null)
            {
                return NotFound();
            }

            var sale = await _context.Sale.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            var details = _context.SalesDetails.Where(x => x.SalesID == id);
            if(details != null)
            {
                foreach(var item in details)
                {
                    if(item.ProductId != null)
                    {
                        var product = await _context.Products.FindAsync(item.ProductId);
                        item.ProductName = product.Name;
                    }
                    bool alreadyExist = sale.SalesDetails.Contains(item);
                    if(!alreadyExist)
                    {
                        sale.SalesDetails.Add(item);
                    }
                }
            }
            else
            {
                sale.SalesDetails.Add(new SalesDetail());
            }

            sale.Subtotal = sale.TotalAmout - sale.VatTotal;
            sale.Subtotal = (double?)Math.Round((decimal)sale.Subtotal, 2);
            sale.VatTotal = (double?)Math.Round((decimal)sale.VatTotal, 2);
            sale.TotalAmout = (double?)Math.Round((decimal)sale.TotalAmout, 2);
            sale.OrderDate = sale.OrderDate;
            return View(sale);
        }

        

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sale == null)
            {
                return NotFound();
            }

            var sale = await _context.Sale
                .FirstOrDefaultAsync(m => m.SalesID == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            Sale sale = new Sale();
            sale.SalesDetails = new List<SalesDetail>();

            //for a while we are generating rows from server side but good practice //is to genrate it from client side(JQuery/JavaScript)
            SalesDetail row1 = new SalesDetail();
            sale.SalesDetails.Add(row1);
            sale.VatParcentage = 15;
            sale.TotalAmout = 0;
            sale.Subtotal = 0;
            var orderNo = $"{RandomString(5)} - {RandomNumber(1000, 9999)}";  
            sale.OrderNo = orderNo;
            return View(sale);
            //sale.SalesDetails.Add(salesDetail);
            //return View("Create",sale);
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sale sale)
        {
            sale.PaymentMethod = "N/A";
            _context.Add(sale);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "SalesDetails", sale);
        }

        

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
         

            var sale = await _context.Sale.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            var details = _context.SalesDetails.Where(x => x.SalesID == id);
            if (details != null)
            {
                foreach (var item in details)
                {
                    if (item.ProductId != null)
                    {
                        var product = await _context.Products.FindAsync(item.ProductId);
                        item.ProductName = product.Name;
                    }
                    bool alreadyExist = sale.SalesDetails.Contains(item);
                    if (!alreadyExist)
                    {
                        sale.SalesDetails.Add(item);
                    }
                }
            }
            else
            {
                sale.SalesDetails.Add(new SalesDetail());
            }
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesID,OrderNo,CustomerName,CustomerPhone,CustomerAddress,OrderDate,PaymentMethod,TotalAmout,CreatedBy,CreatedOn,Status,Subtotal,DiscountParcentage,VatParcentage")] Sale sale)
        {
            if (id != sale.SalesID)
            {
                return NotFound();
            }
                try
                {
                sale.PaymentMethod = "cash";
                    _context.Update(sale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleExists(sale.SalesID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            return RedirectToAction("Create", "SalesDetails", sale);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sale == null)
            {
                return NotFound();
            }

            var sale = await _context.Sale
                .FirstOrDefaultAsync(m => m.SalesID == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sale == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Sale'  is null.");
            }
            var sale = await _context.Sale.FindAsync(id);
            if (sale != null)
            {
                _context.Sale.Remove(sale);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleExists(int id)
        {
          return _context.Sale.Any(e => e.SalesID == id);
        }

        private int RandomNumber(int min, int max)
        {
            var _random = new Random();
            return _random.Next(min, max);
        }
        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);
            var _random = new Random();
            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):   
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToUpper() : builder.ToString();
        }
    }
}


