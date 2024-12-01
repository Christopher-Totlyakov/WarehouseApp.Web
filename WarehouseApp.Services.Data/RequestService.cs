using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models;
using WarehouseApp.Data.Models.Interfaces;
using WarehouseApp.Data.Repository.Interfaces;
using WarehouseApp.Services.Data.Interfaces;
using WarehouseApp.Services.Mapping;
using WarehouseApp.Web.ViewModels.Product;
using WarehouseApp.Web.ViewModels.Requests;

namespace WarehouseApp.Services.Data
{
    public class RequestService : IRequestService
    {
        private readonly IRepository repository;

        public RequestService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<RequestListViewModel>> GetAllRequestsAsync()
        {
            return await repository.GetAllAttached<Request>()
            .Select(r => new RequestListViewModel
            {
                RequestId = r.RequestId,
                RequesterEmail = r.Requester.Email,
                Status = r.Status,
                RequestDate = r.RequestDate
            }).ToListAsync();
        }

        public async Task<RequestDetailsViewModel?> GetRequestDetailsAsync(int requestId)
        {
            var request = await repository.GetAllAttached<Request>()
                .Where(r => r.RequestId == requestId)
                .Select(r => new RequestDetailsViewModel
                {
                    RequestId = r.RequestId,
                    RequesterEmail = r.Requester.Email,
                    Status = r.Status,
                    RequestDate = r.RequestDate,
                    ProcessedByWorkerEmail = r.ProcessedByWorker != null ? r.ProcessedByWorker.Email : null,
                    Note = r.Note,
                    Products = r.RequestProducts.Select(rp => new RequestProductDetailsViewModel
                    {
                        ProductName = rp.Product.Name,
                        QuantityRequested = rp.QuantityRequested,
                        PriceUponRequest = rp.PriceUponRequest
                    })
                }).FirstOrDefaultAsync();

            return request;
        }
    }
}
