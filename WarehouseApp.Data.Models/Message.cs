using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.Models.Interfaces;
using WarehouseApp.Data.Models.Users;
using Microsoft.EntityFrameworkCore;

using static WarehouseApp.Common.EntityValidationConstants.Message;

namespace WarehouseApp.Data.Models
{
    public class Message
    {
        [Key]
        [Comment("Message Identifier")]
        public int MessageId { get; set; }

        [Required]
        [Comment("User who can send message (SenderMassageUser)")]
        public Guid SenderId { get; set; }

        [ForeignKey(nameof(SenderId))]
        public SenderMessageUser Sender { get; set; } = null!;

        [Comment("Person (WarehouseWorker) who read message")]
        public Guid? ReceiverId { get; set; }

        [ForeignKey(nameof(ReceiverId))]
        public WarehouseWorker? Receiver { get; set; }

        public ICollection<MessagesForWarehouseWorker> MessagesForWarehouseWorker { get; set; } = new HashSet<MessagesForWarehouseWorker>();

        [Required]
        [Comment("Message type")]
        [MaxLength(MassageTypeMaxLength)]
        public string MessageType { get; set; } = null!;

        [Required]
        [Comment("Message content")]
        [MaxLength(MassageContentMaxLength)]
        public string MessageContent { get; set; } = null!;

        [Required]
        [Comment("Sent date")]
        public DateTime SentDate { get; set; }

        [Required]
        [Comment("Message status")]
        [StringLength(MassageStatusMaxLength)]
        public string Status { get; set; } = null!;

        [Required]
        public bool SoftDelete { get; set; } = false;
    }
}
