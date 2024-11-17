using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models.Interfaces;
using WarehouseApp.Data.Models.Users;
using static WarehouseApp.Common.EntityValidationConstants.Sale;
using Microsoft.EntityFrameworkCore;

namespace WarehouseApp.Data.Models
{
    public class Sale
    {
        [Key]
        [Comment("Sale Identifier")]
        public int SaleId { get; set; }

        [Required]
        [Comment("Customer Identifier")]
        public Guid CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public RequesterAndBuyerUser Customer { get; set; } = null!;

        [Comment("Whether the sale was made by request")]
        public int? RequestId { get; set; }

        [ForeignKey(nameof(RequestId))]
        public Request? Request { get; set; }

        [Required]
        [Comment("Sale date")]
        public DateTime SaleDate { get; set; }

        [Required]
        [Comment("Total amount owed")]
        [Range(TotalAmountMin, TotalAmountMax)]
        public decimal TotalAmount { get; set; }

        public ICollection<SaleProduct> SaleProducts { get; set; } = new HashSet<SaleProduct>();
    }
}
