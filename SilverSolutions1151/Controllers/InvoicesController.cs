using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SilverSolutions1151.Data;
using SilverSolutions1151.Models;
using SilverSolutions1151.Models.Entity;
using SilverSolutions1151.Models.Paging;

namespace SilverSolutions1151.Controllers
{
    [Authorize]
    public class InvoicesController : Controller
    {

        private readonly ApplicationDbContext _context;
        private int defaultPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultPaginationSize"]);

        public InvoicesController(ApplicationDbContext context)
        {
            _context = context;
        }
        /*CUSTOM*/

        private void FillIndexViewBags(IPagedList<Invoice> invoices)
        {
            ViewBag.NetTotal = invoices.Sum(i => i.NetTotal);
            ViewBag.TotalWithVAT = invoices.Sum(i => i.TotalWithVAT);

        }

        public ViewResult Search(string text, string from, string noname, string to, int? page, int? pagesize, bool? proposal = false)
        {
            //System.Web.Mvc.seSession["invoiceText"] = text;
            //Session["invoiceFrom"] = from;
            //Session["invoiceTo"] = to;

            var invoices = _context.Invoice.Include(i => i.InvoiceDetails).Include(i => i.Customer);

            if (!string.IsNullOrWhiteSpace(from))
            {
                DateTime fromDate;
                if (DateTime.TryParse(from, CultureInfo.CurrentUICulture, DateTimeStyles.AssumeLocal, out fromDate))
                    invoices = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Invoice, Models.Customer>)invoices.Where(t => t.TimeStamp >= fromDate);
            }
            if (!string.IsNullOrWhiteSpace(to))
            {
                DateTime toDate;
                if (DateTime.TryParse(to, CultureInfo.CurrentUICulture, DateTimeStyles.AssumeLocal, out toDate))
                    invoices = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Invoice, Models.Customer>)invoices.Where(t => t.TimeStamp <= toDate);
            }

