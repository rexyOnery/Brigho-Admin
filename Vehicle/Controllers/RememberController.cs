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
        [HttpPost("recoverpassword")]
        public async Task<string> RecoverPassword(EmailModel model)
        {
            string emailaddress = model.Email;
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
