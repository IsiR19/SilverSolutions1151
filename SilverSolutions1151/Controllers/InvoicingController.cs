using Microsoft.AspNetCore.Mvc;

namespace SilverSolutions1151.Controllers
{
    public class InvoicingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Print()
        {
            return View();
        }
    }
}
