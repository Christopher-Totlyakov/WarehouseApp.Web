using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using WarehouseApp.Data.Models;
using WarehouseApp.Data.Repository.Interfaces;
using WarehouseApp.Services.Data;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Web.ViewModels.Message;

namespace WarehouseApp.Web.Controllers
{
    [Authorize]   
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

        [HttpGet]
        public IActionResult SendMessage() 
        {
            var message = new SendMessage();
            return View(message);
        }

        [HttpPost]
        public IActionResult SendMessage(SendMessage message)
        {
            if (!ModelState.IsValid)
            {
                return View(message);
            }
            //TODO validacia parst guid
           var idUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (String.IsNullOrWhiteSpace(idUser))
            {
                RedirectToAction(nameof(Index));
            }

            // Invalid parameter in the URL
            bool isGuidValid = Guid.TryParse(idUser, out Guid parsedGuid);
            if (!isGuidValid)
            {
                RedirectToAction(nameof(Index));
            }


            messageService.SendMess(message, parsedGuid);

            return RedirectToAction(nameof(Index));
        }
    }
}
