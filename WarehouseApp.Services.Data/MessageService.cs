using Microsoft.EntityFrameworkCore;
using WarehouseApp.Data;
using WarehouseApp.Data.Repository.Interfaces;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Services.Mapping;
using WarehouseApp.Web.ViewModels.Message;
using static WarehouseApp.Common.EntityValidationConstants;

namespace WarehouseApp.Services.Data
{
    public class MessageService : IMessageServices
    {
        private IRepository repository;

        public MessageService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<MessageViewModel>> GetAllMessageAsync()
        {
            var messages = await repository.GetAllAttached<WarehouseApp.Data.Models.Message>()
                    .Include(m => m.Sender)
                    .Include(m => m.Receiver)
                        .To<MessageViewModel>()
                        .ToListAsync();
            
            return messages;
        }

        public async Task<MessageViewModel> GetMessageByIdAsync(int id) 
        {
            var message = await repository.GetAllAttached<WarehouseApp.Data.Models.Message>()
                    .Include(m => m.Sender)
                    .Include(m => m.Receiver)
                    .Where(m => m.MessageId == id)
                    .Select(m => new MessageViewModel
                    {
                        MessageId = m.MessageId,
                        SenderEmail = m.Sender.Email!,
                        ReceiverName = m.Receiver != null ? m.Receiver.FirstName + " " + m.Receiver.LastName : "Not assigned",
                        MessageType = m.MessageType,
                        MessageContent = m.MessageContent,
                        SentDate = m.SentDate,
                        Status = m.Status
                    })
                    .FirstOrDefaultAsync();
            return message;
        }

        public async Task<bool> SendMess(SendMessage message, Guid idUser) 
        {
            var newMessage = new WarehouseApp.Data.Models.Message()
            {
                SenderId = idUser,
                MessageType = message.MessageType,
                MessageContent = message.MessageContent,
                SentDate = DateTime.UtcNow,
                Status = "Unread"
                
            };

            try
            {
                await repository.AddAsync<WarehouseApp.Data.Models.Message>(newMessage);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
