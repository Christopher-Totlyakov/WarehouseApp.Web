using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseApp.Services.Data;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Web.Authorize;
using WarehouseApp.Web.ViewModels.Admin;

namespace WarehouseApp.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
    [WarehouseWorkerAuthorize]
    public class UserManagementController : Controller
    {
        private readonly IUsersServices userService;

        public UserManagementController(IUsersServices _userService, IUsersServices managerService)
        {
            userService = _userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await userService.GetAllUsersAsync();

            return View(users);
        }
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> DeletePersonalData(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var result = await userService.DeletePersonalDataAsync(id);

            if (!result)
            {
                TempData["ErrorMessage"] = "Failed to delete personal data.";
                return RedirectToAction("Details", new { id });
            }

            TempData["SuccessMessage"] = "Personal data deleted successfully.";
            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> ActivateAccount(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            await userService.ChangeAccountAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DeactivateAccount(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            await userService.ChangeAccountAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
