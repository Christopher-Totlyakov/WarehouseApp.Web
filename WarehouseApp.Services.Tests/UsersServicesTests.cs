using Microsoft.AspNetCore.Identity;
using MockQueryable;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models.Users;
using WarehouseApp.Services.Data;

namespace WarehouseApp.Services.Tests
{
    [TestFixture]
    public class UsersServicesTests
    {
        private Mock<UserManager<ApplicationUser>> userManagerMock;
        private UsersServices usersServices;

        [SetUp]
        public void SetUp()
        {
            var storeMock = new Mock<IUserStore<ApplicationUser>>();
            userManagerMock = new Mock<UserManager<ApplicationUser>>(storeMock.Object, null, null, null, null, null, null, null, null);
            usersServices = new UsersServices(userManagerMock.Object);
        }

        [Test]
        public async Task DeletePersonalDataAsync_ShouldReturnFalseWhenUserNotFound()
        {
            var userId = Guid.NewGuid().ToString();
            userManagerMock.Setup(m => m.Users).Returns(Enumerable.Empty<ApplicationUser>().BuildMock());

            var result = await usersServices.DeletePersonalDataAsync(userId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeletePersonalDataAsync_ShouldSetPersonalDataToDeleted()
        {
            var userId = Guid.NewGuid().ToString();
            var user = new WarehouseWorker
            {
                Id = Guid.Parse(userId),
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                StartWork = DateTime.UtcNow
            };

            var users = new List<ApplicationUser> { user }.BuildMock();
            userManagerMock.Setup(m => m.Users).Returns(users);
            userManagerMock.Setup(m => m.UpdateAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);

            var result = await usersServices.DeletePersonalDataAsync(userId);

            Assert.IsTrue(result);
            Assert.That(user.FirstName, Is.EqualTo("[Deleted]"));
            Assert.That(user.LastName, Is.EqualTo("[Deleted]"));
            Assert.That(user.EndWork?.ToString("yyyy-MM-dd"), Is.EqualTo(DateTime.UtcNow.ToString("yyyy-MM-dd")));
        }

        [Test]
        public async Task DeletePersonalDataAsync_ShouldHandleNonStringProperties()
        {
            var userId = Guid.NewGuid().ToString();
            var user = new Supplier
            {
                Id = Guid.Parse(userId),
                Email = "supplier@example.com",
                CompanyName = "Test Company",
                factoryLocation = "Test Location",
                PreferredDeliveryMethod = "Courier"
            };

            var users = new List<ApplicationUser> { user }.BuildMock();
            userManagerMock.Setup(m => m.Users).Returns(users);
            userManagerMock.Setup(m => m.UpdateAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);

            var result = await usersServices.DeletePersonalDataAsync(userId);

            Assert.IsTrue(result);
            Assert.That(user.CompanyName, Is.EqualTo("[Deleted]"));
            Assert.That(user.factoryLocation, Is.EqualTo("[Deleted]"));
            Assert.That(user.PreferredDeliveryMethod, Is.EqualTo("[Deleted]"));
        }

        [Test]
        public async Task DeletePersonalDataAsync_ShouldNotModifyIdOrUserName()
        {
            var userId = Guid.NewGuid().ToString();
            var user = new ApplicationUser
            {
                Id = Guid.Parse(userId),
                UserName = "testuser",
                Email = "test@example.com"
            };

            var users = new List<ApplicationUser> { user }.BuildMock();
            userManagerMock.Setup(m => m.Users).Returns(users);
            userManagerMock.Setup(m => m.UpdateAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);

            var result = await usersServices.DeletePersonalDataAsync(userId);

            Assert.IsTrue(result);
            Assert.That(user.Id.ToString(), Is.EqualTo(userId));
            Assert.That(user.UserName, Is.EqualTo("testuser"));
        }

        [Test]
        public async Task DeletePersonalDataAsync_ShouldReturnFalseWhenUpdateFails()
        {
            var userId = Guid.NewGuid().ToString();
            var user = new ApplicationUser
            {
                Id = Guid.Parse(userId),
                Email = "test@example.com"
            };

            var users = new List<ApplicationUser> { user }.BuildMock();
            userManagerMock.Setup(m => m.Users).Returns(users);
            userManagerMock.Setup(m => m.UpdateAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Update failed" }));

            var result = await usersServices.DeletePersonalDataAsync(userId);

            Assert.IsFalse(result);
        }
    }
}
