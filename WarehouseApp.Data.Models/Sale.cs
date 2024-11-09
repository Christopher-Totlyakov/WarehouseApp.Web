using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models.Interfaces;
using WarehouseApp.Data.Models.Users;

namespace WarehouseApp.Data.Models
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public RequesterAndBuyerUser Customer { get; set; } = null!;

        public int? RequestId { get; set; }

        [ForeignKey(nameof(RequestId))]
        public Request Request { get; set; }

        [Required]
        public DateTime SaleDate { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public ICollection<SaleProduct> SaleProducts { get; set; } = new HashSet<SaleProduct>();
    }
}
