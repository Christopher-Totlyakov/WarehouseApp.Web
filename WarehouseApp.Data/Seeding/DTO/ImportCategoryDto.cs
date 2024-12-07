using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static WarehouseApp.Common.EntityValidationConstants.Category;

namespace WarehouseApp.Data.Seeding.DTO
{
    internal class ImportCategoryDto
    {
        [Required]
        [MaxLength(CategoryNameMaxLength)]
        [MinLength(CategoryNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        public bool SoftDelete { get; set; }
    }
}
