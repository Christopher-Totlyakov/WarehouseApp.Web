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
using WarehouseApp.Web.ViewModels.Message;

namespace WarehouseApp.Services.Tests
{
    [TestFixture]
    public class MessageServiceTests
    {
        private Mock<IRepository> repositoryMock;
        private MessageService messageService;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IRepository>();
            messageService = new MessageService(repositoryMock.Object);
        }

        [Test]
        public async Task GetAllMessageAsync_ShouldReturnEmptyList_WhenNoMessagesExist()
        {
            repositoryMock.Setup(r => r.GetAllAttached<Message>())
                .Returns(new List<Message>().BuildMock());

            var result = await messageService.GetAllMessageAsync();

            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task GetAllMessageFromCurrentUserAsync_ShouldReturnMessagesForUser()
        {
            var userId = Guid.NewGuid();
            var messages = new List<Message>
            {
                new Message { MessageId = 1, SenderId = userId, Sender = new SenderMessageUser(){ Email = "123@gmail.com"}, MessageContent = "Test Message 1", MessageType = "Error" , SentDate = DateTime.UtcNow , Status = "read", SoftDelete =false},
                new Message { MessageId = 2, SenderId = userId, Sender = new SenderMessageUser(){ Email = "99@gmail.com"}, MessageContent = "Test Message 2" ,  MessageType = "Error" , SentDate = DateTime.UtcNow , Status = "read", SoftDelete =false ,}
            };

            repositoryMock.Setup(r => r.GetAllAttached<Message>())
                .Returns(messages.BuildMock());

            var result = await messageService.GetAllMessageFromCurrentUserAsync(userId);

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.IsTrue(result.All(m => m.SenderEmail != null));
        }

        [Test]
        public async Task GetMessageByIdAsync_ShouldReturnCorrectMessage()
        {
            var userId = Guid.NewGuid();
            var messages = new List<Message>
            {
                new Message { MessageId = 1, SenderId = userId, Sender = new SenderMessageUser(){ Email = "123@gmail.com"}, MessageContent = "Test Message 1", MessageType = "Error" , SentDate = DateTime.UtcNow , Status = "read", SoftDelete =false},
                new Message { MessageId = 2, SenderId = userId, Sender = new SenderMessageUser(){ Email = "99@gmail.com"}, MessageContent = "Test Message 2" ,  MessageType = "Error" , SentDate = DateTime.UtcNow , Status = "read", SoftDelete =false ,}
            };

            repositoryMock.Setup(r => r.GetAllAttached<Message>())
                .Returns(messages.BuildMock());

            var result = await messageService.GetMessageByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.That(result.MessageContent, Is.EqualTo("Test Message 1"));
        }

        [Test]
        public async Task SendMess_ShouldReturnFalse_WhenRepositoryThrowsException()
        {
            var message = new SendMessage
            {
                MessageType = "Test Type",
                MessageContent = "Test Content"
            };

            repositoryMock.Setup(r => r.AddAsync(It.IsAny<Message>())).Throws(new Exception());

            var result = await messageService.SendMess(message, Guid.NewGuid());

            Assert.IsFalse(result);
        }

        [Test]
        public async Task EditMessagesStatusAsync_ShouldReturnFalse_WhenMessageNotFound()
        {
            repositoryMock.Setup(r => r.GetByIdAsync<Message, int>(It.IsAny<int>()))
                .ReturnsAsync((Message)null);

            var model = new EditMessageStatusViewModel { MessageId = 1, Status = "Read" };

            var result = await messageService.EditMessagesStatusAsync(model);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task EditMessagesStatusAsync_ShouldReturnFalse_WhenRepositoryThrowsException()
        {
            var message = new Message { MessageId = 1, Status = "Unread" };
            repositoryMock.Setup(r => r.GetByIdAsync<Message, int>(1)).ReturnsAsync(message);
            repositoryMock.Setup(r => r.UpdateAsync(message)).Throws(new Exception());

            var model = new EditMessageStatusViewModel { MessageId = 1, Status = "Read" };

            var result = await messageService.EditMessagesStatusAsync(model);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task EditMessagesStatusAsync_ShouldUpdateMessageStatus()
        {
            var message = new Message { MessageId = 1, Status = "Unread" };
            repositoryMock.Setup(r => r.GetByIdAsync<Message, int>(1)).ReturnsAsync(message);

            var model = new EditMessageStatusViewModel { MessageId = 1, Status = "Read" };

            var result = await messageService.EditMessagesStatusAsync(model);

            Assert.IsTrue(result);
            Assert.That(message.Status, Is.EqualTo("Read"));
        }
    }
}
