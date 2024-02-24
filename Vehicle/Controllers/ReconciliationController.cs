using Microsoft.AspNetCore.Mvc;

namespace Vehicle.Controllers
{
    public class ReconciliationController : Controller
    {
        public IActionResult Payment()
        {
            return View();
        }
    }
}
