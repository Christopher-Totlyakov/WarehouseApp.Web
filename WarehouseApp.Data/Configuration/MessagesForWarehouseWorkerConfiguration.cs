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
    public class MessagesForWarehouseWorkerConfiguration : IEntityTypeConfiguration<MessagesForWarehouseWorker>
    {
        public void Configure(EntityTypeBuilder<MessagesForWarehouseWorker> builder)
        {
            builder.HasKey(mfww => new { mfww.WarehouseWorkerId, mfww.MassageId });

            builder.HasOne(mfww => mfww.Massage)
                .WithMany(mfww => mfww.MessagesForWarehouseWorker)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(mfww => mfww.WarehouseWorker)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
