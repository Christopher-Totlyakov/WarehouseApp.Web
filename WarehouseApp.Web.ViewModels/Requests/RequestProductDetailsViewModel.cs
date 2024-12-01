using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Web.ViewModels.Requests
{
    public class RequestProductDetailsViewModel
    {
        public string ProductName { get; set; } = null!;
        public int QuantityRequested { get; set; }
        public decimal PriceUponRequest { get; set; }
    }
}
