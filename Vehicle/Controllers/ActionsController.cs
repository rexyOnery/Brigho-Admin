using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicle.Controllers
{
    public class ActionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Completed()
        {
            return View();
        }

        public IActionResult Inprogress()
        {
            return View();
        }

        public IActionResult Pending()
        {
            return View();
        }
    }
}
