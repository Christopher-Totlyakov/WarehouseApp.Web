using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static WarehouseApp.Common.EntityValidationConstants.OrderProduct;

namespace WarehouseApp.Data.Models
{
    public class OrderProduct
    {
        [Required]
        [Comment("Order Identifier")]
        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; } = null!;

        [Required]
        [Comment("Product Identifier")]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

        [Required]
        [Comment("Quantity of a particular product")]
        [Range(QuantityOrderedMinLength,QuantityOrderedMaxLength)]
        public int QuantityOrdered { get; set; }
    }
}
