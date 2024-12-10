using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Web.Authorize;

using static WarehouseApp.Common.Messages;

namespace WarehouseApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [WarehouseWorkerAuthorize]
    public class SalesController : Controller
    {
        private readonly ISalesService salesService;

        public SalesController(ISalesService _salesService)
        {
            salesService = _salesService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var sales = await salesService.GetAllSalesAsync();
            return View(sales);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var saleDetails = await salesService.GetSaleDetailsAsync(id);
            if (saleDetails.CustomerName == null) 
            {
				TempData[ErrorMessage] = "No such sale";
				return RedirectToAction(nameof(Index));
            }
            return View(saleDetails);
        }
    }
}
