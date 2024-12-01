using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Web.ViewModels.ShoppingCart
{
    public class RequestProductViewModel
    {

        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public int Quantity { get; set; }

        public decimal PriceUponRequest { get; set; }

        public bool IsInStock { get; set; }


    }
}
