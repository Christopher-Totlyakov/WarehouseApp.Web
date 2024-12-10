using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Web.ViewModels.ShoppingCart
{
    public class AddToCartViewModel
    {
        public int ProductId { get; set; }
        [Range(1,999)]
        public int Quantity { get; set; }
    }
}
