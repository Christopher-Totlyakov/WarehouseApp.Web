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
    }
}
