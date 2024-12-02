using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models;
using WarehouseApp.Data.Models.Users;
using WarehouseApp.Data.Repository;
using WarehouseApp.Data.Repository.Interfaces;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Web.ViewModels.Orders;

namespace WarehouseApp.Services.Data
{
    public class OrdersService : IOrdersService
    {
        private readonly IRepository repository;

        public OrdersService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<bool> AddOrderAsync(OrderViewModel model, Guid userId)
        {
            var order = new Order
            {
                SupplierId = model.SupplierId,
                OrderDate = DateTime.UtcNow,
                Status = model.Status,
                RequestedByWorkerId = userId
            };

            foreach (var product in model.OrderProducts)
            {
                order.OrderProducts.Add(new OrderProduct
                {
                    ProductId = product.ProductId,
                    QuantityOrdered = product.QuantityOrdered
                });
            }

            try
            {
                await repository.AddAsync(order);
                await repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<List<Supplier>> GetAllSuppliersAsync()
        {
            return await repository.GetAllAttached<Supplier>()
                .Where(s => s.CompanyName != "[Deleted]"
                || s.factoryLocation != "[Deleted]"
                || s.PreferredDeliveryMethod != "[Deleted]").ToListAsync();
        }

        public async Task<List<OrderProductViewModel>> GetProductsFromRequestsAsync()
        {
            var productsToOrder = await repository.GetAllAttached<RequestProduct>()
                    .Include(rp => rp.Product)
                    .GroupBy(rp => new { rp.ProductId, rp.Product.Name })
                    .Select(group => new OrderProductViewModel
                    {
                        ProductId = group.Key.ProductId,
                        ProductName = group.Key.Name,
                        QuantityOrdered = group.Sum(rp => rp.QuantityRequested),
                        IsAutoAdded = true
                    })
                    .ToListAsync();

            return productsToOrder;
        }

        public async Task<List<OrderListViewModel>> GetAllOrdersAsync()
        {
            return await repository.GetAllAttached<Order>()
                .Include(o => o.Supplier)
                .Select(o => new OrderListViewModel
                {
                    OrderId = o.OrderId,
                    SupplierName = o.Supplier.UserName,
                    OrderDate = o.OrderDate.ToString("yyyy-MM-dd"),
                    Status = o.Status
                })
                .ToListAsync();
        }
		public async Task<List<OrderListViewModel>> GetAllOrdersBySupplierIdAsync(Guid id)
		{
			return await repository.GetAllAttached<Order>()
                .Where(o => o.SupplierId == id)
			   .Include(o => o.Supplier)
			   .Select(o => new OrderListViewModel
			   {
				   OrderId = o.OrderId,
				   SupplierName = o.Supplier.UserName,
				   OrderDate = o.OrderDate.ToString("yyyy-MM-dd"),
				   Status = o.Status
			   })
			   .ToListAsync();
		}

		public async Task<OrderDetailsViewModel?> GetOrderDetailsAsync(int orderId)
        {
            var order = await repository.GetAllAttached<Order>()
                .Include(o => o.Supplier)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .Include(o => o.RequestedByWorker)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
                return null;

            return new OrderDetailsViewModel
            {
                OrderId = order.OrderId,
                SupplierName = order.Supplier.UserName,
                OrderDate = order.OrderDate,
                Status = order.Status,
                RequestedByWorkerName = order.RequestedByWorker.UserName,
                Products = order.OrderProducts.Select(op => new OrderProductDetailsViewModel
                {
                    ProductName = op.Product.Name,
                    QuantityOrdered = op.QuantityOrdered
                }).ToList()
            };
        }

        public async Task<List<OrderProductViewModel>> GetAllAvailableProductsAsync()
        {
            return await repository.GetAllAttached<Product>()
                .Where(p => !p.SoftDelete && p.StockQuantity > 0)
                .Select(p => new OrderProductViewModel
                {
                    ProductId = p.Id,
                    ProductName = p.Name
                })
                .ToListAsync();
        }
		

		public async Task<bool> EditOrderStatusAsync(EditOrderStatusViewModel model)
        {
            var order = await repository.GetByIdAsync<Order, int>(model.OrderId);
            if (order == null)
            {
                return false;
            }
            
            try
            {
                order.Status = model.Status;
                await repository.UpdateAsync(order);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

		
	}
}
