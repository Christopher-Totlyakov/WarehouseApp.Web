using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static WarehouseApp.Common.EntityValidationConstants.RequestProduct;

namespace WarehouseApp.Data.Models
{
    public class RequestProduct
    {
        [Required]
        [Comment("Request Identifier")]
        public int RequestId { get; set; }

        [ForeignKey(nameof(RequestId))]
        public Request Request { get; set; } = null!;

        [Required]
        [Comment("Product Identifier")]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

        [Required]
        [Comment("Requested product quantity")]
        [Range(QuantityRequestedMin, QuantityRequestedMax)]
        public int QuantityRequested { get; set; }

        [Required]
        [Comment("Product price on request")]
        [Range(PPriceUponRequestMin, PriceUponRequestMax)]
        public decimal PriceUponRequest { get; set; }
    }
}
