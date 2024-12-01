using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Web.ViewModels.Sales
{
    public class SaleViewModel
    {
        public int SaleId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
