using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseApp.Data.Models;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Web.Authorize;
using WarehouseApp.Web.ViewModels.Category;
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

        [HttpGet]
        public async Task<IActionResult> Index(decimal minPrice = 0, decimal maxPrice = 999, int? categoryId = null)
        {
            IEnumerable<ProductIndexViewModel> products =
                await productService.GetAllProductsAsync(minPrice, maxPrice, categoryId);
            
            var categories = await productService.GetAllCategoryAsync();

            
            ViewData["MinPrice"] = minPrice;
            ViewData["MaxPrice"] = maxPrice;
            ViewData["SelectedCategoryId"] = categoryId;
            ViewData["AvailableCategories"] = categories;


            return View(products);
        }

        [HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			ProductDetailsViewModel products =
			   await productService.GetProductDetailsByIdAsync(id);

            if (products == null)
            {
                return RedirectToAction(nameof(Index));
            }

			return View(products);
		}

        [HttpGet]
        [WarehouseWorkerAuthorize]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await productService.GetProductEditByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpPost]
        [WarehouseWorkerAuthorize]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableCategories = await productService.GetAllCategoryAsync();
                    
                return View(model);
            }

            bool successfully = await productService.SaveProductAsync( model);

            if (!successfully)
            {
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [WarehouseWorkerAuthorize]
        public async Task<IActionResult> Add()
        {
            var model = new EditProductViewModel();
            model.AvailableCategories = await productService.GetAllCategoryAsync();

            return View(model);
        }
        [HttpPost]
        [WarehouseWorkerAuthorize]
        public async Task<IActionResult> Add(EditProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableCategories = await productService.GetAllCategoryAsync();
                return View(model);
            }

            // Проверка и запис на файла
            if (model.ImageFile != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(fileStream);
                }

                model.ImagePath = $"/img/{uniqueFileName}"; 
            }

            bool successfully = await productService.AddProductAsync(model);

            if (!successfully)
            {
                return View(model);
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        [WarehouseWorkerAuthorize]
        public async Task<IActionResult> Delete(int id)
        {
            bool IsSuccess = await productService.SoftDeleteAsync(id);
            
            if (!IsSuccess) 
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
