using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Migrations;
using WarehouseApp.Data.Models;
using WarehouseApp.Services.Mapping;
using static WarehouseApp.Common.EntityValidationConstants.Product;

namespace WarehouseApp.Data.Seeding.DTO
{
    public class ImportProductDTO : IMapTo<Product>
    {

        [Required]
        [MaxLength(Common.EntityValidationConstants.Product.ProductNameMaxLength)]
        [MinLength(ProductNameMinLength)]
        public string Name { get; set; } = null!;

        [StringLength(Common.EntityValidationConstants.Product.ImagePathMaxLength)]
        public string? ImagePath { get; set; } = null!;

        [Required]
        [MaxLength(ProductDescriptionMaxLength)]
        [MinLength(ProductDescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(ProductPriceMin, ProductPriceMax)]
        public decimal Price { get; set; }

        [Required]
        [Range(ProductStockQuantityMin, ProductStockQuantityMax)]
        public uint StockQuantity { get; set; }

        [Required]
        public bool SoftDelete { get; set; } = false;
    }
}
