using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Web.ViewModels.Product;

namespace WarehouseApp.Web.ViewModels.Home
{
    public class HomeViewModel
    {
        public IEnumerable<ProductIndexViewModel> TopProducts { get; set; } = new List<ProductIndexViewModel>();
        public IEnumerable<ProductIndexViewModel> NewProducts { get; set; } = new List<ProductIndexViewModel>();
    }
}
