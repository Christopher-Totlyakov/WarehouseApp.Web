using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WarehouseApp.Data.Models.Users;
using WarehouseApp.Data.Repository.Interfaces;

namespace WarehouseApp.Web.Authorize
{
    public class RequesterAndBuyerUserAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userIdClaim = context.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                context.Result = new ForbidResult();
                return;
            }

            var repository = context.HttpContext.RequestServices.GetService<IRepository>();
            if (repository == null)
            {
                context.Result = new StatusCodeResult(500);
                return;
            }

            var isWarehouseWorker = repository.GetAllAttached<RequesterAndBuyerUser>().Any(w => w.Id == userId);

            if (!isWarehouseWorker)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
