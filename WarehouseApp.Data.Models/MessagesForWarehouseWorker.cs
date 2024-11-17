using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models.Users;

namespace WarehouseApp.Data.Models
{
    public class MessagesForWarehouseWorker
    {
        [Required]
        [Comment("Warehouse Worker Identifier")]
        public Guid WarehouseWorkerId { get; set; }

        public WarehouseWorker WarehouseWorker { get; set; } = null!;

        [Required]
        [Comment("Message Identifier")]
        public int MassageId { get; set; }

        public Message Massage { get; set; } = null!;
    }
}
