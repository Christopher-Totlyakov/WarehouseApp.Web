using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Web.ViewModels.Requests
{
    public class RequestDetailsViewModel
    {
        public int RequestId { get; set; }
        public string RequesterEmail { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime RequestDate { get; set; }
        public string? ProcessedByWorkerEmail { get; set; }
        public string? Note { get; set; }
        public IEnumerable<RequestProductDetailsViewModel> Products { get; set; } = new List<RequestProductDetailsViewModel>();

    }
}
