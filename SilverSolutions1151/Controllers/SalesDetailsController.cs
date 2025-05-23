﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SilverSolutions1151.Data;
using SilverSolutions1151.Data.Entity;
using SilverSolutions1151.Models.Entity;

namespace SilverSolutions1151.Controllers
{
    [Authorize]
    public class SalesDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SalesDetails
        public async Task<IActionResult> Index(SalesDetail salesDetail)
        {
            var salesDetails = await _context.SalesDetails.Where(x => x.SalesID == salesDetail.SalesID).ToListAsync();
            
            return View(salesDetails);
        }

        // GET: SalesDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SalesDetails == null)
            {
                return NotFound();
            }

            var salesDetail = await _context.SalesDetails
                .Include(s => s.Sale)
                .FirstOrDefaultAsync(m => m.SalesDetailID == id);
            if (salesDetail == null)
            {
                return NotFound();
            }

            return View(salesDetail);
        }

        // GET: SalesDetails/Create
        public IActionResult Create(Sale sale)
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductID", "Name");
            ViewData["SalesID"] = new SelectList(_context.Sale, "SalesID", "SalesID");
            return View();
        }

        // POST: SalesDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesDetailID,SalesID,ProductId,ProductName,UnitPrice,Quantity,LineTotal,CreatedBy,CreatedOn,Status")] SalesDetail salesDetail)
        {
            var total = salesDetail.Quantity * salesDetail.UnitPrice;
            salesDetail.LineTotal = total;
                _context.Add(salesDetail);
                await _context.SaveChangesAsync();


            var quantity = salesDetail.Quantity;
            var productName = _context.Products.Where(x => x.ProductID == salesDetail.ProductId).FirstOrDefault();
            if (productName.Name.Contains("17"))
            {
                quantity = quantity * 17;
            }
            else if (productName.Name.Contains("50"))
            {
                quantity= quantity * 50;
            }
            else if (productName.Name.ToLower().Contains("1kg"))
            {
                quantity = quantity * 1000;
            }

            var productId = _context.ProductType.FirstOrDefault();
            var ingredients = _context.Ingredients.Where(x => x.Description == "Tobacco").FirstOrDefault();
            var ingredientId = ingredients.Id;

            //Update the sold items 
                var updateSold = new ManufacturingStage
            {
                Id = Guid.NewGuid(),
                ProductionStage = ProductionStage.Sold,
                Quantity = (decimal)quantity,
                CreatedDate = DateTime.Now,
                IngredientId = ingredientId,
                ProductTypeId = productId.Id
                };

            _context.Add(updateSold);
            await _context.SaveChangesAsync();
            //update pacakaging
            var updatePackaging = new ManufacturingStage
            {
                Id = Guid.NewGuid(),
                ProductionStage = ProductionStage.Packing,
                Quantity = (decimal)-quantity,
                CreatedDate = DateTime.Now,
                IngredientId = ingredientId,
                ProductTypeId = productId.Id
            };

            _context.Add(updatePackaging);
            await _context.SaveChangesAsync();

            var sale = await _context.Sale.FindAsync(salesDetail.SalesID);
            if(sale != null)
            {
                //sale.TotalAmout = sale.Subtotal + salesDetail.LineTotal;
                sale.Subtotal = sale.Subtotal + salesDetail.LineTotal;
                sale.TotalAmout = sale.Subtotal;
                sale.VatTotal = sale.TotalAmout * (double)(sale.VatParcentage / 100m);
                sale.TotalAmout = sale.TotalAmout + (sale.TotalAmout * (double)(sale.VatParcentage / 100m));
               
                if(sale.DiscountParcentage > 0)
                {
                    sale.DiscountTotal = (double)((decimal)sale.DiscountParcentage / 100m) * sale.TotalAmout;
                    sale.TotalAmout = sale.TotalAmout + (double)((decimal)sale.DiscountParcentage / 100m) * sale.TotalAmout;
                }

                
                sale.Subtotal = (double?)Math.Round((decimal)sale.Subtotal, 2);
                sale.VatTotal = (double?)Math.Round((decimal)sale.VatTotal, 2);
                sale.TotalAmout = (double?)Math.Round((decimal)sale.TotalAmout, 2);

                _context.Update(sale);
                await _context.SaveChangesAsync();

               
            }
            ViewData["SalesID"] = new SelectList(_context.Sale, "SalesID", "SalesID", salesDetail.SalesID);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductID", "Name", salesDetail.ProductId);


           
            return RedirectToAction("Edit", "Sales", new {id = sale.SalesID });
        }

        // GET: SalesDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SalesDetails == null)
            {
                return NotFound();
            }

            var salesDetail = await _context.SalesDetails.FindAsync(id);
            if (salesDetail == null)
            {
                return NotFound();
            }
            ViewData["SalesID"] = new SelectList(_context.Sale, "SalesID", "SalesID", salesDetail.SalesID);
            return View(salesDetail);
        }

        // POST: SalesDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesDetailID,SalesID,ProductId,ProductName,UnitPrice,Quantity,LineTotal,CreatedBy,CreatedOn,Status")] SalesDetail salesDetail)
        {
            var total = salesDetail.Quantity * salesDetail.UnitPrice;
            salesDetail.LineTotal = total;
            _context.Add(salesDetail);
            await _context.SaveChangesAsync();


            var quantity = salesDetail.Quantity;
            var productName = _context.Products.Where(x => x.ProductID == salesDetail.ProductId).FirstOrDefault();
            if (productName.Name.Contains("17"))
            {
                quantity = quantity * 17;
            }
            else if (productName.Name.Contains("50"))
            {
                quantity = quantity * 50;
            }
            else if (productName.Name.ToLower().Contains("1kg"))
            {
                quantity = quantity * 1000;
            }

            var productId = _context.ProductType.FirstOrDefault();
            var ingredients = _context.Ingredients.Where(x => x.Description == "Tobacco").FirstOrDefault();
            var ingredientId = ingredients.Id;


            var updateSold = new ManufacturingStage
            {
                Id = Guid.NewGuid(),
                ProductionStage = ProductionStage.Sold,
                Quantity = (decimal)quantity,
                CreatedDate = DateTime.Now,
                IngredientId = ingredientId,
                ProductTypeId = productId.Id
            };

            _context.Add(updateSold);
            await _context.SaveChangesAsync();

            var updatePackaging = new ManufacturingStage
            {
                Id = Guid.NewGuid(),
                ProductionStage = ProductionStage.Packing,
                Quantity = (decimal)-quantity,
                CreatedDate = DateTime.Now,
                IngredientId = ingredientId,
                ProductTypeId = productId.Id
            };

            _context.Add(updatePackaging);
            await _context.SaveChangesAsync();

            var sale = await _context.Sale.FindAsync(salesDetail.SalesID);
            if (sale != null)
            {
                //sale.TotalAmout = sale.Subtotal + salesDetail.LineTotal;
                sale.Subtotal = sale.Subtotal + salesDetail.LineTotal;
                sale.TotalAmout = sale.Subtotal;
                sale.VatTotal = sale.TotalAmout * (double)(sale.VatParcentage / 100m);
                sale.TotalAmout = sale.TotalAmout + (sale.TotalAmout * (double)(sale.VatParcentage / 100m));

                if (sale.DiscountParcentage > 0)
                {
                    sale.DiscountTotal = (double)((decimal)sale.DiscountParcentage / 100m) * sale.TotalAmout;
                    sale.TotalAmout = sale.TotalAmout + (double)((decimal)sale.DiscountParcentage / 100m) * sale.TotalAmout;
                }


                sale.Subtotal = (double?)Math.Round((decimal)sale.Subtotal, 2);
                sale.VatTotal = (double?)Math.Round((decimal)sale.VatTotal, 2);
                sale.TotalAmout = (double?)Math.Round((decimal)sale.TotalAmout, 2);

                _context.Update(sale);
                await _context.SaveChangesAsync();


            }
            ViewData["SalesID"] = new SelectList(_context.Sale, "SalesID", "SalesID", salesDetail.SalesID);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductID", "Name", salesDetail.ProductId);

            return RedirectToAction("Edit", "Sales", new { id = sale.SalesID });
        }

        // GET: SalesDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SalesDetails == null)
            {
                return NotFound();
            }

            var salesDetail = await _context.SalesDetails
                .Include(s => s.Sale)
                .FirstOrDefaultAsync(m => m.SalesDetailID == id);
            if (salesDetail == null)
            {
                return NotFound();
            }

            return View(salesDetail);
        }

        // POST: SalesDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SalesDetails == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SalesDetails'  is null.");
            }
            var salesDetail = await _context.SalesDetails.FindAsync(id);
            if (salesDetail != null)
            {
                _context.SalesDetails.Remove(salesDetail);
            }
            
            await _context.SaveChangesAsync();

            var total = salesDetail.Quantity * salesDetail.UnitPrice;
            salesDetail.LineTotal = total;
           
            var quantity = salesDetail.Quantity;
            var productName = _context.Products.Where(x => x.ProductID == salesDetail.ProductId).FirstOrDefault();
            if (productName.Name.Contains("17"))
            {
                quantity = quantity * 17;
            }
            else if (productName.Name.Contains("50"))
            {
                quantity = quantity * 50;
            }
            else if (productName.Name.ToLower().Contains("1kg"))
            {
                quantity = quantity * 1000;
            }


            var productId = _context.ProductType.FirstOrDefault();
            var ingredients = _context.Ingredients.Where(x => x.Description == "Tobacco").FirstOrDefault();
            var ingredientId = ingredients.Id;

            var updateSold = new ManufacturingStage
            {
                Id = Guid.NewGuid(),
                ProductionStage = ProductionStage.Sold,
                Quantity = (decimal)-quantity,
                CreatedDate = DateTime.Now,
                IngredientId = ingredientId,
                ProductTypeId = productId.Id
            };

            _context.Add(updateSold);
            await _context.SaveChangesAsync();

            var updatePackaging = new ManufacturingStage
            {
                Id = Guid.NewGuid(),
                ProductionStage = ProductionStage.Packing,
                Quantity = (decimal)quantity,
                CreatedDate = DateTime.Now,
                IngredientId = ingredientId,
                ProductTypeId = productId.Id
            };

            _context.Add(updatePackaging);
            await _context.SaveChangesAsync();

            var sale = await _context.Sale.FindAsync(salesDetail.SalesID);
            if (sale != null)
            {
                //sale.TotalAmout = sale.Subtotal + salesDetail.LineTotal;
                sale.Subtotal = sale.Subtotal - salesDetail.LineTotal;
                sale.TotalAmout = sale.Subtotal;
                sale.VatTotal = sale.TotalAmout * (double)(sale.VatParcentage / 100m);
                sale.TotalAmout = sale.TotalAmout + (sale.TotalAmout * (double)(sale.VatParcentage / 100m));

                if (sale.DiscountParcentage > 0)
                {
                    sale.DiscountTotal = (double)((decimal)sale.DiscountParcentage / 100m) * sale.TotalAmout;
                    sale.TotalAmout = sale.TotalAmout + (double)((decimal)sale.DiscountParcentage / 100m) * sale.TotalAmout;
                }


                sale.Subtotal = (double?)Math.Round((decimal)sale.Subtotal, 2);
                sale.VatTotal = (double?)Math.Round((decimal)sale.VatTotal, 2);
                sale.TotalAmout = (double?)Math.Round((decimal)sale.TotalAmout, 2);

                _context.Update(sale);
                await _context.SaveChangesAsync();


            }

            return RedirectToAction("Edit", "Sales", new { id = salesDetail.SalesID });
        }

        private bool SalesDetailExists(int id)
        {
          return _context.SalesDetails.Any(e => e.SalesDetailID == id);
        }
    }
}
