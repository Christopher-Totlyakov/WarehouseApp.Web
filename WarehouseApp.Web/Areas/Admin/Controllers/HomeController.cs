using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseApp.Web.Authorize;

namespace WarehouseApp.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
    [WarehouseWorkerAuthorize]
    public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
