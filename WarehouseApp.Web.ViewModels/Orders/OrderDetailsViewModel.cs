using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Web.ViewModels.Orders
{
    public class OrderDetailsViewModel
    {
        public int OrderId { get; set; }
        public string SupplierName { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = null!;
        public string RequestedByWorkerName { get; set; } = null!;
        public List<OrderProductDetailsViewModel> Products { get; set; } = new();
    }
}
