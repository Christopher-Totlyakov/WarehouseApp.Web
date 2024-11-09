using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models;

namespace WarehouseApp.Data.Configuration
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(pc => new { pc.ProductId, pc.CategoryId });

            builder.HasOne(cp => cp.Category)
                .WithMany(c => c.ProductCategories)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cp => cp.Product)
                .WithMany(p => p.ProductCategories)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
