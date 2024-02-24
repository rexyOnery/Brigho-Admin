using Microsoft.AspNetCore.Mvc;

namespace Vehicle.Controllers
{
	public class PowerAdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
