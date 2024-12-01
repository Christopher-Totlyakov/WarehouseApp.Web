using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models;
using WarehouseApp.Data.Repository;
using WarehouseApp.Data.Repository.Interfaces;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Web.ViewModels.Sales;

namespace WarehouseApp.Services.Data
{
    public class SalesService : ISalesService
    {
        private readonly IRepository repository;

        public SalesService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<SaleViewModel>> GetAllSalesAsync()
        {
            return await repository.GetAllAttached<Sale>()
                .Include(s => s.Customer)
                .Select(s => new SaleViewModel
                {
                    SaleId = s.SaleId,
                    CustomerName = s.Customer.UserName,
                    SaleDate = s.SaleDate.ToString("g"),
                    TotalAmount = s.TotalAmount
                })
                .ToListAsync();
        }

        public async Task<SaleDetailsViewModel> GetSaleDetailsAsync(int saleId)
        {
            var sale = await repository.GetAllAttached<Sale>()
                .Include(s => s.Customer)
                .Include(s => s.SaleProducts)
                .ThenInclude(sp => sp.Product)
                .FirstOrDefaultAsync(s => s.SaleId == saleId);

            if (sale == null) throw new ArgumentException("Sale not found.");

            return new SaleDetailsViewModel
            {
                SaleId = sale.SaleId,
                CustomerName = sale.Customer.UserName,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalAmount,
                Products = sale.SaleProducts.Select(sp => new SaleProductViewModel
                {
                    ProductName = sp.Product.Name,
                    QuantitySold = sp.QuantitySold,
                    UnitPrice = sp.UnitPrice
                }).ToList()
            };
        }

    }
}
