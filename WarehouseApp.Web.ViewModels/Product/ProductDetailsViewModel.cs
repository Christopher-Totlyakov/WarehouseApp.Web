using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Web.ViewModels.Product
{
    public class ProductDetailsViewModel
    {
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public string? ImagePath { get; set; }

		public string Description { get; set; } = null!;

		public decimal Price { get; set; }

		public uint StockQuantity { get; set; }

		public IEnumerable<string> Categories { get; set; } = new List<string>();
	}
}
