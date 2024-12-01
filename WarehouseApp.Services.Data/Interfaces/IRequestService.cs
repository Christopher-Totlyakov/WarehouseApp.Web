using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Web.ViewModels.Requests;

namespace WarehouseApp.Services.Data.Interfaces
{
    public interface IRequestService
    {
        Task<IEnumerable<RequestListViewModel>> GetAllRequestsAsync();

        Task<RequestDetailsViewModel?> GetRequestDetailsAsync(int requestId);
    }
}
