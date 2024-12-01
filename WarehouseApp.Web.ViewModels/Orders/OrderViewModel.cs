using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Web.ViewModels.Orders
{
    public class OrderViewModel
    {
        public Guid SupplierId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending";
        
        public List<OrderProductViewModel> OrderProducts { get; set; } = new();
        //public List<OrderProductViewModel> AvailableProducts { get; set; } = new();
    }
}
