using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models.Users;
using Microsoft.EntityFrameworkCore;
using static WarehouseApp.Common.EntityValidationConstants.Order;

namespace WarehouseApp.Data.Models
{
    public class Order
    {
        [Key]
        [Comment("Order Identifier")]
        public int OrderId { get; set; }

        [Required]
        [Comment("Supplier Identifier")]
        public Guid SupplierId { get; set; }

        [ForeignKey(nameof(SupplierId))]
        public ICollection<Supplier> Supplier { get; set; } = new HashSet<Supplier>();

        [Required]
        [Comment("Order date")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Comment("Order status")]
        [MaxLength(OrderStatusMaxLength)]
        public string Status { get; set; } = null!;

        [Required]
        [Comment("Worker who requests an order")]
        public Guid RequestedByWorkerId { get; set; }

        [ForeignKey(nameof(RequestedByWorkerId))]
        public WarehouseWorker RequestedByWorker { get; set; } = null!;

        public ICollection<OrderProduct> OrderProducts { get; set; } = new HashSet<OrderProduct>();

    }
}
