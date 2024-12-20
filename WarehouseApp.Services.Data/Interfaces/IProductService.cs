﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models;
using WarehouseApp.Web.ViewModels.Category;
using WarehouseApp.Web.ViewModels.Product;

namespace WarehouseApp.Services.Data.Interfaces
{
    public interface IProductService
    {
        Task<(IEnumerable<ProductIndexViewModel>, int)> GetAllProductsPagedAsync(decimal minPrice, decimal maxPrice, int? categoryId, int currentPage, int productsPerPage);

        Task<bool> AddProductAsync(EditProductViewModel model);

        Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(int id);

        Task<EditProductViewModel?> GetProductEditByIdAsync(int id);

        Task<IEnumerable<CategoryViewModel?>> GetAllCategoryAsync();

        Task<bool> SaveProductAsync(EditProductViewModel model);

        Task<bool> SoftDeleteAsync(int id);
    }
}
