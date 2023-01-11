using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vehicle.Models;
using Vehicle.Services;

namespace Vehicle.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Active()
        {
            return View();
        }

        public IActionResult Existing()
        {
            return View();
        }

        public IActionResult Expired()
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

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }

        public IActionResult CashOut()
        {
            return View();
        }
        public IActionResult Client()
        {
            return View();
        }

        public async Task<List<DashItems>> GetAdminDashBoardInfo()
        {
            return Server.GetAdminDashBoardInfoAsync();
        }

        public async Task<List<UserItems>> GetAllForAdmin()
        {
            return Server.GetAllForAdminAsync();
        }

        public async Task<int> CheckOut()
        {
            return Server.CheckOuntCount();
        }

        public async Task<List<PayItems>> GetCashouts()
        {
            return Server.GetCashouts();
        }

        public void SetPaid(int Id)
        {
            Server.SetPaid(Id);
        }

        public void disableAgent(int Id)
        {
            Server.DisableAgent(Id);
        }

        public void enableAgent(int Id)
        {
            Server.EnableAgent(Id);
        }

        public async Task<List<UserItems>> GetAgentClient(int id)
        {
            return Server.GetAgentClient<UserItems>(id);
        }
    }
}
