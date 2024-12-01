using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Web.ViewModels.Sales
{
    public class SaleProductViewModel
    {
        public string ProductName { get; set; } = null!;
        public int QuantitySold { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => QuantitySold * UnitPrice;
    }
}
