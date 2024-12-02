using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Web.Authorize;

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

        public async Task<IActionResult> Index()
        {
            var sales = await salesService.GetAllSalesAsync();
            return View(sales);
        }

        public async Task<IActionResult> Details(int id)
        {
            var saleDetails = await salesService.GetSaleDetailsAsync(id);
            return View(saleDetails);
        }
    }
}
