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

        public async Task<int> CountLogin()
        {
            return await Server.CountLogin();
        }

        public async Task<string> Recover(EmailModel model)
        {
            string emailaddress = model.Email;
            var _codec = Server.GetAccountLoginData<Login>(emailaddress);
            return _codec;
        }

        public async Task<ActionResult<bool>> CodeConfirmation(ConfirmModel model)
        {
            return Server.GetConfirmedCode(model.Code, model.Email);
        }

        public async Task<bool> ChangePassowrd(ChangePasswordModel model)
        {
            return Server.ChangePassword(model.Password, model.Email);
        }

        public async Task<bool> RecoverMailAddress(RecoverMailModel model)
        {
            return Server.SendCode2Email(model);
        }


        public async Task<ActionResult<bool>> AddUser(Login account)
        { 
            return await Server.AddUser<Login>(account);
        }

        

    }
}
