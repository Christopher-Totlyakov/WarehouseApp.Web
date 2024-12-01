using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Web.ViewModels.Sales
{
    public class SaleDetailsViewModel
    {
        public int SaleId { get; set; }
        public string CustomerName { get; set; } = null!;
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SaleProductViewModel> Products { get; set; } = new();
    }
}
