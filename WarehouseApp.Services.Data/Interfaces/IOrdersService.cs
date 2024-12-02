using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models.Users;
using WarehouseApp.Web.ViewModels.Orders;

namespace WarehouseApp.Services.Data.Interfaces
{
    public interface IOrdersService
    {
        Task<bool> AddOrderAsync(OrderViewModel model, Guid userId);

        Task<List<Supplier>> GetAllSuppliersAsync();

        Task<List<OrderProductViewModel>> GetProductsFromRequestsAsync();
        
        Task<List<OrderListViewModel>> GetAllOrdersAsync();

        Task<List<OrderListViewModel>> GetAllOrdersBySupplierIdAsync(Guid id);

		Task<OrderDetailsViewModel?> GetOrderDetailsAsync(int orderId);

        Task<List<OrderProductViewModel>> GetAllAvailableProductsAsync();

        Task<bool> EditOrderStatusAsync(EditOrderStatusViewModel model);
    }
}
