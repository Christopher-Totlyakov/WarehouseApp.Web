using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Web.Authorize;

namespace WarehouseApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [WarehouseWorkerAuthorize]
    public class RequestsController : Controller
    {
        private readonly IRequestService requestService;

        public RequestsController(IRequestService _requestService)
        {
            requestService = _requestService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var requests = await requestService.GetAllRequestsAsync();
            return View(requests);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var request = await requestService.GetRequestDetailsAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            return View(request);
        }
    }
}
