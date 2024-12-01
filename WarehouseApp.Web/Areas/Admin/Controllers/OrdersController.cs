using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WarehouseApp.Data.Models.Interfaces;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Web.ViewModels.Orders;

namespace WarehouseApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
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
            var orders = await orderService.GetAllOrdersAsync();
            return View(orders);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var orderDetails = await orderService.GetOrderDetailsAsync(id);

            if (orderDetails == null)
            {
                return NotFound();
            }

            return View(orderDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var productsToOrder = await orderService.GetProductsFromRequestsAsync();
            var suppliers = await orderService.GetAllSuppliersAsync();
            //var availableProducts = await orderService.GetAllAvailableProductsAsync();

            var model = new OrderViewModel
            {
                OrderDate = DateTime.UtcNow,
                OrderProducts = productsToOrder,
                //AvailableProducts = availableProducts
            };

            ViewBag.Suppliers = suppliers;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Suppliers = await orderService.GetAllSuppliersAsync();
                //model.AvailableProducts = await orderService.GetAllAvailableProductsAsync();

                return View(model);
            }
            //     var selectedProducts = model.OrderProducts
            //.Concat(model.AvailableProducts.Where(p => p.QuantityOrdered > 0)
            //.Select(p => new OrderProductViewModel
            //{
            //    ProductId = p.ProductId,
            //    ProductName = p.ProductName,
            //    QuantityOrdered = p.QuantityOrdered
            //})).ToList();

            //     model.OrderProducts = selectedProducts;

            var idUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrWhiteSpace(idUser))
            {
                RedirectToAction(nameof(Index));
            }

            bool isGuidValid = Guid.TryParse(idUser, out Guid parsedGuid);
            if (!isGuidValid)
            {
                RedirectToAction(nameof(Index));
            }

            bool isSucceeded = await orderService.AddOrderAsync(model, parsedGuid);
            if (!isSucceeded)
            {
                ViewBag.Suppliers = await orderService.GetAllSuppliersAsync();

                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> EditStatus(int Id)
        {
            var order = await orderService.GetOrderDetailsAsync(Id);
            if (order == null)
            {
                return NotFound();
            }

            // Създайте модел с възможни статуси за редактиране
            var statusOptions = new List<string> { "Pending", "Completed", "Cancelled" };

            var model = new EditOrderStatusViewModel
            {
                OrderId = order.OrderId,
                StatusOptions = statusOptions,
                Status = order.Status
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(EditOrderStatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("EditStatus", model);
            }

            bool isSucceeded = await orderService.EditOrderStatusAsync(model);

            if (!isSucceeded)
            {
                return View(model);
            }

            return RedirectToAction(nameof(Index)); 
        }


    }
}
