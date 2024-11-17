using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WarehouseApp.Common.EntityValidationConstants.Category;

namespace WarehouseApp.Data.Models
{
    public class Category
    {
        [Key]
        [Comment("Category Identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        [Comment("Category Name")]
        public string Name { get; set; } = null!;

        public ICollection<ProductCategory> ProductCategories { get; set; } = new HashSet<ProductCategory>();

    }
}
