using Microsoft.AspNetCore.Mvc;

namespace Vehicle.Controllers
{
    public class MarketingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
