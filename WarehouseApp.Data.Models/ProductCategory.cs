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
        public int ProductId { get; set; }

        
        public Product Product { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        
        public Category Category { get; set; } = null!;

    }
}
