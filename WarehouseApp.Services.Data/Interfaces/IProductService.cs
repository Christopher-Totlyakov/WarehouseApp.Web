using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models;
using WarehouseApp.Web.ViewModels.Product;

namespace WarehouseApp.Services.Data.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductIndexViewModel>> GetAllProductsAsync();

        Task AddProductAsync(AddProductFormModel product);

        Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(int id);
    }
}
