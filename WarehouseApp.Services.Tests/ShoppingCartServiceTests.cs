using MockQueryable;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WarehouseApp.Data.Models;
using WarehouseApp.Data.Repository.Interfaces;
using WarehouseApp.Services.Data;
using WarehouseApp.Web.ViewModels.ShoppingCart;

namespace WarehouseApp.Services.Tests
{
    [TestFixture]
    class ShoppingCartServiceTests
    {
        private Mock<IRepository> repositoryMock;
        private ShoppingCartService shoppingCartService;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IRepository>();
            shoppingCartService = new ShoppingCartService(repositoryMock.Object);
        }

        [Test]
        public async Task GetChoiceProductsAsync_ShouldReturnCorrectProducts()
        {
            var cartCookie = JsonSerializer.Serialize(new List<AddToCartViewModel>
            {
            new AddToCartViewModel { ProductId = 1, Quantity = 2 },
            new AddToCartViewModel { ProductId = 2, Quantity = 3 }
            });

            var products = new List<Product>
            {
            new Product { Id = 1, Name = "Product 1", Price = 10.0M, StockQuantity = 5, SoftDelete = false },
            new Product { Id = 2, Name = "Product 2", Price = 20.0M, StockQuantity = 2, SoftDelete = false }
            };

            repositoryMock.Setup(r => r.GetAllAttached<Product>()).Returns(products.BuildMock());

            var result = await shoppingCartService.GetChoiceProductsAsync(cartCookie);

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.IsTrue(result.Any(p => p.Name == "Product 1" && p.InStok));
            Assert.IsTrue(result.Any(p => p.Name == "Product 2" && !p.InStok));
        }

        [Test]
        public void AddProductsInCooke_ShouldAddProductCorrectly()
        {
            var cartCookie = JsonSerializer.Serialize(new List<AddToCartViewModel>
            {
            new AddToCartViewModel { ProductId = 1, Quantity = 2 }
            });

            var newProduct = new AddToCartViewModel { ProductId = 2, Quantity = 3 };

            var result = shoppingCartService.AddProductsInCooke(newProduct, cartCookie);

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.IsTrue(result.Any(p => p.ProductId == 1 && p.Quantity == 2));
            Assert.IsTrue(result.Any(p => p.ProductId == 2 && p.Quantity == 3));
        }

        [Test]
        public void RemoveProductFromCart_ShouldRemoveCorrectly()
        {
            var cartCookie = JsonSerializer.Serialize(new List<AddToCartViewModel>
            {
            new AddToCartViewModel { ProductId = 1, Quantity = 2 },
            new AddToCartViewModel { ProductId = 2, Quantity = 3 }
            });

            var result = shoppingCartService.RemoveProductFromCart(cartCookie, 1);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.IsFalse(result.Any(p => p.ProductId == 1));
        }

        [Test]
        public void GetSelectedItemFromCart_ShouldReturnCorrectItem()
        {
            var cartCookie = JsonSerializer.Serialize(new List<AddToCartViewModel>
            {
            new AddToCartViewModel { ProductId = 1, Quantity = 2 },
            new AddToCartViewModel { ProductId = 2, Quantity = 3 }
            });

            var result = shoppingCartService.GetSelectedItemFromCart(cartCookie, 1);

            Assert.IsNotNull(result);
            Assert.That(result.ProductId, Is.EqualTo(1));
            Assert.That(result.Quantity, Is.EqualTo(2));
        }

        [Test]
        public async Task PurchaseItemAsync_ShouldOnlyPurchaseInStockItems()
        {
            var cartCookie = JsonSerializer.Serialize(new List<AddToCartViewModel>
        {
            new AddToCartViewModel { ProductId = 1, Quantity = 2 },
            new AddToCartViewModel { ProductId = 2, Quantity = 3 }
        });

            var products = new List<Product>
            {
            new Product { Id = 1, Name = "Product 1", Price = 10.0M, StockQuantity = 5, SoftDelete = false },
            new Product { Id = 2, Name = "Product 2", Price = 20.0M, StockQuantity = 2, SoftDelete = false }
            };

            repositoryMock.Setup(r => r.GetAllAttached<Product>()).Returns(products.BuildMock());

            var result = await shoppingCartService.PurchaseItemAsync(cartCookie, Guid.NewGuid().ToString());

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.IsTrue(result.Any(p => p.ProductId == 2));
            repositoryMock.Verify(r => r.AddAsync(It.IsAny<Sale>()), Times.Once);
        }

        [Test]
        public async Task RequestItemAsync_ShouldReturnFalseWhenInvalidUserId()
        {
            var cartCookie = JsonSerializer.Serialize(new List<AddToCartViewModel>
            {
                new AddToCartViewModel { ProductId = 1, Quantity = 2 }
            });

            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10.0M, StockQuantity = 5, SoftDelete = false }
            };

            var mockProducts = products.AsQueryable().BuildMock();
            repositoryMock.Setup(r => r.GetAllAttached<Product>()).Returns(mockProducts);

            var result = await shoppingCartService.RequestItemAsync(cartCookie, "InvalidGuid");

            Assert.IsFalse(result);
        }


        [Test]
        public async Task RequestItemAsync_ShouldHandleRequestCorrectly()
        {
            var cartCookie = JsonSerializer.Serialize(new List<AddToCartViewModel>
            {
            new AddToCartViewModel { ProductId = 1, Quantity = 2 }
            });

            var products = new List<Product>
            {
            new Product { Id = 1, Name = "Product 1", Price = 10.0M, StockQuantity = 5, SoftDelete = false }
            };

            repositoryMock.Setup(r => r.GetAllAttached<Product>()).Returns(products.BuildMock());

            var result = await shoppingCartService.RequestItemAsync(cartCookie, Guid.NewGuid().ToString());

            Assert.IsTrue(result);
            repositoryMock.Verify(r => r.AddAsync(It.IsAny<Request>()), Times.Once);
        }
    }
}
