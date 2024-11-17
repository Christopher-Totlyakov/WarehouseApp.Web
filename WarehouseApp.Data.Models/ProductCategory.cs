using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Data.Models
{
    public class ProductCategory
    {
        [Required]
        [Comment("Product Identifier")]
        public int ProductId { get; set; }
                
        public Product Product { get; set; } = null!;

        [Required]
        [Comment("Category Identifier")]
        public int CategoryId { get; set; }
                
        public Category Category { get; set; } = null!;
    }
}
