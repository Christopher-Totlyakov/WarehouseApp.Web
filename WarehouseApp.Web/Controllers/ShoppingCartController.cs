using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
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

            var cart = shoppingCartService.SetProductsInCooke(model, cartCookie);

            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddMinutes(30)
            };

            Response.Cookies.Append("ShoppingCart", JsonSerializer.Serialize(cart), options);

            return RedirectToAction("Index");
        }
    }
}
