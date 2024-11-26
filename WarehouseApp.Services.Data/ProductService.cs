using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models;
using WarehouseApp.Data.Repository.Interfaces;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Services.Mapping;
using WarehouseApp.Web.ViewModels.Category;
using WarehouseApp.Web.ViewModels.Product;

namespace WarehouseApp.Services.Data
{
    public class ProductService : IProductService
    {
        private IRepository repository;

        public ProductService(IRepository _repository)
        {
            repository = _repository;
        }

        public Task AddProductAsync(AddProductFormModel product)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductIndexViewModel>> GetAllProductsAsync()
        {
            IEnumerable<ProductIndexViewModel> productViewModel = await repository
                .GetAllAttached<Product>()
                .To<ProductIndexViewModel>()
                .ToArrayAsync();

            return productViewModel;
        }

        public async Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(int id)
        {
            ProductDetailsViewModel? productViewModel = await repository
               .GetAllAttached<Product>()
               .Include(p => p.ProductCategories)
                   .ThenInclude(pc => pc.Category)
                   .Select(p => new ProductDetailsViewModel
				   {
					   Id = p.Id,
					   Name = p.Name,
					   ImagePath = p.ImagePath,
					   Description = p.Description,
					   Price = p.Price,
					   StockQuantity = p.StockQuantity,
					   Categories = p.ProductCategories.Select(pc => pc.Category.Name).ToList()
				   })
				   .FirstOrDefaultAsync(c => c.Id == id);
			   

			return productViewModel;
		}

        public async Task<EditProductViewModel?> GetProductEditByIdAsync(int id)
        {
            var product = await repository.GetAllAttached<Product>()
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            EditProductViewModel? model = null;
            if (product == null)
            {
                return model;
            }

            var categories = await GetAllCategoryAsync();

            model = new EditProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                ImagePath = product.ImagePath,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                SelectedCategoryIds = product.ProductCategories.Select(pc => pc.CategoryId).ToList(),
                AvailableCategories = categories
            };

            return model;
        }
        public async Task<IEnumerable<CategoryViewModel?>> GetAllCategoryAsync()
        {
            var categories = await repository.GetAllAttached<Category>()
               .Select(c => new CategoryViewModel
               {
                   Id = c.Id,
                   Name = c.Name
               }).ToListAsync();
            return categories;
        }

        public async Task<bool> SaveProductAsync(EditProductViewModel model)
        {
            var product = await repository.GetAllAttached<Product>()
               .Include(p => p.ProductCategories)
               .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (product == null)
            {
                return false;
            }

            product.Name = model.Name;
            product.ImagePath = model.ImagePath;
            product.Description = model.Description;
            product.Price = model.Price;
            product.StockQuantity = model.StockQuantity;

            product.ProductCategories.Clear();
            foreach (var categoryId in model.SelectedCategoryIds)
            {
                product.ProductCategories.Add(new ProductCategory
                {
                    ProductId = product.Id,
                    CategoryId = categoryId
                });
            }

           await repository.UpdateAsync(product);
            return true;
        }
    }
}
