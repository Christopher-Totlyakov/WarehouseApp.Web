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
    public class SupplierOrderConfiguration : IEntityTypeConfiguration<SupplierOrder>
    {
        public void Configure(EntityTypeBuilder<SupplierOrder> builder)
        {
            builder.HasKey(sc => new { sc.OrderId, sc.SupplierId });

            builder.HasOne(sc => sc.Supplier)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sc => sc.Order)
                .WithMany(p => p.SupplierOrder)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

