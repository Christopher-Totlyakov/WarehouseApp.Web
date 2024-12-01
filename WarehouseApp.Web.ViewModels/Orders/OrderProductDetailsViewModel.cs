using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Web.ViewModels.Orders
{
    public class OrderProductDetailsViewModel
    {
        public string ProductName { get; set; } = null!;
        public int QuantityOrdered { get; set; }
    }
}
