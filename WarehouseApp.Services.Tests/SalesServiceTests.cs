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

namespace WarehouseApp.Services.Tests
{
    [TestFixture]
    public class SalesServiceTests
    {
        private Mock<IRepository> repository;
        private SalesService salesService;

        [SetUp]
        public void Setup()
        {
            repository = new Mock<IRepository>();
            salesService = new SalesService(repository.Object);
        }

        [Test]
        public async Task GetAllSalesAsync_ShouldReturnAllSales()
        {
            var salesData = new List<Sale>
            {
                new Sale { SaleId = 1, Customer = new RequesterAndBuyerUser { UserName = "Customer1" }, SaleDate = DateTime.UtcNow, TotalAmount = 100 },
                new Sale { SaleId = 2, Customer = new RequesterAndBuyerUser { UserName = "Customer2" }, SaleDate = DateTime.UtcNow, TotalAmount = 200 }
            }.BuildMock();

            repository.Setup(r => r.GetAllAttached<Sale>()).Returns(salesData);

            var result = await salesService.GetAllSalesAsync();

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().CustomerName, Is.EqualTo("Customer1"));
        }

        [Test]
        public async Task GetAllSalesAsync_ShouldHandleEmptyDatabase()
        {
            repository.Setup(r => r.GetAllAttached<Sale>()).Returns(new List<Sale>().BuildMock());

            var result = await salesService.GetAllSalesAsync();

            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task GetSaleDetailsAsync_ShouldReturnCorrectDetails()
        {
            var saleData = new Sale
            {
                SaleId = 1,
                Customer = new RequesterAndBuyerUser { UserName = "Customer1" },
                SaleDate = DateTime.UtcNow,
                TotalAmount = 150,
                SaleProducts = new List<SaleProduct>
                {
                    new SaleProduct { Product = new Product { Name = "Product1" }, QuantitySold = 2, UnitPrice = 50 },
                    new SaleProduct { Product = new Product { Name = "Product2" }, QuantitySold = 1, UnitPrice = 50 }
                }
            };

            repository.Setup(r => r.GetAllAttached<Sale>()).Returns(new List<Sale> { saleData }.BuildMock());

            var result = await salesService.GetSaleDetailsAsync(1);

            Assert.IsNotNull(result);
            Assert.That(result.SaleId, Is.EqualTo(1));
            Assert.That(result.CustomerName, Is.EqualTo("Customer1"));
            Assert.That(result.Products.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetSaleDetailsAsync_ShouldThrowExceptionWhenSaleNotFound()
        {
            repository.Setup(r => r.GetAllAttached<Sale>()).Returns(new List<Sale>().BuildMock());

            Assert.ThrowsAsync<ArgumentException>(async () => await salesService.GetSaleDetailsAsync(999), "Sale not found.");
        }

        [Test]
        public async Task GetAllSalesAsync_ShouldHandleSalesWithoutCustomer()
        {
            var salesData = new List<Sale>
            {
                new Sale { SaleId = 1, Customer = new RequesterAndBuyerUser { UserName = null }, SaleDate = DateTime.UtcNow, TotalAmount = 100 }
            }.BuildMock();

            repository.Setup(r => r.GetAllAttached<Sale>()).Returns(salesData);

            var result = await salesService.GetAllSalesAsync();

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.IsNull(result.First().CustomerName);
        }

        [Test]
        public async Task GetSaleDetailsAsync_ShouldHandleSaleWithMissingProducts()
        {
            var saleData = new Sale
            {
                SaleId = 1,
                Customer = new RequesterAndBuyerUser { UserName = "Customer1" },
                SaleDate = DateTime.UtcNow,
                TotalAmount = 150,
                SaleProducts = new List<SaleProduct>()
            };

            repository.Setup(r => r.GetAllAttached<Sale>()).Returns(new List<Sale> { saleData }.BuildMock());

            var result = await salesService.GetSaleDetailsAsync(1);

            Assert.IsNotNull(result);
            Assert.That(result.SaleId, Is.EqualTo(1));
            Assert.That(result.CustomerName, Is.EqualTo("Customer1"));
            Assert.IsEmpty(result.Products);
        }

        [Test]
        public async Task GetAllSalesAsync_ShouldHandleLargeNumberOfSales()
        {
            var largeSalesList = new List<Sale>();
            for (int i = 0; i < 10000; i++)
            {
                largeSalesList.Add(new Sale
                {
                    SaleId = i,
                    Customer = new RequesterAndBuyerUser { UserName = $"Customer{i}" },
                    SaleDate = DateTime.UtcNow.AddDays(-i),
                    TotalAmount = i * 10
                });
            }

            repository.Setup(r => r.GetAllAttached<Sale>()).Returns(largeSalesList.BuildMock());

            var result = await salesService.GetAllSalesAsync();

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(10000));
            Assert.That(result.First().CustomerName, Is.EqualTo("Customer0"));
        }
    }
}
