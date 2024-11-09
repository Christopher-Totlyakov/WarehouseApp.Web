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
    public class RequestProductConfiguration : IEntityTypeConfiguration<RequestProduct>
    {
        public void Configure(EntityTypeBuilder<RequestProduct> builder)
        {
            builder.HasKey(rp => new { rp.ProductId, rp.RequestId });

            builder.HasOne(cp => cp.Product)
                .WithMany(c => c.RequestProducts)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cp => cp.Request)
                .WithMany(p => p.RequestProducts)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
