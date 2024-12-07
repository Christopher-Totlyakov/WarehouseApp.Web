using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models;
using WarehouseApp.Data.Seeding.DTO;
using WarehouseApp.Services.Mapping;

namespace WarehouseApp.Data.Seeding
{
    public static class DbSeeder
    {
        public static async Task SeedProductsAsync(IServiceProvider services, string jsonPath)
        {
            using var dbContext = services.GetRequiredService<WarehouseDbContext>();

            ICollection<Product> existingProducts = await dbContext.Products.ToListAsync();

            try
            {
                string jsonInput = await File.ReadAllTextAsync(jsonPath, Encoding.ASCII, CancellationToken.None);
                var productDtos = JsonConvert.DeserializeObject<ImportProductDTO[]>(jsonInput);

                if (productDtos == null)
                {
                    Console.WriteLine("No products to import.");
                    return;
                }

                foreach (var productDto in productDtos)
                {
                    if (!IsValid(productDto))
                    {
                        Console.WriteLine($"Invalid product: {productDto.Name}");
                        continue;
                    }

                    if (existingProducts.Any(p => p.Name == productDto.Name))
                    {
                        Console.WriteLine($"Product already exists: {productDto.Name}");
                        continue;
                    }

                    Product product = AutoMapperConfig.MapperInstance.Map<Product>(productDto);
                    await dbContext.Products.AddAsync(product);
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding products: {ex.Message}");
            }
        }

        public static async Task SeedCategoriesAsync(IServiceProvider services, string jsonPath)
        {
            await using var dbContext = services.GetRequiredService<WarehouseDbContext>();

            ICollection<Category> existingCategories = await dbContext.Categories.ToListAsync();

            try
            {
                string jsonInput = await File.ReadAllTextAsync(jsonPath, Encoding.UTF8);
                var categoryDtos = JsonConvert.DeserializeObject<ImportCategoryDto[]>(jsonInput);

                foreach (var dto in categoryDtos)
                {
                    if (!IsValid(dto) || existingCategories.Any(c => c.Name == dto.Name))
                    {
                        continue;
                    }

                    var category = new Category
                    {
                        Name = dto.Name,
                        SoftDelete = dto.SoftDelete
                    };

                    await dbContext.Categories.AddAsync(category);
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding categories: {ex.Message}");
            }
        }

        public static async Task SeedProductsWithCategoriesAsync(IServiceProvider services, string productsJsonPath, string categoriesJsonPath)
        {
            await using var dbContext = services.GetRequiredService<WarehouseDbContext>();

            ICollection<Category> categories = await dbContext.Categories.ToListAsync();
            ICollection<Product> existingProducts = await dbContext.Products.ToListAsync();

            if (!categories.Any())
            {
                await SeedCategoriesAsync(services, categoriesJsonPath);
                categories = await dbContext.Categories.ToListAsync();
            }

            if (!existingProducts.Any()) 
            {
                await SeedProductsAsync(services, productsJsonPath);
                categories = await dbContext.Categories.ToListAsync();
            }

            foreach (var product in existingProducts)
            {
                if (!IsValid(product))
                {
                    continue;
                }

                var assignedCategories = categories.OrderBy(_ => Guid.NewGuid()).Take(2).ToList();

                var productCategories = new List<ProductCategory>();

                foreach (var category in assignedCategories)
                {
                    productCategories.Add(new ProductCategory
                    {
                        CategoryId = category.Id,
                        ProductId = product.Id
                    });
                }

                var existingProductCategories = dbContext.ProductCategories
                    .Where(pc => pc.ProductId == product.Id)
                    .Select(pc => pc.CategoryId)
                    .ToList();

                var newProductCategories = productCategories
                    .Where(pc => !existingProductCategories.Contains(pc.CategoryId))
                    .ToList();

                await dbContext.ProductCategories.AddRangeAsync(newProductCategories);
            }

            await dbContext.SaveChangesAsync();
        }




        private static bool IsValid(object obj)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(obj);
            return Validator.TryValidateObject(obj, context, validationResults, true);
        }
    }
}