            if (!string.IsNullOrWhiteSpace(text))
            {
                invoices = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Invoice, Models.Customer>)invoices.Where(t => (t.Notes.ToLower().IndexOf(text.ToLower()) > -1) || (t.Name.ToLower().IndexOf(text.ToLower()) > -1) || (t.Customer.CustomerName.ToLower().IndexOf(text.ToLower()) > -1));
            }

            ViewBag.IsProposal = proposal;

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            IPagedList<Invoice> invoices_paged = null;

            if (proposal == true)//once the data is in memory, i can filter by IsProposal
                invoices = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Invoice, Models.Customer>)invoices.Where(i => i.InvoiceNumber == 0);  //we can not use  Where(i => i.IsProposal) from within the LINQ db context                
            else
                invoices = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Invoice, Models.Customer>)invoices.Where(i => i.InvoiceNumber > 0); //we can not use  Where(i => i.IsProposal) from within the LINQ db context                     

            invoices_paged = invoices.OrderByDescending(i => i.TimeStamp).ToPagedList(currentPageIndex, (pagesize.HasValue) ? pagesize.Value : defaultPageSize);

            FillIndexViewBags(invoices_paged);

            //if (Request.IsAjaxRequest())
            //    return PartialView("Index", invoices_paged);
            //else
            //    return View("Index", invoices_paged);

            return View("Index",invoices_paged);
        }

        public PartialViewResult UnPaidInvoices()
        {
            var invoices = _context.Invoice.Include(i => i.Customer).Where(i => i.Paid == false && i.DueDate >= DateTime.Now && i.InvoiceNumber > 0).OrderBy(i => i.DueDate);
            return PartialView("InvoicesListPartial", invoices.ToList());
        }

        public PartialViewResult LatestProposals()
        {
            var invoices = _context.Invoice.Include(i => i.Customer).Where(i => i.InvoiceNumber == 0).OrderByDescending(i => i.TimeStamp);
            return PartialView("InvoicesListPartial", invoices.ToList());
        }

        public PartialViewResult OverDueInvoices()
        {
            var invoices = _context.Invoice.Include(i => i.Customer).Where(i => i.Paid == false && i.DueDate < DateTime.Now && i.InvoiceNumber > 0).OrderBy(i => i.DueDate);
            return PartialView("InvoicesListPartial", invoices.ToList());
        }

        /*END CUSTOM*/

        //
        // GET: /Invoice/

        //public ActionResult Index(string filter, int? page, int? pagesize, bool? proposal = false)
        //{
        //    #region remember filter stuff
        //    //if (filter == "clear")
        //    //{
        //    //    Session["invoiceText"] = null;
        //    //    Session["invoiceFrom"] = null;
        //    //    Session["invoiceTo"] = null;
        //    //}
        //    //else
        //    //{
        //    //    if ((Session["invoiceText"] != null) || (Session["invoiceFrom"] != null) || (Session["invoiceTo"] != null))
        //    //    {
        //    //        return RedirectToAction("Search", new { text = Session["invoiceText"], from = Session["invoiceFrom"], to = Session["invoiceTo"], proposal = proposal });
        //    //    }
        //    //}
        //    #endregion


        //    int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
        //    var invoices = _context.Invoice.Include(i => i.InvoiceDetails).Include(i => i.Customer);
        //    ViewBag.IsProposal = proposal;

        //    IPagedList<Invoice> invoices_paged = null;
        //    if (proposal == true)
        //    {
        //        //invoices = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Invoice, Models.Customer>)invoices.Where(i => i.InvoiceNumber == 0);  //we can not use  Where(i => i.IsProposal) from within the LINQ db context                
        //    }
        //    else
        //    {
        //        //invoices = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Invoice, Models.Customer>)invoices.Where(i => i.InvoiceNumber > 0);  //we can not use  Where(i => i.IsProposal) from within the LINQ db context                
        //    }
        //    pagesize = pagesize ?? 1;
        //    invoices_paged = invoices.OrderByDescending(i => i.TimeStamp).ToPagedList(currentPageIndex, (pagesize.HasValue) ? pagesize.Value : defaultPageSize);

        //    FillIndexViewBags(invoices_paged);

        //    return View(invoices_paged);
        //}

        //
        // GET: /Invoice/Details/5

        public ViewResult Print(int id, bool? proposal = false)
        {
            //if (Request["lan"] != null)
            //{
                //valid culture name?
                CultureInfo[] cultures = System.Globalization.CultureInfo.GetCultures
                         (CultureTypes.SpecificCultures);

     
            //}


            ViewBag.Print = true;
            ViewBag.MyCompany = System.Configuration.ConfigurationManager.AppSettings["MyCompanyName"];
            ViewBag.MyCompanyID = System.Configuration.ConfigurationManager.AppSettings["MyCompanyID"];
            ViewBag.MyCompanyAddress = System.Configuration.ConfigurationManager.AppSettings["MyCompanyAddress"];
            ViewBag.MyCompanyPhone = System.Configuration.ConfigurationManager.AppSettings["MyCompanyPhone"];
            ViewBag.MyEmail = System.Configuration.ConfigurationManager.AppSettings["MyEmail"];
            ViewBag.MyBankAccount = System.Configuration.ConfigurationManager.AppSettings["MyBankAccount"];

            Invoice invoice = _context.Invoice.Find(id);
            if (proposal == true)
                return View("PrintProposal", invoice);
            else
                return View(invoice);
        }

        //
        // GET: /Invoice/Create

        public ActionResult Create(bool? proposal = false)
        {
            Invoice i = new Invoice();
            i.TimeStamp = DateTime.Now;
            i.DueDate = DateTime.Now.AddDays(30); //30 days after today
            //i.AdvancePaymentTax = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["DefaultAdvancePaymentTax"]);

            if (!proposal == true)
            {
                //generate next invoice number
                var next_invoice = (from inv in _context.Invoice
                                    orderby inv.InvoiceNumber descending
                                    select inv).FirstOrDefault();
                if (next_invoice != null)
                    i.InvoiceNumber = next_invoice.InvoiceNumber + 1;
            }
            ViewBag.IsProposal = proposal;
            ViewData["CustomerID"] = new SelectList(_context.Customers.OrderBy(c=> c.CustomerName), "CustomerID", "CustomerName");
            //ViewBag.CustomerID = new SelectList(_context.Customers.OrderBy(c => c.CustomerName), "CustomerID", "CustomerName");
            return View(i);
        }

        //
        // POST: /Invoice/Create


        public ActionResult Index()
        {
            //if (Session["AdminLogin"].ToString() != "")
            //{
            List<Invoice> OrderAndCustomerList = _context.Invoice.OrderByDescending(s => s.TimeStamp).ToList();
            return View(OrderAndCustomerList);
            //}

            //return RedirectToAction("Login", "AdminPanel");
        }

        [HttpPost]
        [System.Web.Mvc.ValidateInput(false)]
        public ActionResult Create(Invoice invoice, bool? proposal = false)
        {
            ViewData["CustomerID"] = new SelectList(_context.Customers.OrderBy(c=>c.CustomerName), "CustomerID", "CustomerName");
            //ViewBag.CustomerID = new SelectList(_context.Customers.OrderBy(c => c.CustomerName), "CustomerID", "Name", invoice.CustomerID);
            ViewBag.IsProposal = proposal;

            if (ModelState.IsValid)
            {
                //make sure invoice number doesn't exist
                if (proposal == false)
                {
                    var invoice_exists = (from inv in _context.Invoice where inv.InvoiceNumber == invoice.InvoiceNumber select inv).FirstOrDefault();
                    if (invoice_exists != null)
                    {
                        ModelState.AddModelError("InvoiceNumber", "Invoice with that number already exists");
                        return View(invoice);
                    }
                }
                _context.Invoice.Add(invoice);
                _context.SaveChanges();
                return RedirectToAction("Edit", "Invoice", new { id = invoice.InvoiceID, proposal = proposal });
            }
            return View(invoice);
        }

        //
        // GET: /Invoice/Edit/5

        public ActionResult Edit(int id, bool? proposal = false, bool? makeinvoice = false, bool? makeproposal = false)
        {
            Invoice invoice = _context.Invoice.Find(id);
            ViewBag.CustomerID = new SelectList(_context.Customers.OrderBy(c => c.CustomerName), "CustomerID", "CustomerName", invoice.CustomerID);

            if (makeinvoice == true)
            {
                //we want to make invoice from this proposal
                //generate next invoice number
                var next_invoice = (from inv in _context.Invoice
                                    orderby inv.InvoiceNumber descending
                                    select inv).FirstOrDefault();

                if (next_invoice != null)
                    invoice.InvoiceNumber = next_invoice.InvoiceNumber + 1; //assign next available invoice number 

                invoice.TimeStamp = DateTime.Now;
                invoice.DueDate = DateTime.Now.AddDays(30);

                ViewBag.Warning = "The current item is going to be converted on Invoice. A new InvoiceNumber has been pre-assigned. The dates will be modified accordingly. Click on 'Save' to continue.";
                ViewBag.ShowMakeInvoice = ViewBag.ShowMakeProposal = false;
            }
            else if (makeproposal == true)
            {
                invoice.InvoiceNumber = 0; //reset invoice number                
                ViewBag.Warning = "The current item is going to be converted on Proposal. You will lose InvoiceNumber. If that's what you want click on 'Save'";
                ViewBag.ShowMakeInvoice = ViewBag.ShowMakeProposal = false;
            }
            else
            {
                if (!invoice.IsProposal && proposal == true)
                {
                    //item is invoice, redirect to proper route (hack)
                    return RedirectToAction("Edit", new { id = id, proposal = false, makeinvoice = false });
                }

                ViewBag.ShowMakeInvoice = invoice.IsProposal;
                ViewBag.ShowMakeProposal = !invoice.IsProposal;
            }

            ViewBag.IsProposal = invoice.IsProposal;

            return View(invoice);
        }

        //
        // POST: /Invoice/Edit/5

        [HttpPost]
        [System.Web.Mvc.ValidateInput(false)]
        public ActionResult Edit(Invoice invoice, bool? proposal = false)
        {
            ViewBag.CustomerID = new SelectList(_context.Customers.OrderBy(c => c.CustomerName), "CustomerID", "Name", invoice.CustomerID);
            ViewBag.IsProposal = proposal;
            if (ModelState.IsValid)
            {
                if (proposal == false)
                {
                    //make sure invoice number doesn't exist
                    var invoice_exists = (from inv in _context.Invoice
                                          where inv.InvoiceNumber == invoice.InvoiceNumber
                                          && inv.InvoiceID != invoice.InvoiceID
                                          select inv).Count();

                    if (invoice_exists > 0)
                    {
                        ModelState.AddModelError("InvoiceNumber", "Invoice with that number already exists");
                        return View(invoice);
                    }
                }

                _context.Entry(invoice).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index", new { proposal = proposal });
            }
            return View(invoice);
        }

        //
        // GET: /Invoice/Delete/5

        public ActionResult Delete(int id, bool? proposal = false)
        {
            ViewBag.IsProposal = proposal;
            Invoice invoice = _context.Invoice.Find(id);
            return View(invoice);
        }

        //
        // POST: /Invoice/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, bool? proposal = false)
        {
            ViewBag.IsProposal = proposal;
            Invoice invoice = _context.Invoice.Find(id);
            _context.Invoice.Remove(invoice);
            _context.SaveChanges();
            return RedirectToAction("Index", new { proposal = proposal });
        }

        protected override void Dispose(bool disposing)
        {
            // _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
