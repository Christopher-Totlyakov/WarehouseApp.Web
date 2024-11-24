using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseApp.Data.Models;
using WarehouseApp.Data.Repository.Interfaces;

using WarehouseApp.Web.ViewModels.Message;

namespace WarehouseApp.Web.Controllers
{
    public class MessageController : Controller
    {
        private readonly IRepository repository;

        public MessageController(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IActionResult> Index()
        {
            var messages = await repository.GetAllAttached<Message>()
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
            .Select(m => new MessageViewModel
            {
                MessageId = m.MessageId,
                SenderId = m.SenderId,
                SenderEmail = m.Sender.Email!, 
                ReceiverId = m.ReceiverId,
                ReceiverName = m.Receiver != null ? m.Receiver.FirstName + m.Receiver.LastName : null,
                MessageType = m.MessageType,
                MessageContent = m.MessageContent,
                SentDate = m.SentDate,
                Status = m.Status
            })
            .ToListAsync();

            return View(messages);
        }
        [HttpGet]
		public async Task<IActionResult> ReadMessage(int id)
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

			if (message == null)
			{
				return NotFound();
			}

			return PartialView("~/Views/Shared/_ReadMessagePartial.cshtml", message);
		}
	}
}
