using Azure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WarehouseApp.Data.Models;
using WarehouseApp.Data.Repository.Interfaces;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Services.Mapping;
using WarehouseApp.Web.ViewModels.ShoppingCart;


namespace WarehouseApp.Services.Data
{
    public class ShoppingCartService : IShoppingCartService
    {
        private IRepository repository;

        public ShoppingCartService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<ShoppingCartItems>> GetChoiceProductsAsync(string cartCookie)
        {
            var items = cartCookie != null
                ? JsonSerializer.Deserialize<List<AddToCartViewModel>>(cartCookie)
                : new List<AddToCartViewModel>();

            var productIds = items.Select(item => item.ProductId).ToList();

            var products = await repository.GetAllAttached<Product>()
            .Where(p => productIds.Contains(p.Id))
            .To<ShoppingCartItems>()
            .ToListAsync();
            foreach (var product in products)
            {
                product.Quantity = items.FirstOrDefault(c => c.ProductId == product.Id).Quantity;
            }
            return products;
        }

        public List<AddToCartViewModel> SetProductsInCooke(AddToCartViewModel model, string cartCookie)
        {
            var cart = cartCookie != null
                ? JsonSerializer.Deserialize<List<AddToCartViewModel>>(cartCookie)
                : new List<AddToCartViewModel>();


            var existingItem = cart.FirstOrDefault(x => x.ProductId == model.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += model.Quantity;
            }
            else
            {
                cart.Add(new AddToCartViewModel { ProductId = model.ProductId, Quantity = model.Quantity });
            }

            return cart;
           
        }

        public List<AddToCartViewModel> RemoveProductFromCart(string cartCookie, int id)
        {
            var cart = cartCookie != null
                ? JsonSerializer.Deserialize<List<AddToCartViewModel>>(cartCookie)
                : new List<AddToCartViewModel>();

            cart = cart.Where(item => item.ProductId != id).ToList();

            return cart;
        }
    }
}
