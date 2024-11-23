using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models;
using WarehouseApp.Data.Repository.Interfaces;
using WarehouseApp.Services.Data.Interfaces;
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
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .Select(p => new ProductIndexViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImagePath = p.ImagePath,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    Categories = p.ProductCategories.Select(pc => pc.Category.Name).ToList()
                })
                .ToArrayAsync();

            return productViewModel;
        }

        public Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
