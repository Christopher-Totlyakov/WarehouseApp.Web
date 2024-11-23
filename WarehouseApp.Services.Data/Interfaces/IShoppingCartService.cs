using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Web.ViewModels.Product;
using WarehouseApp.Web.ViewModels.ShoppingCart;

namespace WarehouseApp.Services.Data.Interfaces
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<ShoppingCartItems>> GetChoiceProductsAsync(string cartCookie);

        List<AddToCartViewModel> SetProductsInCooke(AddToCartViewModel model, string cartCookie);

    }
}