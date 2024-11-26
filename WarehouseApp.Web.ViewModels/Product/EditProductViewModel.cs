using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Services.Mapping;
using WarehouseApp.Web.ViewModels.Category;
using static WarehouseApp.Common.EntityValidationConstants.Product;


namespace WarehouseApp.Web.ViewModels.Product
{
    public class EditProductViewModel : IMapTo<Data.Models.Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ProductNameMinLength, ErrorMessage = "The product name must be more than {1} characters.")]
        [MaxLength(ProductNameMaxLength, ErrorMessage = "The product name cannot exceed {1} characters.")]
        public string Name { get; set; } = null!;

        [StringLength(ImagePathMaxLength, ErrorMessage = "The image path cannot exceed {1} characters.")]
        public string? ImagePath { get; set; } = null!;

        [Required]
        [MaxLength(ProductDescriptionMaxLength, ErrorMessage = "The description cannot exceed {1} characters.")]
        [MinLength(ProductDescriptionMinLength, ErrorMessage = "The description must be more than {1} characters.")]
        public string Description { get; set; } = null!;

        [Required]
        [Range(ProductPriceMin, ProductPriceMax, ErrorMessage = "The price must be between {1} and {2}.")]
        public decimal Price { get; set; }

        [Required]
        [Range(ProductStockQuantityMin, ProductStockQuantityMax, ErrorMessage = "The stock quantity must be between {1} and {2}.")]
        public uint StockQuantity { get; set; }

        // Свързани категории
        public List<int> SelectedCategoryIds { get; set; } = new List<int>();
        public IEnumerable<CategoryViewModel> AvailableCategories { get; set; } = new List<CategoryViewModel>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<EditProductViewModel, Data.Models.Product>()
                .ForMember(d => d.ProductCategories, x => x.Ignore());
        }
    }
}
