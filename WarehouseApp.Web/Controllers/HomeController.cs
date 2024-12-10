using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Web.ViewModels;
using WarehouseApp.Web.ViewModels.Home;
using WarehouseApp.Web.ViewModels.Product;


namespace WarehouseApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService productService;

        public HomeController(ILogger<HomeController> logger, IProductService _productService)
        {
            _logger = logger;
            productService = _productService;
        }

        public async Task<IActionResult> Index(int currentPage = 1)
        {
            const int productsPerPage = 3; 

            var (topProducts, _) = await productService.GetAllProductsPagedAsync(0, 1000, null, currentPage, productsPerPage);
            var (newProducts, _) = await productService.GetAllProductsPagedAsync(0, 1000, null, ++currentPage, productsPerPage);

            var viewModel = new HomeViewModel
            {
                TopProducts = topProducts.OrderByDescending(p => p.Price), 
                NewProducts = newProducts.OrderByDescending(p => p.Name)
            };

            return View(viewModel);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
