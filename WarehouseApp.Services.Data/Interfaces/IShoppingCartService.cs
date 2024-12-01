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

        List<AddToCartViewModel> AddProductsInCooke(AddToCartViewModel model, string cartCookie);

        List<AddToCartViewModel> RemoveProductFromCart(string cartCookie, int id);

        AddToCartViewModel? GetSelectedItemFromCart(string cartCookie, int id);

        List<AddToCartViewModel> SetEditItemInCart(string cartCookie, AddToCartViewModel model);

        Task<IEnumerable<AddToCartViewModel>> PurchaseItemAsync(string cartCookie);

        Task<bool> RequestItemAsync(string cartCookie, string userId);

    }
}