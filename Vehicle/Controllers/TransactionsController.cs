using Microsoft.AspNetCore.Mvc;

namespace Vehicle.Controllers
{
    public class TransactionsController : Controller
    {
        public IActionResult InProgress()
        {
            return View();
        }

        public IActionResult Completed()
        {
            return View();
        }

        public IActionResult Returned()
        {
            return View();
        }

        public IActionResult Cancelled()
        {
            return View();
        }

        public IActionResult Deleted()
        {
            return View();
        }
    }
}
