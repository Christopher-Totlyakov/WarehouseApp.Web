using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Web.ViewModels.Admin;

namespace WarehouseApp.Services.Data.Interfaces
{
    public interface IUsersServices
    {
        Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync();

        Task<UserDetailsViewModel> GetUserByIdAsync(string userId);

        Task<bool> DeletePersonalDataAsync(string userId);
    }
}
