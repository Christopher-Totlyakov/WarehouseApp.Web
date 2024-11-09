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
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey(op => new { op.OrderId, op.ProductId });

            builder.HasOne(op => op.Product)
                .WithMany(p => p.OrderProducts)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(op => op.Order)
                .WithMany(p => p.OrderProducts)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
