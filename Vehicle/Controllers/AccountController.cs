using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vehicle.Models;
using Vehicle.Services;

namespace Vehicle.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Forgot()
        {
            return View();
        }

        public IActionResult ConfirmCode()
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        public async Task<List<Account>> Login(Account account)
        { 
            return await Server.Login<Account>(account);
        }

        public async Task<ActionResult<bool>> AddUser(Login account)
        { 
            return await Server.AddUser<Login>(account);
        }

        

    }
}
