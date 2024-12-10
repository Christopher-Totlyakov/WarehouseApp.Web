using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Security.Claims;
using System.Text.Json;
using WarehouseApp.Data.Repository;
using WarehouseApp.Data.Repository.Interfaces;
using WarehouseApp.Services.Data;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Web.Authorize;
using WarehouseApp.Web.ViewModels.ShoppingCart;
using static WarehouseApp.Common.Messages;

namespace WarehouseApp.Web.Controllers
{
    [RequesterAndBuyerUserAuthorize]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartController(IShoppingCartService _shoppingCartService)
        {
            shoppingCartService = _shoppingCartService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? cartCookie = Request.Cookies["ShoppingCart"];

            IEnumerable<ShoppingCartItems> items = await shoppingCartService
                .GetChoiceProductsAsync(cartCookie);

            return View(items);
        }

        [HttpGet]
        public IActionResult AddToCart(int productId)
        {
            var model = new AddToCartViewModel
            {
                ProductId = productId,
                Quantity = 1 
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddToCart(AddToCartViewModel model)
        {
            if (!ModelState.IsValid)
            {
				TempData[ErrorMessage] = "Failed to Added";
				return View(model);
            }
            var cartCookie = Request.Cookies["ShoppingCart"];

            var cart = shoppingCartService.AddProductsInCooke(model, cartCookie);

            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddMinutes(30)
            };

            Response.Cookies.Append("ShoppingCart", JsonSerializer.Serialize(cart), options);
			TempData[SuccessMessage] = "Successfully Added";
			return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            var cartCookie = Request.Cookies["ShoppingCart"];

            var cart = shoppingCartService.RemoveProductFromCart(cartCookie, id);

            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddMinutes(30)
            };

            Response.Cookies.Append("ShoppingCart", JsonSerializer.Serialize(cart), options);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult EditCartItem(int Id)
        {
            var cartCookie = Request.Cookies["ShoppingCart"];

            var model = shoppingCartService.GetSelectedItemFromCart(cartCookie, Id);

            if (model == null) 
            {
				TempData[ErrorMessage] = "Failed to Edit";
				return RedirectToAction(nameof(Index));
            }
			
			return View(model);
        }

        [HttpPost]
        public IActionResult EditCartItem(AddToCartViewModel model)
        {
            if (!ModelState.IsValid) 
            {
				TempData[ErrorMessage] = "Failed to Edit";
			}
            var cartCookie = Request.Cookies["ShoppingCart"];

            var cart = shoppingCartService.SetEditItemInCart(cartCookie, model);

            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddMinutes(30)
            };

            Response.Cookies.Append("ShoppingCart", JsonSerializer.Serialize(cart), options);

			TempData[SuccessMessage] = "Successfully Edit";
			return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> PurchaseCartItems()
        {
            var cartCookie = Request.Cookies["ShoppingCart"];
            if (cartCookie == null)
            {
				TempData[ErrorMessage] = "Failed to Purchase";
				TempData["Error"] = "Your cart is empty!";
                return RedirectToAction("Index");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var remainingItems = await shoppingCartService.PurchaseItemAsync(cartCookie, userId);

            Response.Cookies.Append("ShoppingCart", JsonSerializer.Serialize(remainingItems));


			TempData[SuccessMessage] = "Successfully Purchase";
			TempData["Success"] = "Transaction completed successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest()
        {
            var cartCookie = Request.Cookies["ShoppingCart"];
            if (cartCookie == null)
            {
				TempData[ErrorMessage] = "Your cart is empty!";
				TempData["Error"] = "Your cart is empty!";
                return RedirectToAction(nameof(Index));
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool isSucceeded =  await shoppingCartService.RequestItemAsync(cartCookie, userId);

            if (!isSucceeded)
            {
				TempData[ErrorMessage] = "Failed to Create Request!";
				TempData["Error"] = "Error!";
                return RedirectToAction(nameof(Index));
            }

            Response.Cookies.Delete("ShoppingCart");


			TempData[SuccessMessage] = "Request created successfully!";
			TempData["Success"] = "Request created successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
