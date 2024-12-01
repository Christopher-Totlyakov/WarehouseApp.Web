using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Web.ViewModels.Orders
{
    public class OrderProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        [Range(1, 999, ErrorMessage = "Quantity must be between 1 and 999.")]
        public int QuantityOrdered { get; set; }
        public bool IsAutoAdded { get; set; } = false;
    }
}
