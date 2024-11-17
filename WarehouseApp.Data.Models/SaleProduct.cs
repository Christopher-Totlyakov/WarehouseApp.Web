using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static WarehouseApp.Common.EntityValidationConstants.SaleProduct;

namespace WarehouseApp.Data.Models
{
    public class SaleProduct
    {
      
        [Required]
        [Comment("Sale Identifier")]
        public int SaleId { get; set; }

        [ForeignKey(nameof(SaleId))]
        public Sale Sale { get; set; } = null!;

        [Required]
        [Comment("Product Identifier")]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

        [Required]
        [Comment("Quantity of product sold")]
        [Range(QuantitySoldMin, QuantitySoldMax)]
        public int QuantitySold { get; set; }

        [Required]
        [Comment("Product unit price")]
        [Range(UnitPriceMin, UnitPriceMax)]
        public decimal UnitPrice { get; set; }
    }
}
