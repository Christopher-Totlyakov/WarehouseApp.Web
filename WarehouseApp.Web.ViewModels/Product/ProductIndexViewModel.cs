using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Services.Mapping;

namespace WarehouseApp.Web.ViewModels.Product
{
    public class ProductIndexViewModel : IMapFrom<Data.Models.Product>
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? ImagePath { get; set; }

        public decimal Price { get; set; }
    }
}
