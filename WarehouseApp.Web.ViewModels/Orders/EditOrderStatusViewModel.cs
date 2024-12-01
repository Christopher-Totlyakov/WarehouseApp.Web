using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Web.ViewModels.Orders
{
    public class EditOrderStatusViewModel
    {
        public int OrderId { get; set; }
        public string Status { get; set; } = null!;
        public List<string> StatusOptions { get; set; } = new List<string>();
    }
}
