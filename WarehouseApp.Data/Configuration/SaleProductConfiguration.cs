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
    public class SaleProductConfiguration : IEntityTypeConfiguration<SaleProduct>
    {
        public void Configure(EntityTypeBuilder<SaleProduct> builder)
        {
            builder.HasKey(sp => new { sp.ProductId, sp.SaleId });

            builder.HasOne(sp => sp.Sale)
                .WithMany(c => c.SaleProducts)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sp => sp.Product)
                .WithMany(p => p.SaleProducts)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
