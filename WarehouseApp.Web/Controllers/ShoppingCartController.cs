using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Security.Claims;
using System.Text.Json;
using WarehouseApp.Data.Repository;
using WarehouseApp.Data.Repository.Interfaces;
using WarehouseApp.Services.Data;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Web.ViewModels.ShoppingCart;

namespace WarehouseApp.Web.Controllers
{
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
            
            var cartCookie = Request.Cookies["ShoppingCart"];

            var cart = shoppingCartService.AddProductsInCooke(model, cartCookie);

            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddMinutes(30)
            };

            Response.Cookies.Append("ShoppingCart", JsonSerializer.Serialize(cart), options);

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
                return RedirectToAction(nameof(Index));
            }
            
            return View(model);
        }

        [HttpPost]
        public IActionResult EditCartItem(AddToCartViewModel model)
        {
            var cartCookie = Request.Cookies["ShoppingCart"];

            var cart = shoppingCartService.SetEditItemInCart(cartCookie, model);

            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddMinutes(30)
            };

            Response.Cookies.Append("ShoppingCart", JsonSerializer.Serialize(cart), options);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> PurchaseCartItems()
        {
            var cartCookie = Request.Cookies["ShoppingCart"];
            if (cartCookie == null)
            {
                TempData["Error"] = "Your cart is empty!";
                return RedirectToAction("Index");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var remainingItems = await shoppingCartService.PurchaseItemAsync(cartCookie, userId);

            Response.Cookies.Append("ShoppingCart", JsonSerializer.Serialize(remainingItems));

            TempData["Success"] = "Transaction completed successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest()
        {
            var cartCookie = Request.Cookies["ShoppingCart"];
            if (cartCookie == null)
            {
                TempData["Error"] = "Your cart is empty!";
                return RedirectToAction(nameof(Index));
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool isSucceeded =  await shoppingCartService.RequestItemAsync(cartCookie, userId);

            if (!isSucceeded)
            {
                TempData["Error"] = "Error!";
                return RedirectToAction(nameof(Index));
            }

            Response.Cookies.Delete("ShoppingCart");

            TempData["Success"] = "Request created successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
