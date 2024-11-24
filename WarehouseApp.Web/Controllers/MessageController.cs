using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseApp.Data.Models;
using WarehouseApp.Data.Repository.Interfaces;
using WarehouseApp.Services.Data;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Web.ViewModels.Message;

namespace WarehouseApp.Web.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageServices messageService;

        public MessageController(IMessageServices _messageService)
        {
            messageService = _messageService;
        }

        public async Task<IActionResult> Index()
        {
            var messages = await messageService.GetAllMessageAsync();

            return View(messages);
        }

        [HttpGet]
		public async Task<IActionResult> ReadMessage(int id)
		{
            var message = await messageService.GetMessageByIdAsync(id);

            if (message == null)
			{
				return NotFound();
			}

			return PartialView("~/Views/Shared/_ReadMessagePartial.cshtml", message);
		}
	}
}
