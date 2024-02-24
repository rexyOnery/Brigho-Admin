using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicle.Controllers
{
    public class DeletedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
