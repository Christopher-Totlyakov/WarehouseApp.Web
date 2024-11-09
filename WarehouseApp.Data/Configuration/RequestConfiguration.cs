using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models;

namespace WarehouseApp.Data.Configuration
{
    public class RequestConfiguration : IEntityTypeConfiguration<Models.Request>
    {
        public void Configure(EntityTypeBuilder<Models.Request> builder)
        {
            builder
                .HasOne(r => r.Requester)
                .WithMany()
                .HasForeignKey(r => r.RequesterId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(r => r.ProcessedByWorker)
                .WithMany()
                .HasForeignKey(r => r.ProcessedByWorkerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

