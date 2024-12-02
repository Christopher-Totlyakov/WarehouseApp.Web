using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using WarehouseApp.Data.Models;
using WarehouseApp.Data.Models.Users;
using WarehouseApp.Data.Repository.Interfaces;

namespace WarehouseApp.Web.Authorize
{
    public class WarehouseWorkerAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Извличане на UserId от claims
            var userIdClaim = context.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                context.Result = new ForbidResult();
                return;
            }

            // Получаване на репозиторието
            var repository = context.HttpContext.RequestServices.GetService<IRepository>();
            if (repository == null)
            {
                context.Result = new StatusCodeResult(500); // Internal Server Error
                return;
            }

            // Проверка в базата дали потребителят е WarehouseWorker
            var isWarehouseWorker = repository.GetAllAttached<WarehouseWorker>().Any(w => w.Id == userId);

            if (!isWarehouseWorker)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
