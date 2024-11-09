using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models.Interfaces;
using WarehouseApp.Data.Models.Users;

namespace WarehouseApp.Data.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        public Guid SenderId { get; set; }

        [ForeignKey(nameof(SenderId))]
        public SenderMessageUser Sender { get; set; } = null!;

        [Required]
        public Guid ReceiverId { get; set; }

        [ForeignKey(nameof(ReceiverId))]
        public ICollection<WarehouseWorker> Receiver { get; set; } = new HashSet<WarehouseWorker>();

        public ICollection<MessagesForWarehouseWorker> MessagesForWarehouseWorker { get; set; } = new HashSet<MessagesForWarehouseWorker>();

        [Required]
        public string MessageType { get; set; } = null!;// Запитване или оплакване

        [Required]
        public string MessageContent { get; set; } = null!;

        [Required]
        public DateTime SentDate { get; set; }

        [Required]
        public string Status { get; set; } = null!;// Прочетено, Непрочетено
    }
}
