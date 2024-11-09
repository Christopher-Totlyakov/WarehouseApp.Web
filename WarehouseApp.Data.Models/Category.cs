using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int ProductCategoriesId { get; set; }

        [ForeignKey(nameof(ProductCategoriesId))]
        public ICollection<ProductCategory> ProductCategories { get; set; } = new HashSet<ProductCategory>();

    }
}
