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
    public class RequestServiceTests
    {
        private Mock<IRepository> repository;
        private RequestService requestService;

        [SetUp]
        public void SetUp()
        {
            repository = new Mock<IRepository>();
            requestService = new RequestService(repository.Object);
        }

        [Test]
        public async Task GetAllRequestsAsync_ShouldReturnAllRequests()
        {
            var requestData = new List<Request>
            {
                new Request
                {
                    RequestId = 1,
                    Requester = new RequesterAndBuyerUser { Email = "user1@example.com" },
                    Status = "Pending",
                    RequestDate = System.DateTime.Now
                },
                new Request
                {
                    RequestId = 2,
                    Requester = new RequesterAndBuyerUser { Email = "user2@example.com" },
                    Status = "Approved",
                    RequestDate = System.DateTime.Now
                }
            }.AsQueryable().BuildMock();

            repository.Setup(r => r.GetAllAttached<Request>()).Returns(requestData);

            var result = await requestService.GetAllRequestsAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());

            var requestList = result.ToList();
            Assert.That(requestList[0].RequestId, Is.EqualTo(1));
            Assert.That(requestList[0].RequesterEmail, Is.EqualTo("user1@example.com"));
            Assert.That(requestList[0].Status, Is.EqualTo("Pending"));
        }

        [Test]
        public async Task GetRequestDetailsAsync_ShouldReturnRequestDetails()
        {
            var requestData = new List<Request>
            {
                new Request
                {
                    RequestId = 1,
                    Requester = new RequesterAndBuyerUser { Email = "user1@example.com" },
                    Status = "Pending",
                    RequestDate = System.DateTime.Now,
                    ProcessedByWorker = new WarehouseWorker { Email = "worker1@example.com" },
                    Note = "Urgent",
                    RequestProducts = new List<RequestProduct>
                    {
                        new RequestProduct
                        {
                            Product = new Product { Name = "Product1" },
                            QuantityRequested = 2,
                            PriceUponRequest = 15.0m
                        }
                    }
                }
            }.BuildMock();

            repository.Setup(r => r.GetAllAttached<Request>()).Returns(requestData);

            var result = await requestService.GetRequestDetailsAsync(1);

            Assert.IsNotNull(result);
            Assert.That(result.RequestId, Is.EqualTo(1));
            Assert.That(result.RequesterEmail, Is.EqualTo("user1@example.com"));
            Assert.That(result.Status, Is.EqualTo("Pending"));
            Assert.That(result.ProcessedByWorkerEmail, Is.EqualTo("worker1@example.com"));
            Assert.That(result.Note, Is.EqualTo("Urgent"));
            Assert.That(result.Products.Count(), Is.EqualTo(1));
            Assert.That(result.Products.First().ProductName, Is.EqualTo("Product1"));
            Assert.That(result.Products.First().QuantityRequested, Is.EqualTo(2));
            Assert.That(result.Products.First().PriceUponRequest, Is.EqualTo(15.0m));
        }

        [Test]
        public async Task GetRequestDetailsAsync_ShouldReturnNull()
        {
            var requestData = new List<Request>().BuildMock();
            repository.Setup(r => r.GetAllAttached<Request>()).Returns(requestData);

            var result = await requestService.GetRequestDetailsAsync(999);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetRequestDetailsAsync_RequestWithMultipleProducts()
        {
            var requestData = new List<Request>
            {
                new Request
                {
                    RequestId = 1,
                    Requester = new RequesterAndBuyerUser { Email = "test@example.com" },
                    Status = "Pending",
                    RequestDate = DateTime.UtcNow,
                    RequestProducts = new List<RequestProduct>
                    {
                        new RequestProduct { Product = new Product { Name = "Product1" }, QuantityRequested = 2, PriceUponRequest = 10.5M },
                        new RequestProduct { Product = new Product { Name = "Product2" }, QuantityRequested = 1, PriceUponRequest = 20.0M }
                    }
                }
            }.BuildMock();

            repository.Setup(r => r.GetAllAttached<Request>())
                .Returns(requestData);
            
            var result = await requestService.GetRequestDetailsAsync(1);
            
            Assert.IsNotNull(result);
            Assert.That(result.Products.Count(), Is.EqualTo(2));
            Assert.That(result.Products.First().ProductName, Is.EqualTo("Product1"));
            Assert.That(result.Products.Last().ProductName, Is.EqualTo("Product2"));
        }
    }
}

