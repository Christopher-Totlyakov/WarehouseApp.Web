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
using WarehouseApp.Web.Authorize;
using WarehouseApp.Web.ViewModels.Message;
using WarehouseApp.Web.ViewModels.Orders;

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
            if (User.HasClaim("UserType", "WarehouseWorker"))
            {
                var messages = await messageService.GetAllMessageAsync();
                return View(messages);
            }
            else 
            {
                var idUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                bool isGuidValid = Guid.TryParse(idUser, out Guid parsedGuid);

                if (!isGuidValid)
                {
                    return RedirectToAction("Index", "Home");
                }

                var messages = await messageService.GetAllMessageFromCurrentUserAsync(parsedGuid);
                return View(messages);
            }
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
        [SenderMessageUserAuthorize]
        public IActionResult SendMessage() 
        {
            var message = new SendMessage();
            return View(message);
        }

        [HttpPost]
        [SenderMessageUserAuthorize]
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
                return RedirectToAction(nameof(Index));
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
        [HttpGet]
        [WarehouseWorkerAuthorize]
        public async Task<IActionResult> EditStatus(int Id)
        {
            var order = await messageService.GetMessageByIdAsync(Id);
            if (order == null)
            {
                return NotFound();
            }

            var statusOptions = new List<string> { "Unread", "Read", "Cancelled", "Accept" };

            var model = new EditMessageStatusViewModel
            {
                MessageId = order.MessageId,
                StatusOptions = statusOptions,
                Status = order.Status
            };

            return View(model);
        }

        [HttpPost]
        [WarehouseWorkerAuthorize]
        public async Task<IActionResult> UpdateStatus(EditMessageStatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("EditStatus", model);
            }

            bool isSucceeded = await messageService.EditMessagesStatusAsync(model);

            if (!isSucceeded)
            {
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
