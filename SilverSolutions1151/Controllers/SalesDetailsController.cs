using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SilverSolutions1151.Data;
using SilverSolutions1151.Helpers;
using SilverSolutions1151.Models.Entity;


namespace SilverSolutions1151.Controllers
{

    public class SalesDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public JsonResult SaveInvoiceSale(Sale sale, List<SalesDetail> salesDetails)
        {
            ReturnMessage retMessage = new ReturnMessage();
            retMessage.IsSuccess = true;

            foreach (var item in salesDetails)
            {
                sale.SalesDetails.Add(new SalesDetail { ProductId = item.ProductId, UnitPrice = item.UnitPrice, Quantity = item.Quantity, LineTotal = item.LineTotal });
                //var prd = db.ProductStocks.Where(x => x.ProductId == item.ProductId && x.Quantity > 0).FirstOrDefault();
                //prd.Quantity = prd.Quantity - item.Quantity;
                //db.Entry(prd).State = EntityState.Modified;
            }
            _context.Sale.Add(sale);
            retMessage.Messagae = "Save Success!";
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                retMessage.IsSuccess = false;
            }

            return Json(retMessage);

        }
     
        public JsonResult EditInvoiceSale(Sale sale, List<SalesDetail> salesDetails, List<int> deleted)
        {
            ReturnMessage retMessage = new ReturnMessage();

            retMessage.IsSuccess = true;

            if (deleted != null)
            {
                foreach (var item in deleted)
                {
                    var data = _context.SalesDetails.Where(x => x.SalesDetailID == item).SingleOrDefault(); ;
                    _context.SalesDetails.Remove(data);
                }
            }

            foreach (var item in salesDetails)
            {
                if (item.SalesDetailID > 0)
                {
                    _context.Entry(item).State = EntityState.Modified;
                    retMessage.Messagae = "Update Success!";
                }
                else
                {
                    sale.SalesDetails.Add(new SalesDetail { ProductId = item.ProductId, UnitPrice = item.UnitPrice, Quantity = item.Quantity, LineTotal = item.LineTotal });
                    // var prd = db.ProductStocks.Where(x => x.ProductId == item.ProductId && x.Quantity > 0).FirstOrDefault();
                    // prd.Quantity = prd.Quantity - item.Quantity;
                    //db.Entry(prd).State = EntityState.Modified;
                    _context.Sale.Add(sale);
                    retMessage.Messagae = "Save Success!";
                }
            }


            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                retMessage.IsSuccess = false;
            }
         
            return Json(retMessage);
        }


        [HttpGet]
        public JsonResult GetAllSales()
        {

            var dataList = _context.Sale.ToList();
            var modefiedData = dataList.Select(x => new
            {
                SalesId = x.SalesID,
                OrderNo = x.OrderNo,
                CustomerName = x.CustomerName,
                CustomerPhone = x.CustomerPhone,
                CustomerAddress = x.CustomerAddress,
                OrderDate = x.OrderDate,
                TotalAmout = x.TotalAmout
            }).ToList();

            return Json(dataList);

        }
        [HttpGet]
        public JsonResult GetInvoiceBySalesId(int salesId)
        {

            List<Sale> dataList = (from sd in _context.SalesDetails.ToList()
                                   join s in _context.Sale on sd.SalesID equals s.SalesID
                                   where sd.SalesID == salesId
                                   select new Sale
                                   {
                                       SalesID = (int)sd.SalesID,
                                       OrderNo = s.OrderNo,
                                       CustomerName = s.CustomerName,
                                       CustomerPhone = s.CustomerPhone,
                                       CustomerAddress = s.CustomerAddress,
                                       OrderDate = s.OrderDate,
                                       PaymentMethod = s.PaymentMethod,
                                       TotalAmout = s.TotalAmout,
                                       SalesDetailId = sd.SalesDetailID,
                                       ProductId = sd.ProductId,
                                       UnitPrice = sd.UnitPrice,
                                       Subtotal = s.Subtotal,
                                       Quantity = sd.Quantity,
                                       LineTotal = sd.LineTotal,
                                       DiscountParcentage = s.DiscountParcentage,
                                       VatParcentage = s.VatParcentage
                                   }).ToList();

            return Json(dataList);
        }

        public JsonResult GetAllProduct()
        {
            var dataList = (from prd in _context.Products.Include("Category").ToList()
                            join stk in _context.ProductStocks on prd.ProductID equals stk.ProductID
                            where stk.Quantity > 0
                            select new
                            {
                                ProductId = prd.ProductID,
                                CategoryId = prd.CategoryID,
                                Name = prd.Name,
                                Status = prd.Status,
                                CategoryName = prd.Category.Name,
                                PurchasePrice = stk.PurchasePrice,
                                SalesPrice = stk.SalesPrice
                            }).ToList();


            return Json(dataList);
        }
    }
}
