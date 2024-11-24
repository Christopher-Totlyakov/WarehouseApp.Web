using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Services.Mapping;

namespace WarehouseApp.Web.ViewModels.ShoppingCart
{
    public class ShoppingCartItems : IMapFrom<Data.Models.Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public bool InStok { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Data.Models.Product, ShoppingCartItems>()
                .ForMember(p => p.Quantity, x => x.Ignore());

            configuration.CreateMap<ShoppingCartItems, ShoppingCartItems>();

        }
    }
}