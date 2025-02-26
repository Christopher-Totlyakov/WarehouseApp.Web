﻿using Microsoft.EntityFrameworkCore;
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


        public async Task<(IEnumerable<ProductIndexViewModel>, int)> GetAllProductsPagedAsync(decimal minPrice, decimal maxPrice, int? categoryId, int currentPage, int productsPerPage)
        {
            var query = repository
                .GetAllAttached<Product>()
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .Where(p => p.SoftDelete != true && p.Price >= minPrice && p.Price <= maxPrice);

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId.Value));
            }

            int totalProducts = await query.CountAsync();
            if (currentPage < 1)
            {
                currentPage = 1;
            }

            var products = await query
                .Skip((currentPage - 1) * productsPerPage)
                .Take(productsPerPage)
                .To<ProductIndexViewModel>()
                .ToArrayAsync();

            return (products, totalProducts);
        }


        public async Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(int id)
        {
            ProductDetailsViewModel? productViewModel = await repository
               .GetAllAttached<Product>()
               .Where(x => x.SoftDelete != true)
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
                .Where(x => x.SoftDelete != true)
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
                .Where(x => x.SoftDelete != true)
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
                .Where(x => x.SoftDelete != true)
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

        public async Task<bool> AddProductAsync(EditProductViewModel model)
        {
            var product = new Product();
            AutoMapperConfig.MapperInstance.Map(model, product);

            if (product == null)
            {
                return false;
            }
            foreach (var categoryId in model.SelectedCategoryIds)
            {
                product.ProductCategories.Add(new ProductCategory
                {
                    CategoryId = categoryId
                });
            }

            await repository.AddAsync(product);
            return true;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var product = await repository
                .GetAllAttached<Product>()
                .Where(x => x.Id == id && x.SoftDelete == false)
                .ToListAsync();

            product[0].SoftDelete = true;

            if (product[0] == null)
            {
                return false;
            }

            await repository.UpdateAsync(product[0]);

            return true;
        }
    }
}
