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
using static WarehouseApp.Common.EntityValidationConstants.Request;

namespace WarehouseApp.Data.Models
{
    public class Request
    {
        [Key]
        [Comment("Request Identifier")]
        public int RequestId { get; set; }

        [Required]
        [Comment("User who can request and buy product")]
        public Guid RequesterId { get; set; }

        [ForeignKey(nameof(RequesterId))]
        public RequesterAndBuyerUser Requester { get; set; } = null!;

        [Required]
        [Comment("Request status")]
        [MaxLength(RequestStatusMaxLength)]
        public string Status { get; set; } = null!;

        [Required]
        [Comment("Request date")]
        public DateTime RequestDate { get; set; }

        [Comment("Warehouse worker who checked the request")]
        public Guid? ProcessedByWorkerId { get; set; }

        [ForeignKey(nameof(ProcessedByWorkerId))]
        public WarehouseWorker? ProcessedByWorker { get; set; } = null!;

        [Comment("Warehouse worker who checked the request")]
        [MaxLength(RequestNoteMaxLength)]
        public string? Note { get; set; }

        public ICollection<RequestProduct> RequestProducts { get; set; } = new HashSet<RequestProduct>();

        [Required]
        public bool SoftDelete { get; set; } = false;
    }
}
