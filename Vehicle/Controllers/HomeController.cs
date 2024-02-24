using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Vehicle.Models;
//using Vehicle.Models;
using Vehicle.Services;

namespace Vehicle.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public List<Ahhttransaction> GetTransaction()
        {
            return Server.GetCurrentDasboad<Ahhttransaction>();
        }
        public void Process(int Id, string processby)
        {
             Server.Process<Ahhttransaction>(Id, processby);
        }

        public void Completed(int Id, string processby)
        {
            Server.Completed<Ahhttransaction>(Id, processby);
        }
        public void Delete(int Id, string processby)
        {
            Server.Delete<Ahhttransaction>(Id, processby);
        }

        public string GetTotalCash()
        {
            return Server.GetTotalCash<Ahhttransaction>();
        }

        

        public IActionResult privacy()
        {
            return View();
        }

         
    }
}
