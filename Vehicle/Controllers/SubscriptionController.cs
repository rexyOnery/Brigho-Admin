using Microsoft.AspNetCore.Mvc;

namespace Vehicle.Controllers
{
    public class SubscriptionController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
