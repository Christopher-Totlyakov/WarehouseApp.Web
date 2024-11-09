using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models.Users;

namespace WarehouseApp.Data.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public Guid SupplierId { get; set; }

        [ForeignKey(nameof(SupplierId))]
        public ICollection<Supplier> Supplier { get; set; } = new HashSet<Supplier>();

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string Status { get; set; } = null!;

        [Required]
        public Guid RequestedByWorkerId { get; set; }

        [ForeignKey(nameof(RequestedByWorkerId))]
        public WarehouseWorker RequestedByWorker { get; set; } = null!;

        public ICollection<OrderProduct> OrderProducts { get; set; } = new HashSet<OrderProduct>();

        public ICollection<SupplierOrder> SupplierOrder { get; set; } = new HashSet<SupplierOrder>();
    }
}
