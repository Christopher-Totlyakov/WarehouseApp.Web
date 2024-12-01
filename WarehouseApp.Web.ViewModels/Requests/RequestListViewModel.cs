using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Web.ViewModels.Requests
{
    public class RequestListViewModel
    {
        public int RequestId { get; set; }
        public string RequesterEmail { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime RequestDate { get; set; }
    }
}
