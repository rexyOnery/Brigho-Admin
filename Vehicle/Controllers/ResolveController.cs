using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vehicle.Models;
using Vehicle.Services;

namespace Vehicle.Controllers
{
    public class ResolveController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public bool ProcessPay(string trans)
        {
           return Server.ProcessPay(trans);
        }
    }
}
