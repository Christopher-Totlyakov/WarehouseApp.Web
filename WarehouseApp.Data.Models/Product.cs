using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WarehouseApp.Common.EntityValidationConstants.Product;

namespace WarehouseApp.Data.Models
{
    public class Product
    {
        [Key]
        [Comment("Product Identifier")]
        public int Id { get; set; }

        [Required]
        [Comment("Product name")]
        [StringLength(ProductNameMaxLength)]
        public string Name { get; set; } = null!;

        [Comment("Image path in file system")]
        [StringLength(ImagePathMaxLength)]
        public string? ImagePath { get; set; } = null!;

        [Required]
        [Comment("Product description")]
        [StringLength(ProductDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Comment("Product Price")]
        [Range(ProductPriceMin,ProductPriceMax)]
        public decimal Price { get; set; }

        [Required]
        [Comment("Product quantity on stock")]
        [Range(ProductStockQuantityMin, ProductStockQuantityMax)]
        public uint StockQuantity { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; } = new HashSet<ProductCategory>();

        public ICollection<RequestProduct> RequestProducts { get; set; } = new HashSet<RequestProduct>();

        public ICollection<SaleProduct> SaleProducts { get; set; } = new HashSet<SaleProduct>();

        public ICollection<OrderProduct> OrderProducts { get; set; } = new HashSet<OrderProduct>();
    }
}
