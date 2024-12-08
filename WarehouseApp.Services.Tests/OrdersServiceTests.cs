using MockQueryable;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models;
using WarehouseApp.Data.Models.Users;
using WarehouseApp.Data.Repository.Interfaces;
using WarehouseApp.Services.Data;
using WarehouseApp.Web.ViewModels.Orders;

namespace WarehouseApp.Services.Tests
{
    [TestFixture]
    public class OrdersServiceTests
    {
        private Mock<IRepository> repositoryMock;
        private OrdersService ordersService;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IRepository>();
            ordersService = new OrdersService(repositoryMock.Object);
        }

        [Test]
        public async Task AddOrderAsync_ShouldReturnFalseWhenRepositoryThrowsException()
        {
            var model = new OrderViewModel
            {
                SupplierId = Guid.NewGuid(),
                Status = "Pending",
                OrderProducts = new List<OrderProductViewModel>
                {
                    new OrderProductViewModel { ProductId = 1, QuantityOrdered = 10 }
                }
            };

            repositoryMock.Setup(r => r.AddAsync(It.IsAny<Order>()))
                          .ThrowsAsync(new Exception());

            var result = await ordersService.AddOrderAsync(model, Guid.NewGuid());

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetAllSuppliersAsync_ShouldExcludeDeletedSuppliers()
        {
            var suppliers = new List<Supplier>
            {
                new Supplier { Id = Guid.NewGuid(), CompanyName = "Supplier1", factoryLocation = "Location1", PreferredDeliveryMethod = "Truck" },
                new Supplier { Id = Guid.NewGuid(), CompanyName = "[Deleted]", factoryLocation = "[Deleted]", PreferredDeliveryMethod = "[Deleted]" }
            }.BuildMock();

            repositoryMock.Setup(r => r.GetAllAttached<Supplier>()).Returns(suppliers);

            var result = await ordersService.GetAllSuppliersAsync();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().CompanyName, Is.EqualTo("Supplier1"));
        }

        [Test]
        public async Task GetProductsFromRequestsAsync_ShouldAggregateQuantitiesCorrectly()
        {
            var requestProducts = new List<RequestProduct>
            {
                new RequestProduct { ProductId = 1, QuantityRequested = 5, Product = new Product { Id = 1, Name = "Product1" } },
                new RequestProduct { ProductId = 1, QuantityRequested = 10, Product = new Product { Id = 1, Name = "Product1" } }
            }.BuildMock();

            repositoryMock.Setup(r => r.GetAllAttached<RequestProduct>()).Returns(requestProducts);

            var result = await ordersService.GetProductsFromRequestsAsync();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().QuantityOrdered, Is.EqualTo(15));
        }

        [Test]
        public async Task EditOrderStatusAsync_ShouldReturnFalseWhenOrderNotFound()
        {
            var model = new EditOrderStatusViewModel { OrderId = 1, Status = "Completed" };

            repositoryMock.Setup(r => r.GetByIdAsync<Order, int>(1))
                          .ReturnsAsync((Order)null);

            var result = await ordersService.EditOrderStatusAsync(model);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task EditOrderStatusAsync_ShouldReturnFalseWhenRepositoryThrowsException()
        {
            var model = new EditOrderStatusViewModel { OrderId = 1, Status = "Completed" };
            var order = new Order { OrderId = 1, Status = "Pending" };

            repositoryMock.Setup(r => r.GetByIdAsync<Order, int>(1))
                          .ReturnsAsync(order);

            repositoryMock.Setup(r => r.UpdateAsync(order))
                          .ThrowsAsync(new Exception());

            var result = await ordersService.EditOrderStatusAsync(model);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetAllOrdersAsync_ShouldMapCorrectly()
        {
            var orders = new List<Order>
            {
                new Order
                {
                    OrderId = 1,
                    OrderDate = new DateTime(2023, 1, 1),
                    Status = "Pending",
                    Supplier = new Supplier { UserName = "Supplier1" }
                }
            }.BuildMock();

            repositoryMock.Setup(r => r.GetAllAttached<Order>()).Returns(orders);

            var result = await ordersService.GetAllOrdersAsync();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().SupplierName, Is.EqualTo("Supplier1"));
        }

        [Test]
        public async Task GetOrderDetailsAsync_ShouldReturnNullWhenOrderNotFound()
        {
            repositoryMock.Setup(r => r.GetAllAttached<Order>())
                          .Returns(Enumerable.Empty<Order>().BuildMock());

            var result = await ordersService.GetOrderDetailsAsync(1);

            Assert.IsNull(result);
        }
    }
}

