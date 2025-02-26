﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseApp.Data.Models;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Web.Authorize;
using WarehouseApp.Web.ViewModels.Category;
using WarehouseApp.Web.ViewModels.Product;
using static WarehouseApp.Common.Messages;

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
        public async Task<IActionResult> Index(decimal minPrice = 0, decimal maxPrice = 999, int? categoryId = null, int currentPage = 1)
        {
            const int ProductsPerPage = 6;

            var (products, totalProducts) = await productService.GetAllProductsPagedAsync(minPrice, maxPrice, categoryId, currentPage, ProductsPerPage);

            var categories = await productService.GetAllCategoryAsync();

            int totalPages = (int)Math.Ceiling((double)totalProducts / ProductsPerPage);

            ViewData["MinPrice"] = minPrice;
            ViewData["MaxPrice"] = maxPrice;
            ViewData["SelectedCategoryId"] = categoryId;
            ViewData["AvailableCategories"] = categories;
            ViewData["CurrentPage"] = currentPage;
            ViewData["TotalPages"] = totalPages;

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
				TempData[ErrorMessage] = "Failed to Edit";
				return View(model);
            }

            bool successfully = await productService.SaveProductAsync( model);

            if (!successfully)
            {
				TempData[ErrorMessage] = "Failed to Edit";
				return View(model);
            }

			TempData[SuccessMessage] = "Successfully Edited";
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
				TempData[ErrorMessage] = "Failed to add";
				return View(model);
            }

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
                TempData[ErrorMessage] = "Failed to add";
                return View(model);
            }
			TempData[SuccessMessage] = "Successfully added";
			return RedirectToAction("Index");
        }
        [HttpGet]
        [WarehouseWorkerAuthorize]
        public async Task<IActionResult> Delete(int id)
        {
            bool IsSuccess = await productService.SoftDeleteAsync(id);
            
            if (!IsSuccess) 
            {
				TempData[ErrorMessage] = "Failed to Delete";
				return RedirectToAction(nameof(Index));
            }

			TempData[SuccessMessage] = "Successfully Deleted";
			return RedirectToAction(nameof(Index));
        }
    }
}
