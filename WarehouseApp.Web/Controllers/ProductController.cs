using Microsoft.AspNetCore.Mvc;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Web.ViewModels.Product;

namespace WarehouseApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductIndexViewModel> products =
                await productService.GetAllProductsAsync();

            return View(products);
        }
    }
}
