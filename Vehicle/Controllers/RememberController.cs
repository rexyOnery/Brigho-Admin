using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vehicle.Models;
using Vehicle.Services;

namespace Vehicle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RememberController : ControllerBase
    {
        [HttpGet("recoverpassword/{emailaddress}")]
        public async Task<string> RecoverPassword(string emailaddress)
        {
            var _codec = Server.GetAccountLoginData<Login>(emailaddress);
            return _codec;
        }


        [HttpGet("confirmation/{code}/{email}")]
        public async Task<bool> CodeConfirmation(string code, string email)
        {
            return Server.GetConfirmedCode(code, email);
        }

        [HttpPost("changepassword/{password}/{email}")]
        public bool ChangePassword(string password, string email)
        {
            return Server.ChangePassword(password, email);
        }

    }
}
