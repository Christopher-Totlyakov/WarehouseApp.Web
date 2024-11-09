using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models.Users;

namespace WarehouseApp.Data.Models
{
    public class SupplierOrder
    {
        [Required]
        public int OrderId { get; set; }

        public Order Order { get; set; } = null!;

        [Required]
        public Guid SupplierId { get; set; }

        public Supplier Supplier { get; set; } = null!;
    }
}
