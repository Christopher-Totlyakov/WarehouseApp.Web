using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models;
using WarehouseApp.Data.Repository;
using WarehouseApp.Data.Repository.Interfaces;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Services.Mapping;
using WarehouseApp.Web.ViewModels.Message;
using WarehouseApp.Web.ViewModels.Product;

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
            var messages = await repository.GetAllAttached<Message>()
                    .Include(m => m.Sender)
                    .Include(m => m.Receiver)
                        .To<MessageViewModel>()
                        .ToListAsync();

            return messages;
        }

        public async Task<MessageViewModel> GetMessageByIdAsync(int id) 
        {
            var message = await repository.GetAllAttached<Message>()
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
    }
}
