using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WarehouseApp.Data.Models.Interfaces;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Web.Authorize;

using static WarehouseApp.Common.Messages;

namespace WarehouseApp.Web.Controllers
{
	[SuppliersAuthorize]
	public class OrdersController : Controller
	{
		private readonly IOrdersService orderService;

		public OrdersController(IOrdersService orderService)
		{
			this.orderService = orderService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var idUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (String.IsNullOrWhiteSpace(idUser))
			{
				return RedirectToAction(nameof(Index));
			}
			bool isGuidValid = Guid.TryParse(idUser, out Guid parsedGuid);

			if (!isGuidValid)
			{
				return RedirectToAction("Index", "Home");
			}

			var orders = await orderService.GetAllOrdersBySupplierIdAsync(parsedGuid);

			return View("~/Areas/Admin/Views/Orders/Index.cshtml", orders);
		}
		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var orderDetails = await orderService.GetOrderDetailsAsync(id);

			if (orderDetails == null)
			{
				TempData[ErrorMessage] = "Details Failed";
				return NotFound();
			}

			return View("~/Areas/Admin/Views/Orders/Details.cshtml", orderDetails);
		}
	}
}
