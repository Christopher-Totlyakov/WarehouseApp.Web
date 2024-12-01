using Azure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WarehouseApp.Data.Models;
using WarehouseApp.Data.Models.Interfaces;
using WarehouseApp.Data.Models.Users;
using WarehouseApp.Data.Repository;
using WarehouseApp.Data.Repository.Interfaces;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Services.Mapping;
using WarehouseApp.Web.ViewModels.ShoppingCart;
using static WarehouseApp.Common.EntityValidationConstants;


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

            var products = await repository.GetAllAttached<WarehouseApp.Data.Models.Product>()
                    .Where(p => productIds.Contains(p.Id) && p.SoftDelete == false)
                    .ToListAsync();

            var shoppingCartItems = products.Select(p =>
            {
                var cartItem = items.FirstOrDefault(i => i.ProductId == p.Id && p.SoftDelete == false);
                return new ShoppingCartItems
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = cartItem?.Quantity ?? 0,
                    InStok = p.StockQuantity >= (cartItem?.Quantity ?? 0)
                };
            }).ToList();

            return shoppingCartItems;
        }

        public List<AddToCartViewModel> AddProductsInCooke(AddToCartViewModel model, string cartCookie)
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

        public AddToCartViewModel? GetSelectedItemFromCart(string cartCookie, int id)
        {
            var cart = cartCookie != null
                ? JsonSerializer.Deserialize<List<AddToCartViewModel>>(cartCookie)
                : new List<AddToCartViewModel>();

            AddToCartViewModel? model = cart.FirstOrDefault(item => item.ProductId == id);

            return model;
        }

        public List<AddToCartViewModel> SetEditItemInCart(string cartCookie, AddToCartViewModel model)
        {
            var cart = cartCookie != null
               ? JsonSerializer.Deserialize<List<AddToCartViewModel>>(cartCookie)
               : new List<AddToCartViewModel>();


            var existingItem = cart.FirstOrDefault(x => x.ProductId == model.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity = model.Quantity;
            }

            return cart;
        }

        public async Task<IEnumerable<AddToCartViewModel>> PurchaseItemAsync(string cartCookie, string userId)
        {

            var cartItems = cartCookie != null
                ? JsonSerializer.Deserialize<List<AddToCartViewModel>>(cartCookie)
                : new List<AddToCartViewModel>();

            var productIds = cartItems.Select(x => x.ProductId).ToList();
            var products = await repository.GetAllAttached<WarehouseApp.Data.Models.Product>()
                .Where(p => productIds.Contains(p.Id) && p.SoftDelete == false)
                .ToListAsync();

            var inStockItems = cartItems.Where(item =>
                products.Any(p => p.Id == item.ProductId && p.StockQuantity >= item.Quantity)).ToList();

            bool isGuidValid = Guid.TryParse(userId, out Guid parsedGuid);
            if (!isGuidValid)
            {
                return cartItems;
            }
            if (inStockItems.Any())
            {
                // Create a new Sale record
                var sale = new WarehouseApp.Data.Models.Sale
                {
                    CustomerId = parsedGuid,
                    SaleDate = DateTime.UtcNow,
                    TotalAmount = inStockItems.Sum(item =>
                        item.Quantity * products.First(p => p.Id == item.ProductId).Price),
                    SaleProducts = inStockItems.Select(item =>
                    {
                        var product = products.First(p => p.Id == item.ProductId);
                        return new WarehouseApp.Data.Models.SaleProduct
                        {
                            ProductId = product.Id,
                            QuantitySold = item.Quantity,
                            UnitPrice = product.Price
                        };
                    }).ToList()
                };


                foreach (var item in inStockItems)
                {
                    var product = products.First(p => p.Id == item.ProductId);
                    product.StockQuantity -= (uint)item.Quantity;
                }

                await repository.AddAsync(sale);
                await repository.SaveChangesAsync();

                var remainingItems = cartItems.Except(inStockItems).ToList();
                return remainingItems;
            }
            return cartItems;
        }

        public async Task<bool> RequestItemAsync(string cartCookie, string userId)
        {
            var cartItems = JsonSerializer.Deserialize<List<AddToCartViewModel>>(cartCookie);

            var productIds = cartItems.Select(x => x.ProductId).ToList();
            var products = await repository.GetAllAttached<WarehouseApp.Data.Models.Product>()
                .Where(p => productIds.Contains(p.Id) && p.SoftDelete == false)
                .ToListAsync();

            var requestProducts = cartItems.Select(item =>
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                return new RequestProductViewModel
                {
                    ProductId = item.ProductId,
                    ProductName = product?.Name ?? "Unknown",
                    Quantity = item.Quantity,
                    IsInStock = product != null && product.StockQuantity >= item.Quantity,
                    PriceUponRequest = product?.Price ?? 0
                };
            }).ToList();

            bool isGuidValid = Guid.TryParse(userId, out Guid parsedGuid);
            if (!isGuidValid)
            {
                return false;
            }
            // Прехвърлете данните към модел за базата данни (ако е необходимо)
            var request = new WarehouseApp.Data.Models.Request
            {
                RequesterId = parsedGuid, // Метод за извличане на текущия потребител
                RequestDate = DateTime.UtcNow,
                Status = "Pending",

                RequestProducts = requestProducts.Select(rp => new WarehouseApp.Data.Models.RequestProduct
                {
                    ProductId = rp.ProductId,
                    QuantityRequested = rp.Quantity,
                    PriceUponRequest = rp.PriceUponRequest
                }).ToList()
            };

            try
            {
                await repository.AddAsync(request);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
